using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TopTimes : MonoBehaviour
{
    public Text[] NamesText;
    public Text[] TimesText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateTopTimes();
        }
    }
    public void UpdateTopTimes()
    {
        StreamReader reader = new StreamReader("TopTimes.txt");
        
        float[] times = new float[3];
        for (int i = 0; i < 3; i++)
            times[i] = float.MaxValue;
        string[] names = new string[3];
        for (int i = 0; i < 3; i++)
            names[i] = "None";
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] split = line.Split(' ');

           float newTime = float.Parse(split[1]);
            for (int j = 0; j < 3; j++)
            {
                if (newTime < times[j])
                {
                    
                    for (int k = 2; k > j; k--)
                    {
                        times[k] = times[k-1];
                        names[k] = names[k-1];
                    }
                    times[j] = newTime;
                    names[j] = split[0];
                    break;
                }
            }

        }
       for (int i = 0; i < 3; i++)
        {
            if (times[i] != float.MaxValue)
            {
                NamesText[i].text = names[i];
                TimesText[i].text = floatTimeToString(times[i]);
            }
        }
        reader.Close();

    }
    public string floatTimeToString(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int fraction = (int)(time * 100) % 100;

        return String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }
}
