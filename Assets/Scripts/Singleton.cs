using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton i;
    public bool GameReady = false;
    public int PlayerCount;
    public int[] CarID;
    public int TrackID;
    public bool CarsSelected = false;

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
            CarsSelected = false;
            SetupController setupController = FindObjectOfType<SetupController>();
            if (PlayerCount == 1)
            {
                 setupController.SetupSinglePlayer(TrackID, CarID[0]);
            }
            else
                setupController.SetupMultiPlayer(TrackID, CarID);

        }
    }
}
