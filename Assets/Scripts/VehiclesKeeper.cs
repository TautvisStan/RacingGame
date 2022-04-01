using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclesKeeper : MonoBehaviour
{
    [SerializeField] private GameObject[] Vehicles;
    public GameObject[] GetVehicles()
    {
        return Vehicles;
    }
}
