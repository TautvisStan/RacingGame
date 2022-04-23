using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelect : MonoBehaviour
{
    private int CarID = 0;
    [SerializeField] private int PlayerNumber;
    private Singleton singleton;
    private GameObject PodiumCar;
    [SerializeField] private GameObject CarSpawn;
    private GameObject[] Cars;
    private bool active = false;
    private bool selected = false;
    private bool AIPlayerLock = false;
    private bool AIPlayer = false;
    [SerializeField] private MultiPlayerSelect multiPlayerSelect;
    [SerializeField] private GameObject PressToJoin;
    [SerializeField] private GameObject Podium;
    [SerializeField] private GameObject Ready;
    private int materialNum = -1;
    [SerializeField] private CarColors carColors;
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
    public void AddAI()
    {
        AIPlayer = true;
        ClickedConfirm();
        for (int i = 0; i < Random.Range(0, Cars.Length); i++)
        {
            ClickedRight();
        }
        for (int i = 0; i < Random.Range(0, carColors.GetSpareMaterialsNum()); i++)
        {
            ClickedDown();
        }
        ClickedConfirm();
        AIPlayerLock = true;
    }

    public void ClickedConfirm()
    {
        if (!AIPlayerLock)
        {
            if (!active && !selected)
            {
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
                    singleton.SetPlayer(PlayerNumber, CarID, materialNum, AIPlayer);
                    selected = true;
                    active = false;
                    Ready.SetActive(true);
                    multiPlayerSelect.PlayerSelecting(PlayerNumber, false);
                    multiPlayerSelect.PlayerDone(PlayerNumber, true);
                }
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
        PodiumCar.transform.SetParent(this.gameObject.transform, true);
        PodiumCar.transform.position = CarSpawn.transform.position;
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
    public void Left()
    {
        if (CarID == 0)
        {
            CarID = Cars.Length - 1;
        }
        else
        {
            CarID--;
        }
        DisplayCar();
    }
    public void Right()
    {
        if (CarID == Cars.Length - 1)
        {
            CarID = 0;
        }
        else
        {
            CarID++;
        }
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