using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;


public class Teste : MonoBehaviour
{
    Client com;
    TcpClient tclient;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        com = new Client();
        tclient = new TcpClient();
        com.Connect(tclient);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("e"))
        {
            Debug.Log("update");
            
            //Client.Instance.ConnectToServer();
        }

        if (Input.GetKey("c"))
        {
            Debug.Log("closing");
            com.Disconnect();
            //Client.Instance.DiconnectServer();
        }

        if (Input.GetKey("s"))
        {
            Debug.Log("sending");
            com.Send("tyu");
            //Client.Instance.DiconnectServer();
        }
    }
}
