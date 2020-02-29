using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPEngine;
using System;


public class SplineFollower : MonoBehaviour
{
    
    public GameObject splineRoot1;
    public float speed;
    private bool start = false;
    //tcpSocket conn;
    TrackBuilder builder;

    Client com; //object of costum class Client, responsible for tcp communication

    private VR vr;
    // Use this for initialization
    void Start()
    {
        vr = new VR();
        AStatus.sceneNumber = 3;
        builder = new TrackBuilder();
        builder.BuildTrack();
        com = new Client();
        //tclient = new TcpClient();
        com.Connect();

    }

    



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("d"))
        {
            //TCP.instance.ConnectToServer();
            start = true; 
            com.Send("startFun", "TRUE", 10);
        }

        if (start)
        {
            splineRoot1 = GameObject.Find("BezierSpline");
            speed = 25;
            bool stat= vr.MoveAlongSpline(splineRoot1, gameObject, speed, VR.TRAVEL_MODE_ONCE);
            if (stat == true)
            {
                start = false;
            }
        }
        

    }

    protected void OnApplicationQuit()
    {
        //conn.Close();
        com.Disconnect();
    }

    
}
