using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UIElements;

public class Foot : MonoBehaviour
{

    private PlayerCharacter player;
    private Stopwatch stopwatch;
    public bool stomping = false;
    private bool onGround = false;
    private bool inZone = false;
    private Animator animator;

    public GameObject winTitle;
   

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        stopwatch = new Stopwatch();
        animator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (inZone)
        {
            stopwatch.Start();

            if (!stomping)
            {
                this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15f, 0);
            }

            if (!onGround && stopwatch.ElapsedMilliseconds > 2000f)
            {
                stomping = true;

                transform.Translate(Vector3.down * Time.deltaTime * 7f);

            }

            if (stopwatch.ElapsedMilliseconds > 4000f && stopwatch.ElapsedMilliseconds < 7000f)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 7f);

            }

            if (stopwatch.ElapsedMilliseconds > 7000f)
            {
                stopwatch.Restart();
                onGround = false;
                stomping = false;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            onGround = true;

            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Squish");
            
        }
    }

    public void SetInZone()
    {
        inZone = true;
    }

    public void SetOutZone()
    {
        inZone = false;
    }

    public IEnumerator Squish()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        
        yield return new WaitForSeconds(2.5f);
        inZone = false;
        stopwatch.Reset();
        
        onGround = false;
        stomping = false;
        player.PlayerReset();
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    public void Swing()
    {
        animator.SetBool("Swinging_b", true);
    }

    public void Kick_Player()
    {
        winTitle.gameObject.SetActive(true);
        Internals.winTime++;
        player.Kicked();
    }
}
