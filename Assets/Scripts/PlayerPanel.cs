using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private float StartTime;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI Best;
    [SerializeField] private TextMeshProUGUI CheckpointText;
    [SerializeField] private TextMeshProUGUI LapText;
    [SerializeField] private Image PowerupImage;
    private float GuiTime;
    private float BestTime = float.MaxValue;
    private bool started = false;
    private int maxLapsNumber;
    private int cpNumber;
    private Color PanelColor;
    private bool TrainingRace;
    [SerializeField] private TopTimes topTimes;

    public void SetMaxLapsNumber(int laps)
    {
        if (laps == -1)
        {
            TrainingRace = true;
        }
        maxLapsNumber = laps;
    }

    public void SetCPNumber(int cp)
    {
        cpNumber = cp;
    }

    public void PaintPanel(Material material)
    {
        GetComponent<Image>().color = material.color;
        PanelColor = material.color;
    }

    public void SetImage(Sprite image)
    {
        PowerupImage.enabled = true;
        PowerupImage.sprite = image;
    }

    public void UnsetImage()
    {
        PowerupImage.sprite = null;
        PowerupImage.enabled = false;
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
        StartTime = Time.time;
    }

    public void StopRacing()
    {
        started = false;
    }

    public void UpdateCheckpointText(int cp)
    {
        CheckpointText.text = $"C {cp}/{cpNumber}";
    }

    public void UpdateLapText(int lap)
    {
        LapText.text = $"L {lap}/{maxLapsNumber}";
    }

    public void LapTime()
    {
        float lapTime = GuiTime;
        if (TrainingRace)
        {
            topTimes.SaveTime(lapTime);
        }
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