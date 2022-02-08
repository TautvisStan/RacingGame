using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton i;
    public bool GameReady = false;
    public int PlayerCount;

    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void StartGame()
    {

        if (GameReady == true)
        {
            GameReady = false;
            SetupController setupController = FindObjectOfType<SetupController>();
            if (PlayerCount == 1)
            {
                 setupController.SetupSinglePlayer(0, 0);
            }

        }
    }
}
