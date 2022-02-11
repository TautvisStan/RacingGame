using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private void Start()
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("EffectsVolume", 0.75f)) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 0.75f)) * 20);
    }
    public void PlaySP()
    {
        Singleton singleton = FindObjectOfType<Singleton>();
        singleton.PlayerCount = 1;
        singleton.CarID = new int[1];
    }
    public void PlayMP()
    {
        Singleton singleton = FindObjectOfType<Singleton>();
        singleton.PlayerCount = 2;
        singleton.CarID = new int[2];
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
