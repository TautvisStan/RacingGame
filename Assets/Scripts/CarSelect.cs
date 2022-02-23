using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour
{
    private int CarID = 0;
    private int PlayerNumber = 0;
    private Singleton singleton;
    private GameObject PodiumCar;
    public GameObject CarSpawn;
    private GameObject[] Cars;
    public void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        Cars = singleton.Vehicles;
    }
    public void DisplayCar()
    {
        GameObject.Destroy(PodiumCar);
        PodiumCar = Instantiate(Cars[CarID].transform.Find("Model").gameObject, CarSpawn.transform.position, CarSpawn.transform.rotation);
        SetLayerRecursively(PodiumCar, "UI");
        // PodiumCar.transform.localScale = CarSpawn.transform.localScale;
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
