using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Foot : MonoBehaviour
{

    private PlayerCharacter player;
    private Stopwatch stopwatch;
    private bool stomping = false;
    private bool onGround = false;
    private bool inZone = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        stopwatch = new Stopwatch();

        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (inZone)
        {
            if (!stomping)
            {
                this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 9.5f, 0);
            }

            if (!onGround && stopwatch.ElapsedMilliseconds > 2000f)
            {
                stomping = true;

                transform.Translate(Vector3.down * Time.deltaTime * 5f);

            }

            if (stopwatch.ElapsedMilliseconds > 5500f && stopwatch.ElapsedMilliseconds < 7000f)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 5f);

            }

            if (stopwatch.ElapsedMilliseconds > 7500f)
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
}
