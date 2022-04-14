using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net.Sockets;
using System.Net;

public class DeepTracking : MonoBehaviour
{
    public int MarkerID = 0;
    public PlayerDetection playerDetection;
    private float[] playerDetection_pos;

    //translation
    public bool IsPositionMapped = false;
    public bool InvertX = false;
    public bool InvertY = false;


    public float CameraOffset = 10;
    private UdpClient client;
    private Camera m_MainCamera;

    //members
    private Vector2 m_ScreenPosition;
    private Vector3 m_WorldPosition;

    void Awake()
    {

    }

    void Start()
    {
        playerDetection = FindObjectOfType<PlayerDetection>();
    }

    void Update()
    {
        playerDetection_pos = new float[] { playerDetection.positions[2 * MarkerID], playerDetection.positions[2 * MarkerID + 1] };
        this.m_ScreenPosition.x = playerDetection_pos[0];
        this.m_ScreenPosition.y = playerDetection_pos[1];

        //Debug.Log(m_ScreenPosition.x + "," + m_ScreenPosition.y);

        UpdateTransform();
    }

    private void UpdateTransform()
    {
        //position mapping
        if (this.IsPositionMapped)
        {
            //calculate world position with respect to camera view direction
            float xPos = this.m_ScreenPosition.x;
            float yPos = this.m_ScreenPosition.y;

            if (this.InvertX) xPos = 1 - xPos;
            if (this.InvertY) yPos = 1 - yPos;

            this.m_WorldPosition = new Vector3(xPos, 0f, yPos) * 100;
            transform.position = this.m_WorldPosition;
        }

    }

}

