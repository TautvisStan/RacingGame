using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour
{
    public GameObject[] Tracks;
    public GameObject[] Cars;
    public GameObject EscMenu;
    public GameObject SinglePlayerCanvas;
    public GameObject MultiPlayerCanvas;
    public GameObject GameOverUI;



    private TopTimes topTimess;
    // Start is called before the first frame update
    private void Awake()
    {
        topTimess = FindObjectOfType<TopTimes>();
    }


    void Start()
    {
        // SetupSinglePlayer(0, 0);
        int[] carID = new int[] { 0, 0 };
        SetupMultiPlayer(0, carID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupSinglePlayer(int trackID, int carID)
    {
        SinglePlayerCanvas.SetActive(true);
        GameObject track = Instantiate(Tracks[trackID], new Vector3(0, 0, 0), Tracks[trackID].transform.rotation);
        GameObject spawn = GameObject.Find("Spawn 1");
        GameObject car = Instantiate(Cars[carID], spawn.transform.position, spawn.transform.rotation);
        
        car.GetComponent<Player>().SetCar(3, GameObject.Find("SinglePlayerCanvas/Checkpoint").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Lap").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Powerups").GetComponent<Text>());
        SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().player = car;
        SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().StartCountdown();
    }

    public void SetupMultiPlayer(int trackID, int[] carID)
    {
        MultiPlayerCanvas.SetActive(true);
        GameObject.Find("Top Times:").SetActive(false);
        GameObject track = Instantiate(Tracks[trackID], new Vector3(0, 0, 0), Tracks[trackID].transform.rotation);
        MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().players = new GameObject[carID.Length];
        for (int i = 0; i < carID.Length; i++)
        {
            GameObject spawn = GameObject.Find(string.Format("Spawn {0}", i+1));
            GameObject car = Instantiate(Cars[carID[i]], spawn.transform.position, spawn.transform.rotation);
            car.name = (i+1).ToString();
            car.GetComponent<Player>().SetCar(3, GameObject.Find(string.Format("MultiPlayerCanvas/Checkpoint {0}", i + 1)).GetComponent<Text>(), GameObject.Find(string.Format("MultiPlayerCanvas/Lap {0}", i + 1)).GetComponent<Text>(), null);
            MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().players[i] = car;
        }



        MultiPlayerCanvas.GetComponent<MultiPlayerTimer>().StartCountdown();
    }
}
