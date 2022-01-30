using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    //Input Handling
    bool moveLeft = false;
    bool moveRight = false;
    bool jumping = false;
    bool hasJumped = false;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        sr = transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
      
        
        if (Input.GetKey("a"))
        {
            moveLeft = true;
            sr.flipX = true;
        }
        
        else
        {
            moveLeft = false;
        }

        if (Input.GetKey("d"))
        {
            moveRight = true;
            sr.flipX = false;
        }

        else
        {
            moveRight = false;
        }

        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            anim.SetBool("is_Walking_b", true);
        }
        else
        {
            anim.SetBool("is_Walking_b", false);
        }


        if (Input.GetKey("space"))
        {
            jumping = true;

        }
        
        else
        {
            jumping = false;

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate() 
    {
        if (moveLeft) 
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        if (moveRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (jumping && !hasJumped) 
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            hasJumped = true;
        }

    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        hasJumped = false;
    }


}
