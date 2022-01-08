using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerTimer : MonoBehaviour
{
    private float startTime1;
    private float startTime2;
    string timeText;
    bool started = false;
    public RawImage img3;
    public RawImage img2;
    public RawImage img1;
    public RawImage img0;
    public Text timer1;
    public Text timer2;
    public Text best1;
    public Text best2;
    private float guiTime1;
    private float guiTime2;
    private float bestTime1 = float.MaxValue;
    private float bestTime2 = float.MaxValue;
    public GameObject[] players;
    public int maxLaps;
    public AudioSource finishAudio;
    public GameObject GameOverUI;
    public Canvas gameUI;
    void Awake()
    {
        StartCoroutine(Countdown(3));
        foreach (GameObject player in players)
            player.GetComponent<PlayerController>().enabled = false;
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
        startTime1 = Time.time;
        startTime2 = Time.time;
        started = true;
        foreach (GameObject player in players)
            player.GetComponent<PlayerController>().enabled = true;
    }
    void Update()
    {
        if (started)
        {
            guiTime1 = Time.time - startTime1;
            guiTime2 = Time.time - startTime2;

            int minutes1 = (int)guiTime1 / 60;
            int seconds1 = (int)guiTime1 % 60;
            int fraction1 = (int)(guiTime1 * 100) % 100;

            timer1.text = String.Format("Lap time {0:00}:{1:00}:{2:00}", minutes1, seconds1, fraction1);

            int minutes2 = (int)guiTime2 / 60;
            int seconds2 = (int)guiTime2 % 60;
            int fraction2 = (int)(guiTime2 * 100) % 100;

            timer2.text = String.Format("Lap time {0:00}:{1:00}:{2:00}", minutes2, seconds2, fraction2);
        }
    }
    public void LapTime(int playerNum, int lapsCompleted)
    {
        if (playerNum == 1)
        {
            float lapTime = guiTime1;
            startTime1 = Time.time;
            if (lapTime < bestTime1)
            {
                bestTime1 = lapTime;
                int minutes = (int)bestTime1 / 60;
                int seconds = (int)bestTime1 % 60;
                int fraction = (int)(bestTime1 * 100) % 100;

                best1.text = String.Format("Best time {0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            }
        }

        if (playerNum == 2)
        {
            float lapTime = guiTime2;
            startTime2 = Time.time;
            if (lapTime < bestTime2)
            {
                bestTime2 = lapTime;
                int minutes = (int)bestTime2 / 60;
                int seconds = (int)bestTime2 % 60;
                int fraction = (int)(bestTime2 * 100) % 100;

                best2.text = String.Format("Best time {0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            }
        }
        if (lapsCompleted == maxLaps)
        {
            started = false;
            gameUI.enabled = false;
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<Player>().enabled = false;
                player.GetComponent<Rigidbody>().isKinematic = true;
                player.GetComponent<VelocityAudio>().stopEngineSound();
            }
            finishAudio.Play();
            GameOverUI.GetComponent<GameOver>().EndRace(playerNum, bestTime2, bestTime1);
        }

    }
}
