using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TopTimes : MonoBehaviour
{
    public class PlayerLapTime
    {
        public string Name = "None";
        public int TrackID;
        public float Time = float.MaxValue;
    }

    public Text[] NamesText;
    public Text[] TimesText;
    private int TrackID;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateTopTimes();
        }
    }

    public void SetTrackID(int id)
    {
        TrackID = id;
    }

    public void SaveTime(float time)
    {
        PlayerLapTime LapTime = new PlayerLapTime();
        LapTime.Name = PlayerPrefs.GetString("PlayerName", "Player");
        LapTime.TrackID = TrackID;
        LapTime.Time = time;
        string json = JsonUtility.ToJson(LapTime) + "\n";
        File.AppendAllText(Application.dataPath + "/TopTimes.txt", json);
    }

    public void UpdateTopTimes()
    {
        PlayerLapTime[] times = new PlayerLapTime[3];
        for(int i = 0; i < 3; i++)
        {
            times[i] = new PlayerLapTime();
        }
        if (File.Exists(Application.dataPath + "/TopTimes.txt"))
        {
            string[] jsonLines = File.ReadAllLines(Application.dataPath + "/TopTimes.txt");
            foreach(string json in jsonLines)
            {
                PlayerLapTime newTime = new PlayerLapTime();
                JsonUtility.FromJsonOverwrite(json, newTime);
                if(newTime.TrackID == TrackID)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (newTime.Time < times[i].Time)
                        {

                            for (int j = 2; j > i; j--)
                            {
                                times[j] = times[j - 1];
                            }
                            times[i] = newTime;
                            break;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 3; i++)
        {
            NamesText[i].text = times[i].Name;
            TimesText[i].text = floatTimeToString(times[i].Time);
        }
    }

    public string floatTimeToString(float time)
    {
        if(time == float.MaxValue)
        {
            return "--:--:--";
        }
        else
        {
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            int fraction = (int)(time * 100) % 100;
            return String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        }
    }
}
