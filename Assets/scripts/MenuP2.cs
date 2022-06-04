using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuP2 : MonoBehaviour
{
        [System.NonSerialized]
    public bool is2Inside = false;
    
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
        if(target.tag == "Player2")
        {
            is2Inside = true;
            Debug.Log("Player 2 Inside");
        }
    }

    void OnTriggerExit(Collider target)
    {
        if(target.tag == "Player2")
        {
            is2Inside = false;
            Debug.Log("Player 2 Outside");
        }
    }
}
