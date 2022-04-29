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
    private GameObject[] Cars;
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
        Cars = singleton.PassVehicles();
        controls = singleton.GetControls(1);
        Podium.SetActive(true);
        DisplayCar();
    }
    private void Update()
    {
        if (Input.GetKeyDown(controls["Confirm"]))
        {
            if (!selected)
            {
                singleton.SetPlayer(0, CarID, materialNum, false);
                selected = true;
                Ready.SetActive(true);
                SelectedText.SetActive(true);
            }
        }
        if (Input.GetKeyDown(controls["Reset"]))
        {
            if (selected)
            {
                selected = false;
                Ready.SetActive(false);
                singleton.UnSetPlayer(0);
                SelectedText.SetActive(false);
            }
        }
        if (Input.GetKeyDown(controls["Left"]))
        {
            if (!selected)
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
        }
        if (Input.GetKeyDown(controls["Right"]))
        {
            if (!selected)
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
        }
        if (Input.GetKeyDown(controls["Forwards"]))
        {
            if (!selected)
            {
                materialNum = carColors.SetColorsNext(PodiumCar, materialNum);
            }
        }
        if (Input.GetKeyDown(controls["Backwards"]))
        {
            if (!selected)
            {
                materialNum = carColors.SetColorsPrevious(PodiumCar, materialNum);
            }
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
    public static void SetLayerRecursively(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerName);
        }
    }
}