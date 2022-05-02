using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    [SerializeField] private AudioSource[] music;

    void Start()
    {
        int playing = Random.Range(0, music.Length);
        music[playing].Play();
    }
}
