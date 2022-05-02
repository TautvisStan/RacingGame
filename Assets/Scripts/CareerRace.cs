using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CareerRace : MonoBehaviour
{
    private UnlockableTrack[] Tracks;
    private UnlockableMaterial[] Materials;
    private UnlockableVehicle[] Vehicles;
    private Singleton singleton;
    private bool[] MaterialsInUse;

    private void Start()
    {
        singleton = FindObjectOfType<Singleton>();
        Tracks = singleton.PassUnlockableTracks();
        Vehicles = singleton.PassUnlockableVehicles();
        Materials = singleton.PassUnlockabeMaterials();
        MaterialsInUse = new bool[Materials.Length];
        for(int i = 0; i < MaterialsInUse.Length; i++)
        {
            MaterialsInUse[i] = false;
        }
    }

    public void StartCareerRace()
    {
        int Track = GetRandomTrack();
        int Vehicle = GetRandomVehicle();
        int Material = GetRandomMaterial();
        singleton.SetPlayer(0, Vehicle, Material, false);
        singleton.TrackID = Track;
        int Laps = Random.Range(1, 4);
        singleton.LapsCount = Laps;
        int AIs = Random.Range(1, 4);
        for(int i = 0; i < AIs; i++)
        {
            Vehicle = GetRandomVehicle();
            Material = GetRandomMaterial();
            
            singleton.SetPlayer(i+1, Vehicle, Material, true);
        }
        singleton.RacePoints = Laps * AIs * 3;
        singleton.GameReady = true;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int GetRandomMaterial()
    {
        int Material = Random.Range(0, Materials.Length);
        Debug.Log("B" + " " + Material + " " + Materials[Material].unlocked + " " + MaterialsInUse[Material]);
        while (!Materials[Material].unlocked || MaterialsInUse[Material])
        {
            
            Material++;
            if (Material == Materials.Length)
            {
                Material = 0;
            }
        }
        Debug.Log("A" + " " + Material + " " + Materials[Material].unlocked + " " + MaterialsInUse[Material]);
        MaterialsInUse[Material] = true;
        return Material;
    }

    public int GetRandomVehicle()
    {
        int Vehicle = Random.Range(0, Vehicles.Length);
        while (!Vehicles[Vehicle].unlocked)
        {
            Vehicle++;
            if (Vehicle == Vehicles.Length)
            {
                Vehicle = 0;
            }
        }
        return Vehicle;
    }

    public int GetRandomTrack()
    {
        int Track = Random.Range(0, Tracks.Length);
        while (!Tracks[Track].unlocked)
        {
            Track++;
            if (Track == Tracks.Length)
            {
                Track = 0;
            }
        }
        return Track;
    }
}
