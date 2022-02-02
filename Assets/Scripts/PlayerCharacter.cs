using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    public Canvas titleScreenCanvas;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    [SerializeField] float defaultSpeed;
    [SerializeField] float defaultJumpForce;
    [SerializeField] float mudSpeedPenalty = 0.5f;

    [SerializeField] GameObject ball;

    [SerializeField] Transform playerSpawn;
    [SerializeField] Transform ballSpawn;

    //Input Handling
    bool moveLeft = false;
    bool moveRight = false;
    bool jumping = false;
    public bool hasJumped = false;
    float speed;
    float jumpForce;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        sr = transform.GetComponent<SpriteRenderer>();
        speed = defaultSpeed;
        jumpForce = defaultJumpForce;
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
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                titleScreenCanvas.gameObject.SetActive(true);
            }
            //else
            //{
            //    Time.timeScale = 1;
            //    titleScreenCanvas.gameObject.SetActive(false);
            //}

            //Application.Quit();
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

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Mud")) 
        {
            speed *= mudSpeedPenalty;
            jumpForce *= mudSpeedPenalty;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        speed = defaultSpeed;
        jumpForce = defaultJumpForce;
    }

    public void PlayerReset() 
    {
        transform.position = playerSpawn.position;
        ball.transform.position = ballSpawn.position;
        ball.transform.localScale = new Vector3(1, 1, 1);
    }

}
