using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject loadingImage1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartRide(string LevelName)
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void ConfByKukaPressed()
    {
        this.loadingImage1.SetActive(true);
    }
}
