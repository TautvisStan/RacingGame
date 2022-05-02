using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerTimer : MonoBehaviour
{
    [SerializeField] private RawImage img3;
    [SerializeField] private RawImage img2;
    [SerializeField] private RawImage img1;
    [SerializeField] private RawImage img0;
    private PlayerInfo[] players;
    [SerializeField] private int maxLaps;
    [SerializeField] private AudioSource finishAudio;
    [SerializeField] private GameOver GameOverUI;
    [SerializeField] private Canvas gameUI;
    [SerializeField] private PlayerPanel[] Panels;

    public void SetMaxLaps(int LapCount)
    {
        maxLaps = LapCount;
    }

    public void SetPlayers(PlayerInfo[] Players)
    {
        players = Players;
    }

    public void SetPlayerGameObject(int player, GameObject obj)
    {
        players[player].PlayerObject = obj;
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown(3));
        foreach (PlayerInfo player in players)
        {
            if(player.racing)
            {
                if(player.AIRacer)
                {
                    player.PlayerObject.GetComponentInChildren<AIController>().enabled = false;
                }
                else
                {
                    player.PlayerObject.GetComponentInChildren<PlayerController>().enabled = false;
                }
            }
        }
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        while (count > -1)
        {
            switch (count)
            {
                case 3:
                    img3.enabled = true;
                    break;
                case 2:
                    img3.enabled = false;
                    img2.enabled = true;
                    break;
                case 1:
                    img2.enabled = false;
                    img1.enabled = true;
                    break;
                case 0:
                    img1.enabled = false;
                    img0.enabled = true;
                    StartGame();
                    break;
            }
            yield return new WaitForSeconds(1);
            count--;
        }
        img0.enabled = false;
    }

    void StartGame()
    {
        foreach (PlayerInfo player in players)
        {
            if (player.racing)
            {
                if (player.AIRacer)
                {
                    player.PlayerObject.GetComponentInChildren<AIController>().enabled = true;
                }
                else
                {
                    player.PlayerObject.GetComponentInChildren<PlayerController>().enabled = true;
                }
                player.PlayerObject.GetComponentInChildren<Player>().ActivatePanel();
            }
        }
    }

    public void LapTime(int playerNum, int lapsCompleted)
    {
        if (lapsCompleted == maxLaps)
        {
            gameUI.enabled = false;
            foreach (PlayerInfo player in players)
            {
                if (player.racing)
                {
                    if (player.AIRacer)
                    {
                        player.PlayerObject.GetComponentInChildren<AIController>().enabled = false;
                    }
                    else
                    {
                        player.PlayerObject.GetComponentInChildren<PlayerController>().enabled = false;
                    }
                    player.PlayerObject.GetComponentInChildren<Player>().DeactivatePanel();
                    player.PlayerObject.GetComponentInChildren<Player>().enabled = false;
                    player.PlayerObject.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    player.PlayerObject.GetComponentInChildren<VelocityAudio>().stopEngineSound();
                }
            }
            finishAudio.Play();
            GameOverUI.EndRace(playerNum, players, Panels);
        }
    }
}
