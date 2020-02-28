using System.Collections;
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
    int numPoints = 50;
    CSVWriter writer;


    // Start is called before the first frame update
    void Start()
    {
        //this.loadingImage1.SetActive(false);
        com = new Client();
        //tclient = new TcpClient();
        com.Connect();
        
    }

    // Update is called once per frame
    public void StartRide(string LevelName)
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void ConfByKukaPressed()
    {
        this.loadingImage1.SetActive(true);


        writer = new CSVWriter();
        writer.createNewFile();
        //writer.addRecord("feefef;egheohge");
        com.Send("startFun", "TRUE",messID);


        AStatus.sceneNumber = 1;
        getPoints = true;
        

    }

    void OnApplicationQuit()
    {
        com.Disconnect();
    }

    void Update()
    {
        if (getPoints)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                com.Send("$POS_ACT", messID);
                messID++;
                timer = 1.0f;
                if ((numPoints - messID) == 0)
                {
                    getPoints = false;
                    com.Disconnect();
                    this.loadingImage1.SetActive(false);
                }
            }
        }

    }
}
