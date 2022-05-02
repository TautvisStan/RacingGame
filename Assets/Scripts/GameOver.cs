using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject[] BestTimeText;
    [SerializeField] private Text WinnerText;
    [SerializeField] private Canvas EndGameCanvas;
    private Singleton singleton;

    private void Start()
    {
        singleton = FindObjectOfType<Singleton>();
    }

    public void EndRace(int winnerNum, PlayerInfo[] players, PlayerPanel[] Panels)
    {
        WinnerText.text = "PLAYER " + winnerNum + " WINS";
        EndGameCanvas.enabled = true;
        WinnerText.color = Panels[winnerNum-1].GetColor();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].racing)
            {
                BestTimeText[i].SetActive(true);
                if (Panels[i].GetBestTime() != float.MaxValue)
                {
                    BestTimeText[i].GetComponent<Text>().text = FloatTimeToString(Panels[i].GetBestTime());
                }
                BestTimeText[i].GetComponent<Text>().color = Panels[i].GetColor();
            }
        }
        if(winnerNum == 1)
        {
            singleton.RaceEnd();
        }
    }

    public string FloatTimeToString(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int fraction = (int)(time * 100) % 100;
        return string.Format("Best time {0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
