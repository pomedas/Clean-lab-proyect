using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPointer : MonoBehaviour
{
    public Transform pointer;
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            pointer.transform.position, speed * Time.deltaTime);

        //transform.LookAt(pointer);
    }
}
