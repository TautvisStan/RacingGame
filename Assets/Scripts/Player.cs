using UnityEngine;

public class Player : MonoBehaviour
{
    public PowerupItem Powerup = null;
    private int lapsCompleted = 0;
    private int checkpoint = 0;
    private PlayerPanel Panel;
    private MultiPlayerTimer timerController;
    private CheckpointController checkpointController;
    private PowerupController powerupController;
    private PlayerController playerController;

    private void Awake()
    {
        timerController = FindObjectOfType<MultiPlayerTimer>();
        checkpointController = FindObjectOfType<CheckpointController>();
        powerupController = FindObjectOfType<PowerupController>();
        playerController = GetComponent<PlayerController>();
    }

    public Transform GetPlayerTransform()
    {
        return playerController.GetTransform();
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
        timerController.LapTime(int.Parse(transform.parent.name), lapsCompleted);
        Panel.UpdateCheckpointText(checkpoint);
    }

    public void RegisterCheckpoint(GameObject cp)
    {
        checkpointController.AddCheckpoint(cp, this, checkpoint);
    }

    public void RegisterPowerup(GameObject powerup)
    {
        if (Powerup == null)
        {
            powerupController.PickupPowerup(powerup, this);
            Panel.SetImage(Powerup.PowerupImage);
        }
    }

    public void ActivatePowerup()
    {
        if (Powerup != null)
        {
            if (Powerup.Activate(this))
            {
                Powerup = null;
                Panel.UnsetImage();
            }
        }
    }
}
