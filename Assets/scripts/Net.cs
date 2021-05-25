using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public Transform red_boat;
    public Transform blue_boat;

    public float slow_down_distance;
    public float max_speed;

    public float max_stretch;
    public float changeLineWidthFrom = 0.8f;

    private Vector3 vertexPosition;
    private bool ripped = false;

    public int strength = 10;
    public int damage = 0;

    public int fishCount;
    public int trashCount;

    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        vertexPosition = red_boat.position + ((blue_boat.position - red_boat.position) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (damage >= strength) Rip();
        
        if (!ripped)
        {
            float boat_distance = Vector3.Distance(blue_boat.position, red_boat.position);
            if (boat_distance > max_stretch) Rip();

            // change line width when boat distances is high
            float stretch = boat_distance / max_stretch;
            if (stretch > changeLineWidthFrom)
            {
                float widthFactor = Mathf.Clamp(((stretch - 1) / (1-changeLineWidthFrom)) * -10, 0, 10);
                lineRenderer.widthMultiplier = widthFactor;
                // TODO change color based on stretch
            }
            

            // calculate position of central curve vertex
            Vector3 boatsMidPoint = red_boat.position + ((blue_boat.position - red_boat.position) / 2);
            Vector3 direction = Vector3.Normalize(boatsMidPoint - vertexPosition);
            float speed = Mathf.Lerp(0, max_speed, Mathf.Clamp01(Vector3.Distance(vertexPosition, boatsMidPoint) / slow_down_distance));
            vertexPosition += direction * Time.deltaTime * speed;

            DrawQuadraticBezierCurve(red_boat.position, vertexPosition, blue_boat.position);
        }
    }

    void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
    {
        lineRenderer.positionCount = 40;
        float t = 0f;
        Vector3 B = new Vector3(0, 0, 0);
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
            lineRenderer.SetPosition(i, B);
            t += (1 / (float)lineRenderer.positionCount);
        }
    }

    // TODO: fish and trash collider + collect methods
    // TODO: island collider + rip
    // TODO: change texture and color based on damage

    void Rip()
    {
        ripped = true;
    }
}
