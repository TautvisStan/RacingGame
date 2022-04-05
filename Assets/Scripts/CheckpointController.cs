using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private int CheckpointCount;
    [SerializeField] private AudioSource CPAudio;
    public void AddCheckpoint(GameObject cp, Player player, int checkpoint)
    {
        int value = int.Parse(cp.name);
        if (value == checkpoint + 1)
        {
            if (checkpoint == CheckpointCount - 1)
            {
                player.AddLap();
            }
            else
            {
                player.AddCheckpoint();
            }
            CPAudio.Play();
        }
    }
}
