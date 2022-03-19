using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour
{
    private int CarID = 0;
    public int PlayerNumber;
    private Singleton singleton;
    private GameObject PodiumCar;
    public GameObject CarSpawn;
    private GameObject[] Cars;
    private bool active = false;
    private bool selected = false;
    public MultiPlayerSelect multiPlayerSelect;
    public GameObject PressToJoin;
    public GameObject Podium;
    public GameObject Ready;
    public void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        Cars = singleton.Vehicles;
    }
    public void ClickedConfirm()
    {
        if (!active && !selected)
        {
            PressToJoin.SetActive(true);
            active = true;
            multiPlayerSelect.PlayerSelecting(PlayerNumber, true);
            PressToJoin.SetActive(false);
            Podium.SetActive(true);
            DisplayCar();
        }
        else
        {
            if (!selected)
            {
                selected = true;
                active = false;
                Ready.SetActive(true);
                multiPlayerSelect.PlayerSelecting(PlayerNumber, false);
                multiPlayerSelect.PlayerDone(PlayerNumber, true);
            }
        }
    }
    public void ClickedCancel()
    {
        if (active)
        {
            active = false;
            multiPlayerSelect.PlayerSelecting(PlayerNumber, false);
            PressToJoin.SetActive(true);
            Podium.SetActive(false);
            Destroy(PodiumCar);
            multiPlayerSelect.CheckIfReady();
        }
        else
        {
            if (selected)
            {
                selected = false;
                active = true;
                Ready.SetActive(false);
                multiPlayerSelect.PlayerSelecting(PlayerNumber, true);
                multiPlayerSelect.PlayerDone(PlayerNumber, false);
            }
        }
    }
    public void ClickedLeft()
    {
        if (active)
        {
            Left();
        }
    }
    public void ClickedRight()
    {
        if (active)
        {
            Right();
        }
    }
    public void DisplayCar()
    {
        GameObject.Destroy(PodiumCar);
        PodiumCar = Instantiate(Cars[CarID].transform.Find("Model").gameObject, CarSpawn.transform.position, CarSpawn.transform.rotation);
        SetLayerRecursively(PodiumCar, "UI");
        PodiumCar.GetComponentInChildren<AudioSource>().enabled = false;
         PodiumCar.transform.localScale = CarSpawn.transform.localScale;
        PodiumCar.GetComponent<Rigidbody>().mass = 2500;
    }
    public void Play()
    {
        singleton.CarID[PlayerNumber] = CarID;
        PlayerNumber++;
        if (PlayerNumber == singleton.PlayerCount)
        {
            singleton.CarsSelected = true;
            GameObject.Destroy(PodiumCar);
        }
        else
        {
            CarID = 0;
            DisplayCar();
        }
    }
    public void NextMenu(GameObject TrackSelectMenu)
    {
        if (singleton.CarsSelected)
        {
            this.gameObject.SetActive(false);
            TrackSelectMenu.SetActive(true);
           
        }
    }
    public void NextMenuStart(TrackSelect TrackSelectScript)
    {
        if (singleton.CarsSelected)
        {
            TrackSelectScript.DisplayTrack();
        }
    }
    public void Left()
    {
        if (CarID == 0)
        {
            CarID = Cars.Length - 1;
        }
        else
            CarID--;
        DisplayCar();
    }
    public void Right()
    {
        if (CarID == Cars.Length - 1)
        {
            CarID = 0;
        }
        else
            CarID++;
        DisplayCar();
    }
    public static void SetLayerRecursively(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerName);
        }
    }
}
