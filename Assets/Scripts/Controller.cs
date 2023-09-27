using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Controller : MonoBehaviour
{
    //Start() variables
    private Rigidbody2D rb;
    [SerializeField] Animator anim;
    private Collider2D coll;
    private AudioSource theme;

    //FSM
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int Points = 0;
    [SerializeField] private int Points1 = 0;
    [SerializeField] private TextMeshProUGUI PointsText;
    [SerializeField] private TextMeshProUGUI PointsText1;
    [SerializeField] private float hurtForce = 1f;
    [SerializeField] private AudioSource coins;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource landing;
    [SerializeField] private TextMeshProUGUI PointsDisplay;
    [SerializeField] private TextMeshProUGUI PointsDisplay1;
    [SerializeField] private TextMeshProUGUI X;
    [SerializeField] private GameObject Boy;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] Image coinImage;
    [SerializeField] Image[] lives;
    [SerializeField] int livesRemaining;
    [SerializeField] private string sceneName;
    [SerializeField] private AudioSource mUps;
    [SerializeField] private AudioSource mHurt;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        PointsText1.enabled = false;
        PointsDisplay.enabled = false;
        PointsDisplay1.enabled = false;
    }
    private void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state); //sets animation based on Enumerator state
    }
    private void OnTriggerEnter2D(Collider2D collision) //Trigger for Collectibles
    {
        if (collision.tag == "Collectible")
        {
            Destroy(collision.gameObject); //Coins destroy
            Coins();
            Points += 1;
            Points1 += 1;
            PointsText.text = Points.ToString(); //Converting number to string
            PointsText1.text = Points1.ToString();
        }
        else if(collision.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            mUps.Play();
            jumpForce = 25f;
            GetComponent<SpriteRenderer>().color = Color.blue;
            StartCoroutine(ResetJumpPower());
        }
        else if(collision.tag == "Flag")
        {
            Boy.SetActive(false);
            X.enabled = false;
            lives[0].enabled = false;
            lives[1].enabled = false;
            lives[2].enabled = false;
            coinImage.enabled = false;
            PointsText.enabled = false;
            PauseMenu.SetActive(false);
            PointsText1.enabled = true;
            PointsDisplay.enabled = true;
            PointsDisplay1.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right therefore should be damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    mHurt.Play();
                    LoseLife();
                }
                else
                {
                    //Enemy is to my left therefore i Should be damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    mHurt.Play();
                    LoseLife();
                }
            }

        }
        else if(other.gameObject.tag == "Spikes")
        {
            state = State.hurt;
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                //Enemy is to my right therefore should be damaged and move left
                rb.velocity = new Vector2(-hurtForce, 5f);
                mHurt.Play();
                LoseLife();
            }
            else
            {
                //Enemy is to my left therefore i Should be damaged and move right
                rb.velocity = new Vector2(hurtForce, 5f);
                mHurt.Play();
                LoseLife();
            }
        }
    }
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //Moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        //Moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb.velocity =  new Vector2(0, rb.velocity.y);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
        jumpMusic();
    }
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Coins()
    {
        coins.Play();
    }

    private void jumpMusic()
    {
        jump.Play();
    }

    private void Land()
    {
        landing.Play();
    }

    public void LoseLife()
    {
        livesRemaining--;
        lives[livesRemaining].enabled = false;

        if (livesRemaining == 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator ResetJumpPower()
    {
        yield return new WaitForSeconds(10);
        jumpForce = 18f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
