using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot_Swing : MonoBehaviour
{

    private PlayerCharacter player;
    public bool kick;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(kick == true)
        {
            ;
        }

        
        
    }

    
    

}
