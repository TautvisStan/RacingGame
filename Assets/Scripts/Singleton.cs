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
    public int RacePoints;
    [SerializeField] private VehiclesKeeper Vehicles;
    [SerializeField] private TracksKeeper Tracks;
    [SerializeField] private MaterialsKeeper Materials;
    [SerializeField] private PlayersKeeper Players;
    [SerializeField] private ControlsKeeper Controls;
    [SerializeField] private PowerupsKeeper Powerups;
    
    public void SetPlayer(int PlayerID, int CarID, int MaterialID, bool AI)
    {
        Players.SetPlayerInfo(PlayerID, CarID, MaterialID, AI);
    }

    public void UnSetPlayer(int PlayerID)
    {
        Players.UnSetPlayerInfo(PlayerID);
    }

    public UnlockableMaterial[] PassUnlockabeMaterials()
    {
        return Materials.GetUnlockableMaterials();
    }

    public UnlockableVehicle[] PassUnlockableVehicles()
    {
        return Vehicles.GetUnlockableVehicles();
    }

    public UnlockableTrack[] PassUnlockableTracks()
    {
        return Tracks.GetUnlockableTracks();
    }

    public UnlockablePowerup[] PassUnlockablePowerups()
    {
        return Powerups.GetUnlockablePowerups();
    }

    public List<Dictionary<string, KeyCode>> PassControlsToSetup()
    {
        return Controls.GetControls();
    }
    
    public Dictionary<string, KeyCode> GetControls(int player)
    {
        return Controls.GetControls(player - 1);
    }

    public void SetControls(Dictionary<string, KeyCode> controls, int player)
    {
        Controls.SetControls(controls, player - 1);
    }

    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
            Controls.SetupControls();
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

    public void RaceEnd()
    {
        int playerPoints = PlayerPrefs.GetInt("Points", 0);
        PlayerPrefs.SetInt("Points", playerPoints + RacePoints);
        RacePoints = 0;
    }
}
