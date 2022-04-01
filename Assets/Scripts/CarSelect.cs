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
    private int materialNum = -1;
    public CarColors carColors;
    public void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
    }
    public void Start()
    {
        Cars = singleton.PassVehicles();
    }
    public void DeleteCar()
    {
        Destroy(PodiumCar);
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
                singleton.SetPlayer(PlayerNumber, CarID, materialNum);
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
            materialNum = carColors.ClearColor(materialNum);
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
                singleton.UnSetPlayer(PlayerNumber);
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
    public void ClickedUp()
    {
        if (active)
        {
            materialNum = carColors.SetColorsNext(PodiumCar, materialNum);
        }
    }
    public void ClickedDown()
    {
        if (active)
        {
            materialNum = carColors.SetColorsPrevious(PodiumCar, materialNum);
        }
    }
    public void DisplayCar()
    {
        GameObject.Destroy(PodiumCar);
        PodiumCar = Instantiate(Cars[CarID].transform.Find("Model").gameObject, CarSpawn.transform.position, CarSpawn.transform.rotation);
        //PodiumCar.transform.SetParent(CarSpawn.transform, true);
        SetLayerRecursively(PodiumCar, "UI");
        PodiumCar.GetComponentInChildren<AudioSource>().enabled = false;
         PodiumCar.transform.localScale = CarSpawn.transform.localScale;
        PodiumCar.GetComponent<Rigidbody>().mass = 2500;
        if (materialNum == -1)
        {
            materialNum = carColors.SetColorsNext(PodiumCar, materialNum);
        }
        else
        {
            carColors.SetColor(PodiumCar, materialNum);
        }
    }
    public void Play()
    {

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
