using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text RedWins;
    public Text BlueWins;
    public Text redBest;
    public Text blueBest;
    public Canvas EndGameCanvas;
    public void EndRace(int winnerNum, float bestTimeRed, float bestTimeBlue)
    {
        EndGameCanvas.enabled = true;
        if (winnerNum == 1)
            BlueWins.enabled = true;
        if (winnerNum == 2)
            RedWins.enabled = true;
        if (bestTimeRed != float.MaxValue)
            redBest.text = floatTimeToString(bestTimeRed);
        if (bestTimeBlue != float.MaxValue)
            blueBest.text = floatTimeToString(bestTimeBlue);
    }
    public string floatTimeToString(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int fraction = (int)(time * 100) % 100;

        return String.Format("Best time {0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
