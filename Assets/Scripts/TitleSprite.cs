using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSprite : MonoBehaviour
{

    public Sprite [] titleSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Internals.winTime < 1)
        {
            GetComponent<SpriteRenderer>().sprite = titleSprites[0];
            
        }
        else {
            GetComponent<SpriteRenderer>().sprite = titleSprites[1];
        }
    }
}
