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
            Debug.Log("A mudar RC_P1");
            com.Send("RC_P1", "{E6POS:X 0.0, Y 1.1, Z 2.2, A 3.3, B 4.4, C 5.5, S 6, T 50, E1 0.0, E2 0.0, E3 0.0, E4 0.0, E5 0.0, E6 0.0}", 257); //$POS_ACT

            //Debug.Log("A ler RC_P1");
            //com.Send("XStatus", 112);
        }
    }
}
