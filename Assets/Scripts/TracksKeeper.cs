using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksKeeper : MonoBehaviour
{
    [SerializeField] private GameObject[] Tracks;
    [SerializeField] private GameObject[] TracksDisplays;
    public GameObject[] GetTracks()
    {
        return Tracks;
    }
    public GameObject[] GetTracksDisplays()
    {
        return TracksDisplays;
    }
}
