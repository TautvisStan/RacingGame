using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider Effects;
    [SerializeField] private Slider Music;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private InputField playerName;

    private void Start()
    {
        Effects.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        Music.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        playerName.text = PlayerPrefs.GetString("PlayerName", "Player");
    }

    public void SetEffectsVolume(Slider slider)
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", slider.value);
    }

    public void SetMusicVolume(Slider slider)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider.value) * 20);
        audioMixer.SetFloat("BassVolume", Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
        PlayerPrefs.SetFloat("BassVolume", slider.value);
    }

    public void UpdatePlayerName(InputField input)
    {
        PlayerPrefs.SetString("PlayerName", input.text);
    }
}
