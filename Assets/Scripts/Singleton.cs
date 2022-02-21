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
    public Dictionary<string, KeyCode> controlsP1 = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> controlsP2 = new Dictionary<string, KeyCode>();

    private void SetDefaultControlsP1()
    {
        controlsP1.Add("Forwards", KeyCode.W);
        controlsP1.Add("Backwards", KeyCode.S);
        controlsP1.Add("Left", KeyCode.A);
        controlsP1.Add("Right", KeyCode.D);
        controlsP1.Add("Break", KeyCode.Space);
        controlsP1.Add("Reset", KeyCode.R);
        controlsP1.Add("Confirm", KeyCode.E);
    }
    private void SetDefaultControlsP2()
    {
        controlsP2.Add("Forwards", KeyCode.UpArrow);
        controlsP2.Add("Backwards", KeyCode.DownArrow);
        controlsP2.Add("Left", KeyCode.LeftArrow);
        controlsP2.Add("Right", KeyCode.RightArrow);
        controlsP2.Add("Break", KeyCode.RightControl);
        controlsP2.Add("Reset", KeyCode.RightShift);
        controlsP2.Add("Confirm", KeyCode.Keypad0);
    }


    public Dictionary<string, KeyCode> GetControls(int player)
    {
        if (player == 1)
        {
            if (controlsP1.Count == 0)
            {
                SetDefaultControlsP1();
            }
            return controlsP1;
        }
        if (player == 2)
        {
            if (controlsP2.Count == 0)
            {
                SetDefaultControlsP2();
            }
            return controlsP2;
        }
        return null;
    }
    public void SetControls(Dictionary<string, KeyCode> controls, int player)
    {
        if (player == 1)
            controlsP1 = controls;
        if (player == 2)
            controlsP2 = controls;
    }

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
