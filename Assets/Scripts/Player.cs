using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int lapsCompleted = 0;

    private int checkpoint = 0;

    private int powerupsPicked = 0;
    

    [Header("UI")] 
    [SerializeField] 
    private Text CheckPointText;
    
    [SerializeField] 
    private Text LapText;
    
    [SerializeField] 
    private Text PowerUpsText;

    private PlayerController playerController;
    private PowerupController powerupController;
    private SinglePlayerTimer timerController;
    private MultiPlayerTimer mptimerController;
    private CheckpointController checkpointController;

    private SoundEffects soundEffectsController;

   // public AudioSource CPAudio;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        powerupController = FindObjectOfType<PowerupController>();
        timerController = FindObjectOfType<SinglePlayerTimer>();
        mptimerController = FindObjectOfType<MultiPlayerTimer>();
        soundEffectsController = FindObjectOfType<SoundEffects>();
        checkpointController = FindObjectOfType<CheckpointController>();
        //CPAudio = GameObject.Find("CPAudio").GetComponent<AudioSource>();
    }
      private void StartRacing()
      {
          UpdateCheckpointText();
          UpdateLapText();
        //  UpdatePowerupText();
      }

        public void SetCar(Text cp, Text lap, Text Powerup)
        {
            CheckPointText = cp;
            LapText = lap;
            PowerUpsText = Powerup;
            this.StartRacing();
        }
  
      private void UpdateCheckpointText()
      {
          CheckPointText.text = $"Checkpoint {checkpoint}";
      }
      private void UpdateLapText()
      {
          LapText.text = $"Lap {lapsCompleted}";
      }
      private void UpdatePowerupText()
      {
          PowerUpsText.text = $"Powerups collected: {powerupsPicked}";
      }

      private void AddPowerup(int value)
      {
          powerupsPicked += value;
          UpdatePowerupText();
      }
    public void AddCheckpoint()
    {
        checkpoint += 1;
        UpdateCheckpointText();
    }
    public void AddLap()
    {
        checkpoint = 0;
        lapsCompleted += 1;
        UpdateLapText();
        if (timerController != null)
            timerController.LapTime();
        else
            mptimerController.LapTime(int.Parse(transform.parent.name), lapsCompleted);
        UpdateCheckpointText();
    }

    public void RegisterCheckpoint(GameObject cp)
    {
        var coord = cp.gameObject.transform.position;
        checkpointController.AddCheckpoint(cp.gameObject, this, checkpoint);
    }
    /*  void OnTriggerEnter(Collider collision)
      {
        Debug.Log("CPPP");
          if (collision.CompareTag("Checkpoint"))
          {
            var coord = collision.gameObject.transform.position;
            checkpointController.AddCheckpoint(collision.gameObject, this, checkpoint);
            
            
          }
          
          if (collision.CompareTag("Powerup"))
          {
              AddPowerup(1);
              Debug.Log("Player touched the powerup");
            powerupController.ActivatePowerup(collision.gameObject);
              
          }
      }
    */

  /*  void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("FunCube"))
        {
            Debug.Log("YEET");
            collision.rigidbody.velocity = new Vector3(0, 10, 0);
        }

        if (collision.collider.CompareTag("Fence"))
        {
            soundEffectsController.PlayFenceContactSound();
        }
        if (collision.collider.CompareTag("Player"))
        {
            soundEffectsController.PlayCarContactSound();
        }

    }*/
}
