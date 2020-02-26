using UnityEngine;
using SPEngine;
using BezierSolution;
using System.Collections.Generic;
using System.IO;
using System;


public class TrackBuilder : MonoBehaviour
{
    public GameObject splineRoot;

    public GameObject leftRailPrefab;
    public GameObject rightRailPrefab;
    public GameObject crossBeamPrefab;
    public GameObject tree;
    

    public float resolution = 0.005f;

    private VR vr = new VR();

    // We won't initialise VR class in Start because this script will be used in Edit Mode only and not during play mode. So the start method doesnt get called at all.
    void Start()
    {

    }

    public void TreeBuilder()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-500.0f, 500.0f), 0, UnityEngine.Random.Range(-500.0f, 500.0f));
        Instantiate(tree, position, Quaternion.identity);
    }
    public void BuildTrack()
    {
        ////////////////////////////////////////////////
        /////TEste
        ///

        // Check if a BezierSpline already exists
        if (GameObject.Find("BezierSpline") != null)
        {
            Debug.Log("BezierSpline already Exists");
            GameObject splineOld = GameObject.Find("BezierSpline");
            DestroyImmediate(splineOld);
        }
        else
        {
            Debug.Log("BezierSpline dont Exist.\n Creating one");
            
        }

        BezierSpline spline = new GameObject("BezierSpline").AddComponent<BezierSpline>();
        //////////////////////
        //Read file
        /////////////////////
        ///

        List<string> cordenadaX = new List<string>();
        List<string> cordenadaY = new List<string>();
        List<string> cordenadaZ = new List<string>();

        string m_Path = Application.dataPath;
        m_Path = m_Path + "/elipse.csv";
        //Output the Game data path to the console
        Debug.Log("dataPath : " + m_Path);
        using (var reader = new StreamReader(m_Path))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                cordenadaX.Add(values[3]);
                cordenadaY.Add(values[5]);
                cordenadaZ.Add(values[7]);
            }
        }

        //
        Vector3 homePositionTrack;
        homePositionTrack.x = 0;
        homePositionTrack.y = 0;
        homePositionTrack.z = 0;

        float fx_old = 0;
        float fy_old = 0;
        float fz_old = 0;

        for (int i = 0; i < cordenadaX.Count - 1; i++)
        {

            cordenadaX[i] = cordenadaX[i].Replace('.', ',');
            cordenadaY[i] = cordenadaY[i].Replace('.', ',');
            cordenadaZ[i] = cordenadaZ[i].Replace('.', ',');

            float fx = float.Parse(cordenadaX[i]);
            float fy = float.Parse(cordenadaY[i]);
            float fz = float.Parse(cordenadaZ[i]);


            if (i == 0)
            {
                homePositionTrack.x = fx;
                homePositionTrack.y = fy;
                homePositionTrack.z = fz;
                fx = 0;
                fy = 0;
                fz = 0;
            }
            else
            {
                if (Math.Abs(fx - fx_old) > 2)
                {
                    Vector3 pointPosition = new Vector3(homePositionTrack.x - fx, homePositionTrack.y - fy, homePositionTrack.z - fz);
                    spline.InsertNewPointAt2(spline.GetNumPoints(), pointPosition);
                    spline.Refresh();
                    
                }
                //	else
                //		Debug.LogError("bbbbbbbbbbbbbbbbbbbbbbbbb "+ fx);

            }
            fx_old = fx;
            fy_old = fy;
            fz_old = fz;
        }

        spline.RemovePointAt(0);
        spline.RemovePointAt(1);

        spline.AutoConstructSpline();

        
        vr.BuildRollercoasterTrack(gameObject, GameObject.Find("BezierSpline"), leftRailPrefab, rightRailPrefab, crossBeamPrefab, resolution);
    }


}

