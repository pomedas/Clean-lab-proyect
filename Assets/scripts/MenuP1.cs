using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuP1 : MonoBehaviour
{
    [System.NonSerialized]
    public bool is1Inside = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player1")
        {
            is1Inside = true;
            Debug.Log("Player 1 Inside");
        }
    }

    void OnTriggerExit(Collider target)
    {
        if(target.tag == "Player1")
        {
            is1Inside = false;
            Debug.Log("Player 1 Outside");
        }
    }
}
