using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float maxVelocity;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) < maxVelocity) 
        {
            if (Input.GetKey("d")) 
                rb.AddForce(new Vector3(speed, 0f, 0f));
            if(Input.GetKey("a"))
                rb.AddForce(new Vector3(-speed, 0f, 0f));
        }
        if (Input.GetKeyDown("space"))
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
        
    }
}
