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


    private TopTimes topTimess;
    // Start is called before the first frame update
    private void Awake()
    {
        topTimess = FindObjectOfType<TopTimes>();
    }


    void Start()
    {
        SetupSinglePlayer(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupSinglePlayer(int trackID, int carID)
    {
        GameObject track = Instantiate(Tracks[trackID], new Vector3(0, 0, 0), Tracks[trackID].transform.rotation);
        GameObject spawn = GameObject.Find("Spawn 1");
        GameObject car = Instantiate(Cars[carID], spawn.transform.position, spawn.transform.rotation);
        topTimess.enabled = true;

        SinglePlayerCanvas.SetActive(true);
        Debug.Log(GameObject.Find("SinglePlayerCanvas/Checkpoint").GetComponent<Text>());

        car.GetComponent<Player>().SetCar(3, GameObject.Find("SinglePlayerCanvas/Checkpoint").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Lap").GetComponent<Text>(), GameObject.Find("SinglePlayerCanvas/Powerups").GetComponent<Text>());
        SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().player = car;
        SinglePlayerCanvas.GetComponent<SinglePlayerTimer>().StartCountdown();
    }
}
