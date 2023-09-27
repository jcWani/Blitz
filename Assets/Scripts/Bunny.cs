using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Enemy
{

    [SerializeField] float leftCap;
    [SerializeField] float rightCap;
    [SerializeField] float jumpLength = 10f;
    [SerializeField] float jumpHeight = 15f;
    [SerializeField] LayerMask ground;

    private Collider2D coll;
    private bool facingLeft = true;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump to fall
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        //Fall to idle
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
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
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //test to see if the bunny is on the ground, if true jump.
                if (coll.IsTouchingLayers(ground))
                {
                    //Jump
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
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
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                //test to see if the bunny is on the ground, if true jump.
                if (coll.IsTouchingLayers(ground))
                {
                    //Jump
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }

        }
    }



}
