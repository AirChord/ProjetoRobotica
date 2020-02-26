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
            start = true;
        }

        if (start)
        {
            vr.MoveAlongSpline(splineRoot1, gameObject, speed, VR.TRAVEL_MODE_ONCE);
        }
        

    }

    protected void OnApplicationQuit()
    {
        //conn.Close();
    }

    
}
