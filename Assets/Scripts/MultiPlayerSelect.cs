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
    private List<KeyCode> PlayerUpButtons = new List<KeyCode>();
    private List<KeyCode> PlayerDownButtons = new List<KeyCode>();
    private bool[] PlayerNotReady = new bool[4];
    private bool[] PlayerRacing = new bool[4];
    private bool ReadyToStart = false;

    public GameObject TrackSelectMenu;
    public void NextMenu()
    {
        foreach (CarSelect podium in PlayerPodiums)
        {
            podium.DeleteCar();
        }
        TrackSelectMenu.SetActive(true);
        TrackSelectMenu.GetComponent<TrackSelect>().DisplayTrack();
        this.gameObject.SetActive(false);
    }
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
            // Debug.Log("Visi ready");
            NextMenu();
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
            PlayerUpButtons.Add(controls["Forwards"]);
            PlayerDownButtons.Add(controls["Backwards"]);
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
        foreach (KeyCode key in PlayerUpButtons)
        {
            if (Input.GetKeyDown(key))
            {
                {
                    if (PlayerUpButtons.Contains(key))
                    {
                        PlayerPodiums[PlayerUpButtons.IndexOf(key)].ClickedUp();
                    }
                }
            }
        }
        foreach (KeyCode key in PlayerDownButtons)
        {
            if (Input.GetKeyDown(key))
            {
                {
                    if (PlayerDownButtons.Contains(key))
                    {
                        PlayerPodiums[PlayerDownButtons.IndexOf(key)].ClickedDown();
                    }
                }
            }
        }
    }
}
