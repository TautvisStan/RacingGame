using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerSelect : MonoBehaviour
{
    private Singleton singleton;
    [SerializeField] private CarSelect[] PlayerPodiums;
    private readonly List<KeyCode> PlayerConfirmButtons = new List<KeyCode>();
    private readonly List<KeyCode> PlayerCancelButtons = new List<KeyCode>();
    private readonly List<KeyCode> PlayerLeftButtons = new List<KeyCode>();
    private readonly List<KeyCode> PlayerRightButtons = new List<KeyCode>();
    private readonly List<KeyCode> PlayerUpButtons = new List<KeyCode>();
    private readonly List<KeyCode> PlayerDownButtons = new List<KeyCode>();
    private readonly bool[] PlayerNotReady = new bool[4];
    private readonly bool[] PlayerRacing = new bool[4];
    [SerializeField] private GameObject Controls;
    [SerializeField] private GameObject NextWindowButton;
    [SerializeField] private GameObject TrackSelectMenu;

    public void NextMenu()
    {
        foreach (CarSelect podium in PlayerPodiums)
        {
            podium.DeleteCar();
        }
        TrackSelectMenu.SetActive(true);
        TrackSelectMenu.GetComponent<TrackSelect>().DisplayTrack();
        gameObject.SetActive(false);
    }

    public void PlayerSelecting(int player, bool state)
    {
        PlayerNotReady[player] = state;
        Controls.SetActive(true);
        NextWindowButton.SetActive(false);
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
        if (allDone && atLeastOne)
        {
            Controls.SetActive(false);
            NextWindowButton.SetActive(true);
        }
        else
        {
            Controls.SetActive(true);
            NextWindowButton.SetActive(false);
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
            PlayerConfirmButtons.Add(controls[ControlsStrings.Confirm]);
            PlayerCancelButtons.Add(controls[ControlsStrings.Reset]);
            PlayerLeftButtons.Add(controls[ControlsStrings.Left]);
            PlayerRightButtons.Add(controls[ControlsStrings.Right]);
            PlayerUpButtons.Add(controls[ControlsStrings.Up]);
            PlayerDownButtons.Add(controls[ControlsStrings.Down]);
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
