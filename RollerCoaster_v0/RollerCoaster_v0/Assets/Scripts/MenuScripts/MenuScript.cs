﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject loadingImage1;
    Client com; //object of costum class Client, responsible for tcp communication
    bool getPoints = false;
    float timer = 1.0f;
    int messID = 0;
    int numPoints = 40;
    CSVWriter writer;


    // Start is called before the first frame update
    void Start()
    {
        //this.loadingImage1.SetActive(false);
        com = new Client();
        //tclient = new TcpClient();
        com.Connect();
        AStatus.sceneNumber = 0;
        
    }

    // Update is called once per frame
    public void StartRide(string LevelName)
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        com.Disconnect();
    }

    public void ConfByKukaPressed()
    {
        this.loadingImage1.SetActive(true);

        //Start new file to save point generated by KUKA
        writer = new CSVWriter();
        writer.createNewFile();
        //writer.addRecord("feefef;egheohge");
        //Send message to KUKA start moving
        com.Send("startFun", "TRUE",messID);


        
        getPoints = true;
        
    }

    void OnApplicationQuit()
    {
        com.Disconnect();
    }

    void Update()
    {
        //Debug.Log(AStatus.sceneNumber);
        
        if (getPoints)
        {
            if (!com._socket.Connected)
            {
                
                getPoints = false;

            }
            else
            {
                //Debug.Log("AQUI");
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    //Debug.Log("AQUI11111");
                    
                    
                    com.Send("$POS_ACT", messID);
                    messID++;
                    timer = 1.0f;
                    if ((numPoints - messID) == 0)
                    {
                        getPoints = false;
                        //com.Disconnect();

                    }
                }
            }
            
        }
        else
        {
            this.loadingImage1.SetActive(false);
        }

    }
}
