using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;


public class Teste : MonoBehaviour
{
    Client com; //object of costum class Client, responsible for tcp communication

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        com = new Client();
        //tclient = new TcpClient();
        com.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey("c"))
        {
            Debug.Log("closing");
            com.Disconnect();
        }

        if (Input.GetKey("s"))
        {
            Debug.Log("sending");
            com.Send("$OV_PRO", 257); //$POS_ACT
        }
    }
}
