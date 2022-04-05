using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    private float StartTime;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Best;
    [SerializeField] private TextMeshProUGUI CheckpointText;
    [SerializeField] private TextMeshProUGUI LapText;
    private float GuiTime;
    private float BestTime = float.MaxValue;
    private bool started = false;
    private Color PanelColor;
    public void Start()
    {
        StartTime = Time.time;
    }
    public void PaintPanel(Material material)
    {
        GetComponent<Image>().color = material.color;
        PanelColor = material.color;
    }
    private void Update()
    {
        if (started)
        {
            GuiTime = Time.time - StartTime;
            int minutes = (int)GuiTime / 60;
            int seconds = (int)GuiTime % 60;
            int fraction = (int)(GuiTime * 100) % 100;
            Timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        }
    }
    public Color GetColor()
    {
        return PanelColor;
    }
    public float GetBestTime()
    {
        return BestTime;
    }
    public void StartRacing()
    {
        started = true;
    }
    public void StopRacing()
    {
        started = false;
    }
    public void UpdateCheckpointText(int cp)
    {
        CheckpointText.text = $"C {cp}";
    }
    public void UpdateLapText(int lap)
    {
        LapText.text = $"L {lap}";
    }
    public void LapTime()
    {
        float lapTime = GuiTime;
        StartTime = Time.time;
        if (lapTime < BestTime)
        {
            BestTime = lapTime;
            int minutes = (int)BestTime / 60;
            int seconds = (int)BestTime % 60;
            int fraction = (int)(BestTime * 100) % 100;
            Best.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        }
    }
}