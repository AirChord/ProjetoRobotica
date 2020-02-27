using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPEngine;
using System;


public class SplineFollowerImage : MonoBehaviour
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

        
   
            vr.MoveAlongSpline(splineRoot1, gameObject, speed, VR.TRAVEL_MODE_LOOP);

        

    }


    
}
