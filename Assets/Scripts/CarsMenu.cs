using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsMenu : AbstractUnlockTab
{
    private Singleton singleton;
    private UnlockableVehicle[] vehicles;
    private GameObject PodiumCar;
    [SerializeField] private GameObject CarSpawn;
    [SerializeField] private Material DefaultMaterial;

    public override void LeftItem()
    {
        if (ID == 0)
        {
            ID = vehicles.Length - 1;
        }
        else
        {
            ID--;
        }
        SetItemToStage();
    }

    public override void RightItem()
    {
        if (ID == vehicles.Length - 1)
        {
            ID = 0;
        }
        else
        {
            ID++;
        }
        SetItemToStage();
    }

    private void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        vehicles = singleton.PassUnlockableVehicles();
    }

    public override void SetItemToStage()
    {
        Price = vehicles[ID].price;
        Unlocked = vehicles[ID].unlocked;
            GameObject.Destroy(PodiumCar);
            PodiumCar = Instantiate(vehicles[ID].Vehicle.transform.Find("Model").gameObject, CarSpawn.transform.position, CarSpawn.transform.rotation);
            PodiumCar.transform.SetParent(this.gameObject.transform, true);
            PodiumCar.transform.position = CarSpawn.transform.position;
            SetLayerRecursively(PodiumCar, "UI");
            PodiumCar.GetComponentInChildren<AudioSource>().enabled = false;
            PodiumCar.transform.localScale = CarSpawn.transform.localScale;
            PodiumCar.GetComponent<Rigidbody>().mass = 2500;
        SetColor(PodiumCar, DefaultMaterial);

    }

    public void SetColor(GameObject car, Material material)
    {
        Transform t = car.transform;
        foreach (Transform tr in t)
        {
            if (tr.CompareTag("CarColorable"))
            {
                if (tr.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    renderer.material = material;
                }
            }
            SetColor(tr.gameObject, material);
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

    public override void UnlockItem()
    {
        PlayerPrefs.SetInt("Vehicle" + ID, 1);
        vehicles = singleton.PassUnlockableVehicles();
    }
}
