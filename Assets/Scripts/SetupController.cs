using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour
{
    public GameObject[] Tracks;
    private GameObject[] Cars;
    private Material[] Materials;
    public GameObject EscMenu;
    public GameObject SinglePlayerCanvas;
    public GameObject MultiPlayerCanvas;
    public GameObject GameOverUI;



    private TopTimes topTimess;
    private Singleton singleton;
    // Start is called before the first frame update
    private void Awake()
    {
        topTimess = FindObjectOfType<TopTimes>();
        singleton = FindObjectOfType<Singleton>();
        Cars = singleton.Vehicles;
        Materials = singleton.Materials;
    }


    void Start()
    {
        //Singleton singleton = FindObjectOfType<Singleton>();
        singleton.StartGame();
      //   SetupSinglePlayer(0, 2);
      //  int[] carID = new int[] { 0, 0 };
       // SetupMultiPlayer(0, carID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupGame(int trackID, int[] carID, int[] materialID, bool[] players, bool solo)
    {
        if (solo)
        {
            SinglePlayerCanvas.SetActive(true);
        }
        else
        {
            MultiPlayerCanvas.SetActive(true);
            GameObject.Find("Top Times:").SetActive(false);
            MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().players = new GameObject[carID.Length];
        }
        List<Dictionary<string, KeyCode>> controls = singleton.PassControlsToSetup();
        GameObject track = Instantiate(Tracks[trackID], new Vector3(0, 0, 0), Tracks[trackID].transform.rotation);

        for (int i = 0; i < carID.Length; i++)
        {
            if (players[i])
            {
                GameObject spawn = GameObject.Find(string.Format("Spawn {0}", i + 1));
                GameObject car = Instantiate(Cars[carID[i]], spawn.transform.position, spawn.transform.rotation);
                car.name = (i + 1).ToString();
                SetColor(car, materialID[i]);
                car.GetComponentInChildren<PlayerController>().SetControls(controls[i]);

                if (solo)
                {
                    car.GetComponentInChildren<Player>().SetCar(GameObject.Find("SinglePlayerCanvas/Checkpoint").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Lap").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Powerups").GetComponent<Text>());
                    SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().player = car;
                }
                else
                {
                    car.GetComponentInChildren<Player>().SetCar(GameObject.Find(string.Format("MultiPlayerCanvas/Checkpoint {0}", i + 1)).GetComponent<Text>(), GameObject.Find(string.Format("MultiPlayerCanvas/Lap {0}", i + 1)).GetComponent<Text>(), null);
                    MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().players[i] = car;
                }
                
            }
        }
        if (solo)
        {
            
            SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().StartCountdown();
        }
        else
        {
            MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().StartCountdown();
        }
    }

   /* public void SetupSinglePlayer(int trackID, int carID, int materialID, int playerNum)
    {
       // SinglePlayerCanvas.SetActive(true);
      //  List<Dictionary<string, KeyCode>> controls = singleton.PassControlsToSetup();
      //  GameObject track = Instantiate(Tracks[trackID], new Vector3(0, 0, 0), Tracks[trackID].transform.rotation);
      //  GameObject spawn = GameObject.Find("Spawn 1");
     //   GameObject car = Instantiate(Cars[carID], spawn.transform.position, spawn.transform.rotation);
        
     //   car.GetComponentInChildren<Player>().SetCar(GameObject.Find("SinglePlayerCanvas/Checkpoint").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Lap").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Powerups").GetComponent<Text>());
     //   SetColor(car, materialID);
     //   car.GetComponentInChildren<PlayerController>().SetControls(controls[playerNum]);
        SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().player = car;
        SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().StartCountdown();
    }*/

   /* public void SetupMultiPlayer(int trackID, int[] carID, int[] materialID)
    {
    //    MultiPlayerCanvas.SetActive(true);
   //     List<Dictionary<string, KeyCode>> controls = singleton.PassControlsToSetup();
    //    GameObject.Find("Top Times:").SetActive(false);
  //      GameObject track = Instantiate(Tracks[trackID], new Vector3(0, 0, 0), Tracks[trackID].transform.rotation);
      //  MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().players = new GameObject[carID.Length];
        for (int i = 0; i < carID.Length; i++)
        {
            GameObject spawn = GameObject.Find(string.Format("Spawn {0}", i+1));
            GameObject car = Instantiate(Cars[carID[i]], spawn.transform.position, spawn.transform.rotation);
            car.name = (i+1).ToString();
            car.GetComponentInChildren<Player>().SetCar(GameObject.Find(string.Format("MultiPlayerCanvas/Checkpoint {0}", i + 1)).GetComponent<Text>(), GameObject.Find(string.Format("MultiPlayerCanvas/Lap {0}", i + 1)).GetComponent<Text>(), null);
            SetColor(car, materialID[i]);
            car.GetComponentInChildren<PlayerController>().SetControls(controls[i]);
            MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().players[i] = car;
        }



        MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().StartCountdown();
    }*/
    public void SetColor(GameObject car, int materialNum)
    {
        Transform t = car.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == "CarColorable")
            {
                Renderer renderer = null;
                if (tr.gameObject.TryGetComponent<Renderer>(out renderer))
                {
                    renderer.material = Materials[materialNum];
                }
            }
            SetColor(tr.gameObject, materialNum);
        }
    }

}
