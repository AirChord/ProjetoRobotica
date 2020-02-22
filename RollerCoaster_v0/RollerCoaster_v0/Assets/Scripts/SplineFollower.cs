using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPEngine;
using System;


public class SplineFollower : MonoBehaviour
{
    
    public GameObject splineRoot1;
    public float speed;

    //tcpSocket conn;

    private VR vr;
    // Use this for initialization
    void Start()
    {
        vr = new VR();
        
    }

    



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("d"))
        {
            //TCP.instance.ConnectToServer();
        }


        //vr.MoveAlongSpline(splineRoot1, gameObject, speed, VR.TRAVEL_MODE_ONCE);
        //bool down = Input.GetKeyDown(KeyCode.Space);
        //if (Input.GetKey ("d"))
        //{
        //    string ip = "192.168.10.123";
        //    int port = 7000;
        //    conn = new tcpSocket();
        //    conn.Start(System.Net.IPAddress.Parse(ip), port);

        //    //conn.Send("Ola");

        //    Debug.Log("D pressed");
        //    //    


        //    // TRAVEL_MODE_LOOP, TRAVEL_MODE_ONCE, TRAVEL_MODE_TO_AND_FRO are all static constants of the class VR. Hence they can be accessed using the class name VR and no need an instance of the VR class to access these.
        //    //vr.MoveAlongSpline(splineRoot1, gameObject, speed, VR.TRAVEL_MODE_ONCE);
        //}
        //if (Input.GetKey("s"))
        //{
        //    conn.Send("Ola");
        //    Debug.Log("S presses");
        //}
        //if (Input.GetKey("e"))
        //{
        //    conn.Close();
        //    Debug.Log("E presses");
        //}

    }

    protected void OnApplicationQuit()
    {
        //conn.Close();
    }

    /*
    public class tcpSocket
    {
        #region Data
        public IPAddress serverIp;
        public int port;
        TcpClient connection;
        byte[] readBuffer = new byte[5000];
        string data;

        TcpListener listener;

        NetworkStream stream
        {
            get
            {
                return connection.GetStream();
            }
        }

        #endregion


        public bool Start(IPAddress address, int _port)
        {
            serverIp = address;
            port = _port;

            connection = new TcpClient();
            try
            {
                connection.BeginConnect(serverIp, port, (ar) => EndConnect(ar), null);
                return (true);
            }
            catch (Exception e)
            {
                Debug.Log("Unnable to connect! Exception=" + e);
                return (false);
            }
        }

        private void EndConnect(IAsyncResult ar)
        {
            connection.EndConnect(ar);

            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }

        private void OnRead(IAsyncResult ar)
        {
            int lenght = stream.EndRead(ar);
            if (lenght <= 0)
            {
                print("Connection lost!!");
                return;
            }
            data = System.Text.Encoding.UTF8.GetString(readBuffer, 0, lenght);

            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);

        }

        public void Send(string message)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);

            stream.Write(buffer, 0, buffer.Length);
        }

        internal void Close()
        {
            connection.Close();
        }
    }

    */
}
