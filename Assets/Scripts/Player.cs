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
    
    [Header("Checkpoints in the track")] 
    [SerializeField] 
    private int Cps;

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

    private SoundEffects soundEffectsController;

    public AudioSource CPAudio;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        powerupController = FindObjectOfType<PowerupController>();
        timerController = FindObjectOfType<SinglePlayerTimer>();
        mptimerController = FindObjectOfType<MultiPlayerTimer>();
        soundEffectsController = FindObjectOfType<SoundEffects>();
        //CPAudio = GameObject.Find("CPAudio").GetComponent<AudioSource>();
    }
      private void StartRacing()
      {
          UpdateCheckpointText();
          UpdateLapText();
        //  UpdatePowerupText();
      }

        public void SetCar(int cps, Text cp, Text lap, Text Powerup)
        {
            Cps = cps;
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
      private void AddCheckpoint(int value, Vector3 coord)
      {
        if (value == checkpoint + 1)
          {
            
            checkpoint += 1;
              if (checkpoint == Cps)
              {
                  checkpoint = 0;
                  lapsCompleted += 1;
                  UpdateLapText();
                if (timerController != null)
                    timerController.LapTime();
                else
                    mptimerController.LapTime(Int32.Parse(this.name), lapsCompleted);
              }
            //CPAudio.Play();
            UpdateCheckpointText();
          }
      }
      private void AddPowerup(int value)
      {
          powerupsPicked += value;
          UpdatePowerupText();
      }
      void OnTriggerEnter(Collider collision)
      {
          if (collision.CompareTag("Checkpoint"))
          {
            var coord = collision.gameObject.transform.position;
            AddCheckpoint(int.Parse(collision.name), coord);
            
            
          }
          
          if (collision.CompareTag("Powerup"))
          {
              AddPowerup(1);
              Debug.Log("Player touched the powerup");
            powerupController.ActivatePowerup(collision.gameObject);
              
          }
      }


    void OnCollisionEnter(Collision collision)
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

    }
}
