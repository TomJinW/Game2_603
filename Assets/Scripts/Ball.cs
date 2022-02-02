using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Rigidbody2D rb;
    public float defaultScale = 1f;
    [SerializeField] float scaleIncrement = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Mud")) 
        {
            col.gameObject.SetActive(false);
            transform.localScale += new Vector3(scaleIncrement, scaleIncrement, 0f);
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Mud"))
    //    {
    //        rb.angularDrag = mudSpeedPenalty;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D col)
    //{
    //    rb.angularDrag = 0.05f;
    //}
}
