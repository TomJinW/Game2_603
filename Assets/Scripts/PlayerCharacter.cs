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

    [SerializeField] Transform[] spawnPoints;


    //Input Handling
    bool moveLeft = false;
    bool moveRight = false;
    bool jumping = false;
    public bool canMove = true;
    bool pushing = false;
    int checkPointReached = 0;
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

        else if (col.gameObject.CompareTag("Flower"))
        {
            checkPointReached = 1;          
        }

        else if (col.gameObject.CompareTag("Stomp"))
        {
            checkPointReached = 2;
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
        transform.position = spawnPoints[checkPointReached].position;
        ball.transform.position = spawnPoints[checkPointReached].GetChild(0).transform.position;
        ball.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Kicked()
    {
        StartCoroutine("GetKicked");
    }

    public IEnumerator GetKicked()
    {
        canMove = false;
        rb.AddForce(new Vector2(-1250f, 1000f));
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-550f, 300f));
        
        yield return new WaitForSeconds(5.5f);
        canMove = true;
        swingingFoot.GetComponent<Animator>().SetBool("Swinging_b", false);
        checkPointReached = 0;
        PlayerReset();
        
    }
}
