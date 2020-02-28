using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CSVWriter //: MonoBehaviour
{
    static string filePath = Application.dataPath + "/kukaPoints.txt";
    // Start is called before the first frame update
    //public StreamWriter writer;

    public void addRecord(string _data)
    {
        try
        {
            StreamWriter writer = new StreamWriter(@filePath, true);
            writer.WriteLine(_data);
            writer.Close();

        }
        catch
        {
            Debug.Log("Unnable to WriteFile!");
        }
    }
    public void createNewFile()
    {

        try
        {
            StreamWriter writer = new StreamWriter(@filePath, false);
            //writer.WriteLine("");
            writer.Close();

        }
        catch
        {
            Debug.Log("Unnable to WriteFile!");
        }
    }
}
