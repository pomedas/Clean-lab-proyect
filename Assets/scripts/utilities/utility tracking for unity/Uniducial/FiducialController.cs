/*
Copyright (c) 2012 André Gröschel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net.Sockets;
using System.Net;

public class FiducialController : MonoBehaviour
{
    public float keysVel = 0.05f;
    public int MarkerID = 0;

    public PlayerDetection playerDetection;
    private float[] playerDetection_pos;

    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };

    //translation
    public bool IsPositionMapped = false;
    public bool InvertX = false;
    public bool InvertY = false;

    //rotation
    public bool IsRotationMapped = false;
    public bool AutoHideGO = false;
    private bool m_ControlsGUIElement = false;


    public float CameraOffset = 10;
    public RotationAxis RotateAround = RotationAxis.Back;
    private UniducialLibrary.TuioManager m_TuioManager;
    private UdpClient client;
    private Camera m_MainCamera;

    //members
    private Vector2 m_ScreenPosition;
    private Vector3 m_WorldPosition;
    private Vector2 m_Direction;
    private float m_Angle;
    private float m_AngleDegrees;
    private float m_Speed;
    private float m_Acceleration;
    private float m_RotationSpeed;
    private float m_RotationAcceleration;
    private bool m_IsVisible;

    public float RotationMultiplier = 1;
    private Socket client_s;


    List<Vector3> places;


    private float[] SendAndReceive(float[] dataOut)
    {
        //initialize socket
        float[] floatsReceived;
        client_s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client_s.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 4444);
        if (!client_s.Connected)
        {
            Debug.LogError("Connection Failed");
            return null;
        }

        //convert floats to bytes, send to port
        var byteArray = new byte[dataOut.Length * 4];
        Buffer.BlockCopy(dataOut, 0, byteArray, 0, byteArray.Length);
        client_s.Send(byteArray);

        //allocate and receive bytes
        byte[] bytes = new byte[4000];
        int idxUsedBytes = client_s.Receive(bytes);
        //print(idxUsedBytes + " new bytes received.");

        //convert bytes to floats
        floatsReceived = new float[idxUsedBytes / 4];
        Buffer.BlockCopy(bytes, 0, floatsReceived, 0, idxUsedBytes);

        client_s.Close();
        return floatsReceived;
    }
    void Awake()
    {
        places = new List<Vector3>();
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;

        //uncomment next line to set port explicitly (default is 3333)
        this.m_TuioManager.TuioPort = 7777;

        this.m_TuioManager.Connect();

        this.client = new UdpClient();
        //IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3333); // endpoint where server is listening
        //client.Connect(ep);

        //check if the game object needs to be transformed in normalized 2d space
        if (isAttachedToGUIComponent())
        {
            Debug.LogWarning("Rotation of GUIText or GUITexture is not supported. Use a plane with a texture instead.");
            this.m_ControlsGUIElement = true;
        }

        this.m_ScreenPosition = Vector2.zero;
        this.m_WorldPosition = Vector3.zero;
        this.m_Direction = Vector2.zero;
        this.m_Angle = 0f;
        this.m_AngleDegrees = 0;
        this.m_Speed = 0f;
        this.m_Acceleration = 0f;
        this.m_RotationSpeed = 0f;
        this.m_RotationAcceleration = 0f;
        this.m_IsVisible = true;
    }

    void Start()
    {
        //get reference to main camera
        this.m_MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //check if the main camera exists
        if (this.m_MainCamera == null)
        {
            Debug.LogError("There is no main camera defined in your scene.");
        }

        playerDetection = FindObjectOfType<PlayerDetection>();
        //    Debug.Log(playerDetection.positions[0]);
    }

    void Update()
    {
        //Debug.Log(playerDetection.positions[0]);
        /*  if (this.m_TuioManager.IsConnected){
              Debug.Log("Tuio connected");
          }else{
              Debug.Log("tuio not connected");
          }*/
        /*  if (this.m_TuioManager.IsMarkerAlive(this.MarkerID)){
              Debug.Log("Marker found");
          }else{
              Debug.Log("marker not found");
          }   */

        //var remoteEP = new IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 3333);
        //var data = this.client.Receive(ref remoteEP);
        //float[] data_in;
        float[] floatArray = { (float)this.MarkerID };

        // data_in = SendAndReceive(floatArray);

        playerDetection_pos = new float[] { playerDetection.positions[2 * MarkerID], playerDetection.positions[2 * MarkerID + 1] };

        this.m_ScreenPosition.x = playerDetection_pos[0];
        this.m_ScreenPosition.y = playerDetection_pos[1];

        //Debug.Log("Player " + MarkerID.ToString() + " x: " + playerDetection_pos[0].ToString());

        UpdateTransform();
        //Debug.Log(data_in[0].ToString());
        /*if(MarkerID == (int)data_in[0]){
             this.m_ScreenPosition.x = data_in[1];
             this.m_ScreenPosition.y = data_in[2];
             UpdateTransform();
         }*/


        if (this.m_TuioManager.IsConnected
            && this.m_TuioManager.IsMarkerAlive(this.MarkerID))
        {
            TUIO.TuioObject marker = this.m_TuioManager.GetMarker(this.MarkerID);

            //update parameters
            this.m_ScreenPosition.x = marker.getX();
            this.m_ScreenPosition.y = marker.getY();
            this.m_Angle = marker.getAngle() * RotationMultiplier;
            this.m_AngleDegrees = marker.getAngleDegrees() * RotationMultiplier;
            this.m_Speed = marker.getMotionSpeed();
            this.m_Acceleration = marker.getMotionAccel();
            this.m_RotationSpeed = marker.getRotationSpeed() * RotationMultiplier;
            this.m_RotationAcceleration = marker.getRotationAccel();
            this.m_Direction.x = marker.getXSpeed();
            this.m_Direction.y = marker.getYSpeed();
            this.m_IsVisible = true;

            //set game object to visible, if it was hidden before
            ShowGameObject();

            //update transform component
            UpdateTransform();
        }
        else
        {
            //automatically hide game object when marker is not visible
            if (this.AutoHideGO)
            {
                HideGameObject();
            }
            else
            {
                //activate markers
                transform.GetChild(0).gameObject.SetActive(true);
                //keyboard commands to test
                if (MarkerID == 0)
                {
                    // Debug.Log(transform.position);

                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        transform.position += Vector3.forward * keysVel;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        transform.position += Vector3.forward * -keysVel;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        transform.position += Vector3.right * keysVel;
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        transform.position += Vector3.right * -keysVel;
                    }
                }

                if (MarkerID == 1)
                {
                    if (Input.GetKey(KeyCode.Keypad8))
                    {
                        transform.position += Vector3.forward * keysVel;
                    }
                    if (Input.GetKey(KeyCode.Keypad2))
                    {
                        transform.position += Vector3.forward * -keysVel;
                    }
                    if (Input.GetKey(KeyCode.Keypad6))
                    {
                        transform.position += Vector3.right * keysVel;
                    }
                    if (Input.GetKey(KeyCode.Keypad4))
                    {
                        transform.position += Vector3.right * -keysVel;
                    }
                }
                if (MarkerID == 2)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.position += Vector3.forward * keysVel;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        transform.position += Vector3.forward * -keysVel;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.position += Vector3.right * keysVel;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.position += Vector3.right * -keysVel;
                    }
                }
                if (MarkerID == 3)
                {
                    if (Input.GetKey(KeyCode.I))
                    {
                        transform.position += Vector3.forward * keysVel;
                    }
                    if (Input.GetKey(KeyCode.K))
                    {
                        transform.position += Vector3.forward * -keysVel;
                    }
                    if (Input.GetKey(KeyCode.L))
                    {
                        transform.position += Vector3.right * keysVel;
                    }
                    if (Input.GetKey(KeyCode.J))
                    {
                        transform.position += Vector3.right * -keysVel;
                    }
                }
            }

            this.m_IsVisible = false;
        }
    }


    void OnApplicationQuit()
    {
        if (this.m_TuioManager.IsConnected)
        {
            this.m_TuioManager.Disconnect();
        }
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

            // Debug.Log("test");

            if (this.m_ControlsGUIElement)
            {
                transform.position = new Vector3(xPos, 1 - yPos, 0);
            }
            else
            {
                Debug.Log("here");
                this.m_WorldPosition = new Vector3(xPos, 0f, yPos) * 100;
                /*if (places.Count > 3)
                {
                    places.RemoveAt(0);
                }
                places.Add(this.m_WorldPosition);
                transform.position = computemedian();
                */

                transform.position = this.m_WorldPosition;
                //Logger.AddUserPosition(gameObject.name, m_WorldPosition);
            }
        }


        //rotation mapping
        if (this.IsRotationMapped)
        {
            Quaternion rotation = Quaternion.identity;

            switch (this.RotateAround)
            {
                case RotationAxis.Forward:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.forward);
                    break;
                case RotationAxis.Back:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.back);
                    break;
                case RotationAxis.Up:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.up);
                    break;
                case RotationAxis.Down:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.down);
                    break;
                case RotationAxis.Left:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.left);
                    break;
                case RotationAxis.Right:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.right);
                    break;
            }
            transform.localRotation = rotation;
        }
    }

    private Vector3 computemedian()
    {
        Vector3 newpos = Vector3.zero;
        foreach (Vector3 v in places)
        {
            newpos += v;
        }
        newpos = newpos / places.Count;
        return newpos;
    }

    private void ShowGameObject()
    {
        if (this.m_ControlsGUIElement)
        {
            //show GUI components
            if (gameObject.GetComponent<Text>() != null && !gameObject.GetComponent<Text>().enabled)
            {
                gameObject.GetComponent<Text>().enabled = true;
            }
            if (gameObject.GetComponent<Text>() != null && !gameObject.GetComponent<Text>().enabled)
            {
                gameObject.GetComponent<Text>().enabled = true;
            }
        }
        else
        {
            if (gameObject.GetComponent<Renderer>() != null && !gameObject.GetComponent<Renderer>().enabled)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    private void HideGameObject()
    {
        if (this.m_ControlsGUIElement)
        {
            //hide GUI components
            if (gameObject.GetComponent<Text>() != null && gameObject.GetComponent<Text>().enabled)
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
            if (gameObject.GetComponent<Text>() != null && gameObject.GetComponent<Text>().enabled)
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
        else
        {
            //set 3d game object to visible, if it was hidden before
            if (gameObject.GetComponent<Renderer>() != null && gameObject.GetComponent<Renderer>().enabled)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    #region Getter

    public bool isAttachedToGUIComponent()
    {
        return (gameObject.GetComponent<Text>() != null || gameObject.GetComponent<Text>() != null);
    }
    public Vector2 ScreenPosition
    {
        get { return this.m_ScreenPosition; }
    }
    public Vector3 WorldPosition
    {
        get { return this.m_WorldPosition; }
    }
    public Vector2 MovementDirection
    {
        get { return this.m_Direction; }
    }
    public float Angle
    {
        get { return this.m_Angle; }
    }
    public float AngleDegrees
    {
        get { return this.m_AngleDegrees; }
    }
    public float Speed
    {
        get { return this.m_Speed; }
    }
    public float Acceleration
    {
        get { return this.m_Acceleration; }
    }
    public float RotationSpeed
    {
        get { return this.m_RotationSpeed; }
    }
    public float RotationAcceleration
    {
        get { return this.m_RotationAcceleration; }
    }
    public bool IsVisible
    {
        get { return this.m_IsVisible; }
    }
    #endregion
}

