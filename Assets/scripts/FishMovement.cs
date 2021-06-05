using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float reach_threshold = 5;
    public float max_speed;
    public float rotationSpeed;
    public float new_target_period = 3.0f;

    private Vector3 randomTarget;
    private float elapsed_time = 0;

    void Start()
    {
        newRandomPos();
    }

    void Update()
    {
        
        float speed = Mathf.Lerp(0, max_speed, Mathf.Clamp01(Vector3.Distance(transform.position, randomTarget)));
        transform.position += transform.forward * Time.deltaTime * speed;

        Vector3 target = randomTarget;
        // get obstacles from manager 

        // compute target vector

        // compute rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position)
            , Time.deltaTime * rotationSpeed);

        elapsed_time += Time.deltaTime;
        if (elapsed_time > new_target_period || Vector3.Distance(transform.position, randomTarget) < reach_threshold) {
            elapsed_time = 0;
            newRandomPos();
        }


    }

    void newRandomPos() {
        randomTarget = new Vector3(Random.Range(0,100), -1, Random.Range(0,100));
    }


}
