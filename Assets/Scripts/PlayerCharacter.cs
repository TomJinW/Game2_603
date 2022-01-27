using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    Rigidbody2D rb;
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
    }

    void Update()
    {
        if (Input.GetKey("d"))
            moveLeft = true;
        else
            moveLeft = false;
        if (Input.GetKey("a"))
            moveRight = true;
        else
            moveRight = false;

        if (Input.GetKey("space"))
            jumping = true;
        else
            jumping = false;
        
    }

    void FixedUpdate() 
    {
        if (moveLeft) 
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (moveRight)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
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
