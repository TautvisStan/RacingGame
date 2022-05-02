using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour
{
    private UnlockableTrack[] Tracks;
    private UnlockableVehicle[] Cars;
    private UnlockableMaterial[] Materials;
    [SerializeField] GameObject EscMenu;
    [SerializeField] GameObject MultiPlayerCanvas;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] private PlayerPanel[] Panels;
    private CheckpointController checkpointController;
    private Singleton singleton;

    private void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        Cars = singleton.PassUnlockableVehicles();
        Tracks = singleton.PassUnlockableTracks();
        Materials = singleton.PassUnlockabeMaterials();
    }

    void Start()
    {
        singleton.StartGame();
    }

    public void SetupGame(int trackID, PlayersKeeper Players, int LapCount)
    {
        if(LapCount == -1)
        {
            GameObject.Find("Top Times:").SetActive(true);
        }
        else
        {
            GameObject.Find("Top Times:").SetActive(false);
        }
        FindObjectOfType<TopTimes>().SetTrackID(trackID);
        MultiPlayerCanvas.SetActive(true);
        MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().SetPlayers(Players.ExportPlayers());
        MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().SetMaxLaps(LapCount);
        List<Dictionary<string, KeyCode>> controls = singleton.PassControlsToSetup();
        Instantiate(Tracks[trackID].Track, new Vector3(0, 0, 0), Tracks[trackID].Track.transform.rotation);
        checkpointController = FindObjectOfType<CheckpointController>();
        for (int i = 0; i < Players.GetPlayerCount(); i++)
        {
            if (Players.IsPlayerRacing(i))
            {
                GameObject spawn = GameObject.Find(string.Format("Spawn {0}", i + 1));
                GameObject car = Instantiate(Cars[Players.ReturnCarID(i)].Vehicle, spawn.transform.position, spawn.transform.rotation);
                if(Players.IsPlayerAI(i))
                {
                    car.GetComponentInChildren<PlayerController>().enabled = false;
                }
                else
                {
                    car.GetComponentInChildren<PlayerController>().SetControls(controls[i]);
                    car.GetComponentInChildren<AIController>().enabled = false;
                }
                car.name = (i + 1).ToString();
                SetCarColor(car, Players.ReturnMaterialID(i));
                Panels[i].gameObject.SetActive(true);
                Panels[i].SetMaxLapsNumber(LapCount);
                Panels[i].SetCPNumber(checkpointController.GetCheckpoints());
                SetPanelColor(Panels[i], Players.ReturnMaterialID(i));
                car.GetComponentInChildren<Player>().SetCar(Panels[i]);
                MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().SetPlayerGameObject(i, car);
            }
                
        }
            MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().StartCountdown();
    }

    public void SetCarColor(GameObject car, int materialNum)
    {
        Transform t = car.transform;
        foreach (Transform tr in t)
        {
            if (t.CompareTag("CarColorable"))
            {
                if (tr.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    renderer.material = Materials[materialNum].material;
                }
            }
            SetCarColor(tr.gameObject, materialNum);
        }
    }

    public void SetPanelColor(PlayerPanel panel, int materialNum)
    {
        panel.PaintPanel(Materials[materialNum].material);
    }
}
