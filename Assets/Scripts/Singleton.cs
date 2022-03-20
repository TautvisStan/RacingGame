using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton i;
    public bool GameReady = false;
    public bool[] Players;
    public int[] CarID;
    public int[] MaterialID;
    public int TrackID;
    public bool CarsSelected = false;
    public GameObject[] Vehicles;
    public GameObject[] Tracks;
    public Material[] Materials;

    List<Dictionary<string, KeyCode>> controlsList = new List<Dictionary<string, KeyCode>>();
    public List<Dictionary<string, KeyCode>> PassControlsToSetup()
    {
        return controlsList;
    }
    private void SetDefaultControlsP1()
    {
        controlsList[0].Add("Forwards", KeyCode.W);
        controlsList[0].Add("Backwards", KeyCode.S);
        controlsList[0].Add("Left", KeyCode.A);
        controlsList[0].Add("Right", KeyCode.D);
        controlsList[0].Add("Brake", KeyCode.Space);
        controlsList[0].Add("Reset", KeyCode.R);
        controlsList[0].Add("Confirm", KeyCode.E);
    }
    private void SetDefaultControlsP2()
    {
        controlsList[1].Add("Forwards", KeyCode.UpArrow);
        controlsList[1].Add("Backwards", KeyCode.DownArrow);
        controlsList[1].Add("Left", KeyCode.LeftArrow);
        controlsList[1].Add("Right", KeyCode.RightArrow);
        controlsList[1].Add("Brake", KeyCode.RightControl);
        controlsList[1].Add("Reset", KeyCode.RightShift);
        controlsList[1].Add("Confirm", KeyCode.Keypad0);
    }
    private void SetDefaultControlsP3()
    {
        controlsList[2].Add("Forwards", KeyCode.None);
        controlsList[2].Add("Backwards", KeyCode.None);
        controlsList[2].Add("Left", KeyCode.None);
        controlsList[2].Add("Right", KeyCode.None);
        controlsList[2].Add("Brake", KeyCode.None);
        controlsList[2].Add("Reset", KeyCode.None);
        controlsList[2].Add("Confirm", KeyCode.None);
    }
    private void SetDefaultControlsP4()
    {
        controlsList[3].Add("Forwards", KeyCode.None);
        controlsList[3].Add("Backwards", KeyCode.None);
        controlsList[3].Add("Left", KeyCode.None);
        controlsList[3].Add("Right", KeyCode.None);
        controlsList[3].Add("Brake", KeyCode.None);
        controlsList[3].Add("Reset", KeyCode.None);
        controlsList[3].Add("Confirm", KeyCode.None);
    }
    public Dictionary<string, KeyCode> GetControls(int player)
    {
        return controlsList[player - 1];
    }
    public void SetControls(Dictionary<string, KeyCode> controls, int player)
    {
        controlsList[player - 1] = controls;
    }

    void Awake()
    {
        Players = new bool[4];
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = false;
        }
        Dictionary<string, KeyCode> controlsP1 = new Dictionary<string, KeyCode>();
        Dictionary<string, KeyCode> controlsP2 = new Dictionary<string, KeyCode>();
        Dictionary<string, KeyCode> controlsP3 = new Dictionary<string, KeyCode>();
        Dictionary<string, KeyCode> controlsP4 = new Dictionary<string, KeyCode>();
        controlsList.Add(controlsP1);
        controlsList.Add(controlsP2);
        controlsList.Add(controlsP3);
        controlsList.Add(controlsP4);
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
            // Load controls from file
            SetDefaultControlsP1();
            SetDefaultControlsP2();
            SetDefaultControlsP3();
            SetDefaultControlsP4();
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
            int playersNum = 0;
            int soloPlayer = -1;
            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i])
                {
                    playersNum++;
                    soloPlayer = i;
                }
            }
            if (playersNum == 1)
            {
                setupController.SetupGame(TrackID, CarID, MaterialID, Players, true);
            }
            else
            {
                setupController.SetupGame(TrackID, CarID, MaterialID, Players, false);
            }
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = false;
            }

        }
    }
}
