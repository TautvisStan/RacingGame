using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour
{
    private GameObject[] Tracks;
    private GameObject[] Cars;
    private Material[] Materials;
    public GameObject EscMenu;
    public GameObject SinglePlayerCanvas;
    public GameObject MultiPlayerCanvas;
    public GameObject GameOverUI;
    [SerializeField] private PlayerPanel[] Panels;
    private CheckpointController checkpointController;


    private Singleton singleton;
    // Start is called before the first frame update
    private void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        Cars = singleton.PassVehicles();
        Tracks = singleton.PassTracks();
        Materials = singleton.PassMaterials();
        
    }


    void Start()
    {
        singleton.StartGame();
    }
    public void SetupGame(int trackID, PlayersKeeper Players, int LapCount)
    {

        MultiPlayerCanvas.SetActive(true);
        GameObject.Find("Top Times:").SetActive(false);
        MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().SetPlayers(Players.ExportPlayers());
        MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().SetMaxLaps(LapCount);
        List<Dictionary<string, KeyCode>> controls = singleton.PassControlsToSetup();
        Instantiate(Tracks[trackID], new Vector3(0, 0, 0), Tracks[trackID].transform.rotation);
        checkpointController = FindObjectOfType<CheckpointController>();
        for (int i = 0; i < Players.GetPlayerCount(); i++)
        {
            if (Players.IsPlayerRacing(i))
            {
                GameObject spawn = GameObject.Find(string.Format("Spawn {0}", i + 1));
                GameObject car = Instantiate(Cars[Players.ReturnCarID(i)], spawn.transform.position, spawn.transform.rotation);
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
                    renderer.material = Materials[materialNum];
                }
            }
            SetCarColor(tr.gameObject, materialNum);
        }
    }
    public void SetPanelColor(PlayerPanel panel, int materialNum)
    {
        panel.PaintPanel(Materials[materialNum]);
    }

}
