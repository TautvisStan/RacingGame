using System;
using UnityEngine;

public class VehiclesKeeper : MonoBehaviour
{
    [SerializeField] private UnlockableVehicle[] Vehicles;

    private void Start()
    {
        UpdateVehicleStatus();
    }

    private void UpdateVehicleStatus()
    {
        for (int i = 0; i < Vehicles.Length; i++)
        {
            int status = PlayerPrefs.GetInt("Vehicle" + i, -1);
            if (status == -1)
            {
                PlayerPrefs.SetInt("Vehicle" + i, Vehicles[i].unlocked ? 1 : 0);
            }
            else
            {
                Vehicles[i].unlocked = Convert.ToBoolean(status);
            }
        }
    }

    public UnlockableVehicle[] GetUnlockableVehicles()
    {
        UpdateVehicleStatus();
        return Vehicles;
    }
}
