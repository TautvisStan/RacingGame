using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int lapsCompleted = 0;
    private int checkpoint = 0;
    private PlayerPanel Panel;
    private SinglePlayerTimer timerController;
    private MultiPlayerTimer mptimerController;
    private CheckpointController checkpointController;
    private void Awake()
    {
        timerController = FindObjectOfType<SinglePlayerTimer>();
        mptimerController = FindObjectOfType<MultiPlayerTimer>();
        checkpointController = FindObjectOfType<CheckpointController>();
    }
    private void StartRacing()
    {
        Panel.UpdateCheckpointText(checkpoint);
        Panel.UpdateLapText(lapsCompleted);
    }
    public void SetCar(PlayerPanel panel)
    {
        Panel = panel;
        StartRacing();
    }
    public void ActivatePanel()
    {
        Panel.StartRacing();
    }
    public void DeactivatePanel()
    {
        Panel.StopRacing();
    }
    public void AddCheckpoint()
    {
        checkpoint += 1;
        Panel.UpdateCheckpointText(checkpoint);
    }
    public void AddLap()
    {
        checkpoint = 0;
        lapsCompleted += 1;
        Panel.UpdateLapText(lapsCompleted);
        Panel.LapTime();
        if (timerController != null)
            timerController.LapTime();
        else
        {
            mptimerController.LapTime(int.Parse(transform.parent.name), lapsCompleted);
        }
        Panel.UpdateCheckpointText(checkpoint);
    }
    public void RegisterCheckpoint(GameObject cp)
    {
        checkpointController.AddCheckpoint(cp, this, checkpoint);
    }
}
