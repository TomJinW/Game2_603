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
    [SerializeField] Foot stompingFoot;
    [SerializeField] Foot swingingFoot;

    [SerializeField] Transform playerSpawn1;
    [SerializeField] Transform ballSpawn1;
    [SerializeField] Transform playerSpawn2;
    [SerializeField] Transform ballSpawn2;

    //Input Handling
    bool moveLeft = false;
    bool moveRight = false;
    bool jumping = false;
    bool canMove = true;
    bool pushing = false;
    bool checkPointReached = false;
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

        if (canMove)
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
                if (pushing)
                {
                    anim.SetBool("is_Pushing_b", true);
                }
                else
                {
                    anim.SetBool("is_Pushing_b", false);
                    anim.SetBool("is_Walking_b", true);
                }
                
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
        }
        
       

        if (Input.GetKeyDown(KeyCode.P))
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

        if(Input.GetKeyDown(KeyCode.Escape))
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



        Debug.Log("Touched");
        hasJumped = false;

        if (Internals.winTime == 1)
        {
            Internals.winTime++;
            Time.timeScale = 0;
            titleScreenCanvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col) 
    {




        if (col.gameObject.CompareTag("Mud")) 
        {
            speed *= mudSpeedPenalty;
            jumpForce *= mudSpeedPenalty;
        }

        else if (col.gameObject.CompareTag("Stomp"))
        {
            checkPointReached = true;
            stompingFoot.SetInZone();
        }

        else if (col.gameObject.CompareTag("Spike"))
        {
            PlayerReset();
        }

        else if (col.gameObject.CompareTag("Stop_Stomp"))
        {
            stompingFoot.SetOutZone();
        }
        else if (col.gameObject.CompareTag("Swing"))
        {  
            swingingFoot.Swing();
        }

        else if (col.gameObject.CompareTag("Ball"))
        {
            pushing = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            pushing = false;
        }
        speed = defaultSpeed;
        jumpForce = defaultJumpForce;
    }

    public void PlayerReset() 
    {
        if (!checkPointReached)
        {
            transform.position = playerSpawn1.position;
            ball.transform.position = ballSpawn1.position;
        }
        else
        {
            transform.position = playerSpawn2.position;
            ball.transform.position = ballSpawn2.position;
        }
        ball.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Kicked()
    {
        StartCoroutine("GetKicked");
    }

    public IEnumerator GetKicked()
    {
        canMove = false;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-1350f, 900f));
        yield return new WaitForSeconds(4f);
        canMove = true;
        swingingFoot.GetComponent<Animator>().SetBool("Swinging_b", false);
    }
}
