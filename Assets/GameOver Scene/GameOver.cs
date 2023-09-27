using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private bool moving = true;
    [SerializeField] private float hurtForce = 1f;
    [SerializeField] private string sceneName;
    [SerializeField] private TextMeshProUGUI[] textAppear;
    [SerializeField] private GameObject Emote;
    [SerializeField] private AudioSource mOver;
    [SerializeField] private AudioSource mRestart;
    [SerializeField] private AudioSource mOuch;
    [SerializeField] private AudioSource mFootStep;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        Emote.SetActive(false);
        textAppear[0].enabled = false;
        textAppear[1].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Move();
        }

    }

    private void Move()
    {
        transform.localScale = new Vector3(1, 1);
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stop")
        {
            rb.velocity = new Vector2(-hurtForce, 5f);
            mOuch.Play();
            moving = false;
            StartCoroutine(Sad());
            StartCoroutine(Appear());
            anim.SetTrigger("Die");
        }
    }

    private IEnumerator Appear()
    {
        yield return new WaitForSeconds(4f);
        textAppear[0].enabled = true;
        textAppear[1].enabled = true;
    }

    private IEnumerator Sad()
    {
        yield return new WaitForSeconds(2f);
        Emote.SetActive(true);
        mOver.Play();
    }
    public void PlayAgain()
    {
        mOver.Stop();
        mRestart.Play();
        StartCoroutine(changeScene());
    }

    private IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    private void Step()
    {
        mFootStep.Play();
    }


}

