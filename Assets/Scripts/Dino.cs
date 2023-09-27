using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : Enemy
{

    [SerializeField] float leftCap;
    [SerializeField] float rightCap;
    [SerializeField] float speed = 10f;
    [SerializeField] LayerMask ground;


    private Collider2D coll;
    private bool facingLeft = true;
    private bool running = true;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            Move();
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            //Test to see if beyond the leftcap
            if (transform.position.x > leftCap)
            {
                //Make the sprite face the right direction
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            //Test to see if beyond the leftcap
            if (transform.position.x < rightCap)
            {
                //Make the sprite face the right direction
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                    rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                facingLeft = true;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            running = false;
            anim.SetBool("Idle", true);
            anim.SetBool("Running", false);
            StartCoroutine(MoveAgain());
        }
    }

    private IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(3f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetBool("Idle", false);
        anim.SetBool("Running", true);
        running = true;
    }

}
