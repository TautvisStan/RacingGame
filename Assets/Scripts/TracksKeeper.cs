using System;
using UnityEngine;

public class TracksKeeper : MonoBehaviour
{
    [SerializeField] private UnlockableTrack[] Tracks;
    private void Start()
    {
        UpdateTrackStatus();
    }

    private void UpdateTrackStatus()
    {
        for (int i = 0; i < Tracks.Length; i++)
        {
            int status = PlayerPrefs.GetInt("Track" + i, -1);
            if (status == -1)
            {
                PlayerPrefs.SetInt("Track" + i, Tracks[i].unlocked ? 1 : 0);
            }
            else
            {
                Tracks[i].unlocked = Convert.ToBoolean(status);
            }
        }
    }

    public UnlockableTrack[] GetUnlockableTracks()
    {
        UpdateTrackStatus();
        return Tracks;
    }
}
