using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackSelect : MonoBehaviour
{
    private int TrackID = 0;
    private Singleton singleton;
    private GameObject PodiumTrack;
    public GameObject TrackSpawn;
    public GameObject[] Tracks;
    public void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
    }
    public void DisplayTrack()
    {
        GameObject.Destroy(PodiumTrack);
        PodiumTrack = Instantiate(Tracks[TrackID], TrackSpawn.transform.position, TrackSpawn.transform.rotation);
         PodiumTrack.transform.localScale = TrackSpawn.transform.localScale;
    }
    public void Play()
    {
        singleton.TrackID = TrackID;
        singleton.GameReady = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
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
}
