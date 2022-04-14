using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

public class DeepTrackingReceiver
{
	private bool on = true;
	private System.Threading.Thread lThread;

    public float[] positions;
    private float[] floatArray = { 0.0f, 1.0f, 2.0f, 3.0f };
    private float[] data_in;
    private float[] floatsReceived;
    private byte[] bytes;
    private int idxUsedBytes;
    private Socket client_s;

    public void init()
	{
        positions = new float[8];
    }

	public void listen()
	{
		lThread = new System.Threading.Thread(new System.Threading.ThreadStart(listenUDPThread));
		lThread.Name = "Tracking listen thread";
		lThread.Start();
	}

	private void listenUDPThread()
	{
		while (on)
		{
			try
			{
                data_in = SendAndReceive(floatArray);
                positions = data_in;
                Thread.Sleep(16); //16ms = 60 fps
            }
			catch (Exception e)
			{
				Debug.Log(e.ToString());
			}
		}
	}

	public void close()
	{
        Debug.Log("Stop Thread");
		on = false;
		if (lThread != null) lThread.Abort();
	}

	private string ExtractString(byte[] packet, int start, int length)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < packet.Length; i++) sb.Append((char)packet[i]);
		return sb.ToString();
	}

    private float[] SendAndReceive(float[] dataOut)
    {
        //initialize socket

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
        bytes = new byte[4000];
        idxUsedBytes = client_s.Receive(bytes);
        //print(idxUsedBytes + " new bytes received.");

        //convert bytes to floats
        floatsReceived = new float[idxUsedBytes / 4];
        Buffer.BlockCopy(bytes, 0, floatsReceived, 0, idxUsedBytes);

        client_s.Close();
        return floatsReceived;
    }

}
