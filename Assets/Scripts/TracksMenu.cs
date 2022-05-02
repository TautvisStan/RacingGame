using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksMenu : AbstractUnlockTab
{
    private Singleton singleton;
    private UnlockableTrack[] Tracks;
    private GameObject PodiumTrack;
    [SerializeField] private GameObject TrackSpawn;

    private void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        Tracks = singleton.PassUnlockableTracks();
    }

    public override void LeftItem()
    {
        if (ID == 0)
        {
            ID = Tracks.Length - 1;
        }
        else
        {
            ID--;
        }
        SetItemToStage();
    }

    public override void RightItem()
    {
        if (ID == Tracks.Length - 1)
        {
            ID = 0;
        }
        else
        {
            ID++;
        }
        SetItemToStage();
    }

    public override void SetItemToStage()
    {
        Price = Tracks[ID].price;
        Unlocked = Tracks[ID].unlocked;
        Destroy(PodiumTrack);
        PodiumTrack = Instantiate(Tracks[ID].TrackDisplay, TrackSpawn.transform.position, TrackSpawn.transform.rotation, this.transform);
        PodiumTrack.transform.localScale = TrackSpawn.transform.localScale;
        SetLayerRecursively(PodiumTrack, "UI");
    }

    public override void UnlockItem()
    {
        PlayerPrefs.SetInt("Track" + ID, 1);
        Tracks = singleton.PassUnlockableTracks();
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
