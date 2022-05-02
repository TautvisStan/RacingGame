using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelectSingle : MonoBehaviour
{
    private int CarID = 0;
    private Singleton singleton;
    private GameObject PodiumCar;
    [SerializeField] private GameObject CarSpawn;
    private UnlockableVehicle[] Cars;
    private bool selected = false;
    [SerializeField] private GameObject Podium;
    [SerializeField] private GameObject Ready;
    private int materialNum = -1;
    [SerializeField] private CarColors carColors;
    private Dictionary<string, KeyCode> controls;
    [SerializeField] private GameObject SelectedText;

    public void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
    }

    public void Start()
    {
        Cars = singleton.PassUnlockableVehicles();
        controls = singleton.GetControls(1);
        Podium.SetActive(true);
        DisplayCar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(controls[ControlsStrings.Confirm]))
        {
            if (!selected)
            {
                singleton.SetPlayer(0, CarID, materialNum, false);
                selected = true;
                Ready.SetActive(true);
                SelectedText.SetActive(true);
            }
        }
        if (Input.GetKeyDown(controls[ControlsStrings.Reset]))
        {
            if (selected)
            {
                selected = false;
                Ready.SetActive(false);
                singleton.UnSetPlayer(0);
                SelectedText.SetActive(false);
            }
        }
        if (Input.GetKeyDown(controls[ControlsStrings.Left]))
        {
            if (!selected)
            {
                Left();
                DisplayCar();
            }
        }
        if (Input.GetKeyDown(controls[ControlsStrings.Right]))
        {
            if (!selected)
            {
                Right();
                DisplayCar();
            }
        }
        if (Input.GetKeyDown(controls[ControlsStrings.Up]))
        {
            if (!selected)
            {
                materialNum = carColors.SetColorsNext(PodiumCar, materialNum);
            }
        }
        if (Input.GetKeyDown(controls[ControlsStrings.Down]))
        {
            if (!selected)
            {
                materialNum = carColors.SetColorsPrevious(PodiumCar, materialNum);
            }
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
        if (!Cars[CarID].unlocked)
        {
            Left();
        }
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
        if (!Cars[CarID].unlocked)
        {
            Right();
        }
    }

    public void DisplayCar()
    {
        GameObject.Destroy(PodiumCar);
        PodiumCar = Instantiate(Cars[CarID].Vehicle.transform.Find("Model").gameObject, CarSpawn.transform.position, CarSpawn.transform.rotation);
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

    public static void SetLayerRecursively(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerName);
        }
    }
}