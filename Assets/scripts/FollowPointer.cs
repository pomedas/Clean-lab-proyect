using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowPointer : MonoBehaviour
{
    public Transform pointer;
    public float slow_down_distance;
    public float max_speed;
    public float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        float speed = Mathf.Lerp(0,max_speed, Mathf.Clamp01(Vector3.Distance(transform.position, pointer.position) / slow_down_distance));
        transform.position += transform.forward*Time.deltaTime*speed;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pointer.position-transform.position)
            ,Time.deltaTime * rotationSpeed);
    }
}
