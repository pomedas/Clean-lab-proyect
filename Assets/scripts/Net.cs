using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public Transform red_boat;
    public Transform blue_boat;

    public float slow_down_distance;
    public float max_speed;

    private Vector3 vertexPosition;

    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        vertexPosition = red_boat.position + ((blue_boat.position - red_boat.position) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 boatsMidPoint = red_boat.position + ((blue_boat.position - red_boat.position) / 2);
        Vector3 direction = Vector3.Normalize(boatsMidPoint - vertexPosition);
        float speed = Mathf.Lerp(0, max_speed, Mathf.Clamp01(Vector3.Distance(vertexPosition, boatsMidPoint) / slow_down_distance));
        vertexPosition += direction * Time.deltaTime * speed;

        line.SetPosition(0, red_boat.position);
        line.SetPosition(1, vertexPosition);
        line.SetPosition(2, blue_boat.position);
    }
}
