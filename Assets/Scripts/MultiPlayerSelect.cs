using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerSelect : MonoBehaviour
{
    private Singleton singleton;
    public CarSelect[] PlayerPodiums;
    private List<KeyCode> PlayerConfirmButtons = new List<KeyCode>();
    private List<KeyCode> PlayerCancelButtons = new List<KeyCode>();
    private List<KeyCode> PlayerLeftButtons = new List<KeyCode>();
    private List<KeyCode> PlayerRightButtons = new List<KeyCode>();
    private bool[] PlayerNotReady = new bool[4];
    private bool[] PlayerRacing = new bool[4];
    public void PlayerSelecting(int player, bool state)
    {
        PlayerNotReady[player] = state;
    }
    public void PlayerDone(int player, bool state)
    {
        PlayerRacing[player] = state;
        if (state == true)
        {
            CheckIfReady();
        }
    }
    public void CheckIfReady()
    {
        
        bool allDone = true;
        bool atLeastOne = false;
        for (int i = 0; i < 4; i++)
        {
            if (PlayerNotReady[i] == true)
            {
                allDone = false;
            }
            if (PlayerRacing[i] == true)
            {
                atLeastOne = true;
            }
        }
        if(allDone && atLeastOne)
        {
            Debug.Log("Visi ready");
        }
    }
    private void Start()
    {
        singleton = FindObjectOfType<Singleton>();
        for (int i = 0; i < 4; i++)
        {
            PlayerNotReady[i] = false;
            PlayerRacing[i] = false;
            Dictionary<string, KeyCode> controls = singleton.GetControls(i + 1);
            PlayerConfirmButtons.Add(controls["Confirm"]);
            PlayerCancelButtons.Add(controls["Reset"]);
            PlayerLeftButtons.Add(controls["Left"]);
            PlayerRightButtons.Add(controls["Right"]);
        }

    }
    private void Update()
    {
        foreach (KeyCode key in PlayerConfirmButtons)
        {
            if (Input.GetKeyDown(key))
            {
                {
                    if (PlayerConfirmButtons.Contains(key))
                    {
                        PlayerPodiums[PlayerConfirmButtons.IndexOf(key)].ClickedConfirm();
                    }
                }
            }
        }
        foreach (KeyCode key in PlayerCancelButtons)
        {
            if (Input.GetKeyDown(key))
            {
                {
                    if (PlayerCancelButtons.Contains(key))
                    {
                        PlayerPodiums[PlayerCancelButtons.IndexOf(key)].ClickedCancel();
                    }
                }
            }
        }
        foreach (KeyCode key in PlayerLeftButtons)
        {
            if (Input.GetKeyDown(key))
            {
                {
                    if (PlayerLeftButtons.Contains(key))
                    {
                        PlayerPodiums[PlayerLeftButtons.IndexOf(key)].ClickedLeft();
                    }
                }
            }
        }
        foreach (KeyCode key in PlayerRightButtons)
        {
            if (Input.GetKeyDown(key))
            {
                {
                    if (PlayerRightButtons.Contains(key))
                    {
                        PlayerPodiums[PlayerRightButtons.IndexOf(key)].ClickedRight();
                    }
                }
            }
        }
    }
}
