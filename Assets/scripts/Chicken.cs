using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public float speed_chicken;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(50, 0, 50);
    }

    // Update is called once per frame
    void Update()
    {
        /*random_next_pos = new Vector3(200f * Time.deltaTime, 0, 0);
        while ((random_next_pos.x < 0)or(random_next_pos.x > 100)or(random_next_pos.z < 0)or(random_next_pos.z > 100))
        {

        }*/
        transform.Translate(1.0f * Time.deltaTime, 1.0f * Time.deltaTime, 1.0f * Time.deltaTime);
    }

}
