using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] carSounds;
    [SerializeField]
    private AudioSource[] fenceSounds;

    public void PlayCarContactSound()
    {
        int playing = Random.Range(0, carSounds.Length);
        carSounds[playing].Play();
    }

    public void PlayFenceContactSound()
    {
        int playing = Random.Range(0, fenceSounds.Length);
        fenceSounds[playing].Play();
    }
}
