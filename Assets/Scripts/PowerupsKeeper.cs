using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsKeeper : MonoBehaviour
{
    [SerializeField] private UnlockablePowerup[] Powerups;

    private void Start()
    {
        UpdatePowerupStatus();
    }

    private void UpdatePowerupStatus()
    {
        for (int i = 0; i < Powerups.Length; i++)
        {
            int status = PlayerPrefs.GetInt("Powerup" + i, -1);
            if (status == -1)
            {
                PlayerPrefs.SetInt("Powerup" + i, Powerups[i].unlocked ? 1 : 0);
            }
            else
            {
                Powerups[i].unlocked = Convert.ToBoolean(status);
            }
        }
    }

    public UnlockablePowerup[] GetUnlockablePowerups()
    {
        UpdatePowerupStatus();
        return Powerups;
    }
}
