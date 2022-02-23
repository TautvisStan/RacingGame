using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerTimer : MonoBehaviour
{
    private float startTime;
    string timeText;
    bool started = false;
    public RawImage img3;
    public RawImage img2;
    public RawImage img1;
    public RawImage img0;
    public Text timer;
    public Text best;
    private float guiTime;
    private float bestTime = float.MaxValue;
    public GameObject player;
    private string Name;
    public void StartCountdown()
    {
        StartCoroutine(Countdown(3));
        player.GetComponentInChildren<PlayerController>().enabled = false;
        Name = PlayerPrefs.GetString("PlayerName", "Player");
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > -1)
        {
            if (count == 3)
            {
                img3.enabled = true;
            }
            if (count == 2)
            {
                img3.enabled = false;
                img2.enabled = true;
            }
            if (count == 1)
            {
                img2.enabled = false;
                img1.enabled = true;
            }
            if (count == 0)
            {
                img1.enabled = false;
                img0.enabled = true;
                // count down is finished...
                StartGame();
            }
            yield return new WaitForSeconds(1);
            count--;
        }
        img0.enabled = false;
    }

    void StartGame()
    {
        startTime = Time.time;
        started = true;
        player.GetComponentInChildren<PlayerController>().enabled = true;
    }
    void Update()
    {
        if (started)
        {
            guiTime = Time.time - startTime;

            int minutes = (int)guiTime / 60;
            int seconds = (int)guiTime % 60;
            int fraction = (int)(guiTime * 100) % 100;

            timer.text = String.Format("Lap time {0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        }
    }
    public void LapTime()
    {
        float lapTime = guiTime;
        startTime = Time.time;
        StreamWriter writer = new StreamWriter("TopTimes.txt", true);
        string textToSave = Name + " " + lapTime;
        writer.WriteLine(textToSave);
        writer.Flush();
        writer.Close();
        if (lapTime < bestTime)
        {
            bestTime = lapTime;
            int minutes = (int)bestTime / 60;
            int seconds = (int)bestTime % 60;
            int fraction = (int)(bestTime * 100) % 100;

            string bestText = String.Format("Best time {0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            best.text = bestText;
        }
    }
}
