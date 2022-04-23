﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TrackSelect : MonoBehaviour
{
    private int TrackID = 0;
    private Singleton singleton;
    private GameObject PodiumTrack;
    [SerializeField] private GameObject TrackSpawn;
    private GameObject[] Tracks;
    [SerializeField] private TMP_InputField LapsCount;
    [SerializeField] private GameObject ErrorText;
    public void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        Tracks = singleton.PassTracksDisplays();
    }
    public void LapsCountChanged(TMP_InputField input)
    {
        if (input.text.Length > 0 && input.text[0] == '-')
        {
            input.text = input.text.Remove(0, 1);
        }
    }
    public void DisplayTrack()
    {

        Destroy(PodiumTrack);
        PodiumTrack = Instantiate(Tracks[TrackID], TrackSpawn.transform.position, TrackSpawn.transform.rotation);
        PodiumTrack.transform.SetParent(this.gameObject.transform, true);
        PodiumTrack.transform.localScale = TrackSpawn.transform.localScale;
        PodiumTrack.transform.position = TrackSpawn.transform.position;
        SetLayerRecursively(PodiumTrack, "UI");
    }
    public void Play()
    {
        if (LapsCount.text.Length > 0)
        {
            singleton.TrackID = TrackID;
            singleton.LapsCount = int.Parse(LapsCount.text);
            singleton.GameReady = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        else
        {
            ErrorText.SetActive(true);
        }
    }
    public void Left()
    {
        if (TrackID == 0)
        {
            TrackID = Tracks.Length - 1;
        }
        else
            TrackID--;
        DisplayTrack();
    }
    public void Right()
    {
        if (TrackID == Tracks.Length - 1)
        {
            TrackID = 0;
        }
        else
            TrackID++;
        DisplayTrack();
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
