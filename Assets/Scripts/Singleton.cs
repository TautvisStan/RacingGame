using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton i;
    public bool GameReady = false;
    public int TrackID;
    public int LapsCount;
    public bool CarsSelected = false;
    [SerializeField] private VehiclesKeeper Vehicles;
    [SerializeField] private TracksKeeper Tracks;
    [SerializeField] private MaterialsKeeper Materials;
    [SerializeField] private PlayersKeeper Players;
    List<Dictionary<string, KeyCode>> controlsList = new List<Dictionary<string, KeyCode>>();
    public void SetPlayer(int PlayerID, int CarID, int MaterialID, bool AI)
    {
        Players.SetPlayerInfo(PlayerID, CarID, MaterialID, AI);
    }
    public void UnSetPlayer(int PlayerID)
    {
        Players.UnSetPlayerInfo(PlayerID);
    }
    public Material[] PassMaterials()
    {
        return Materials.GetMaterials();
    }
    public GameObject[] PassVehicles()
    {
        return Vehicles.GetVehicles();
    }
    public GameObject[] PassTracks()
    {
        return Tracks.GetTracks();
    }
    public GameObject[] PassTracksDisplays()
    {
        return Tracks.GetTracksDisplays();
    }
    
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
    public void SaveControls()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("Forwards" + i + 1, (int)controlsList[i]["Forwards"]);
            PlayerPrefs.SetInt("Backwards" + i + 1, (int)controlsList[i]["Backwards"]);
            PlayerPrefs.SetInt("Left" + i + 1, (int)controlsList[i]["Left"]);
            PlayerPrefs.SetInt("Right" + i + 1, (int)controlsList[i]["Right"]);
            PlayerPrefs.SetInt("Brake" + i + 1, (int)controlsList[i]["Brake"]);
            PlayerPrefs.SetInt("Reset" + i + 1, (int)controlsList[i]["Reset"]);
            PlayerPrefs.SetInt("Confirm" + i + 1, (int)controlsList[i]["Confirm"]);
            PlayerPrefs.SetInt("ControlsSet", 1);
        }
        PlayerPrefs.Save();
    }
    private void PullControls()
    {
        for (int i = 0; i < 4; i++)
        {
            controlsList[i].Add("Forwards", (KeyCode)PlayerPrefs.GetInt("Forwards" + i + 1, 0));
            controlsList[i].Add("Backwards", (KeyCode)PlayerPrefs.GetInt("Backwards" + i + 1, 0));
            controlsList[i].Add("Left", (KeyCode)PlayerPrefs.GetInt("Left" + i + 1, 0));
            controlsList[i].Add("Right", (KeyCode)PlayerPrefs.GetInt("Right" + i + 1, 0));
            controlsList[i].Add("Brake", (KeyCode)PlayerPrefs.GetInt("Brake" + i + 1, 0));
            controlsList[i].Add("Reset", (KeyCode)PlayerPrefs.GetInt("Reset" + i + 1, 0));
            controlsList[i].Add("Confirm", (KeyCode)PlayerPrefs.GetInt("Confirm" + i + 1, 0));
        }
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
            if (PlayerPrefs.GetInt("ControlsSet", 0) == 0)
            {
                SetDefaultControlsP1();
                SetDefaultControlsP2();
                SetDefaultControlsP3();
                SetDefaultControlsP4();
                SaveControls();
            }
            else
            {
                PullControls();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {

        if (GameReady == true)
        {
            GameReady = false;
            CarsSelected = false;
            SetupController setupController = FindObjectOfType<SetupController>();
            setupController.SetupGame(TrackID, Players, LapsCount);
        }
    }
}
