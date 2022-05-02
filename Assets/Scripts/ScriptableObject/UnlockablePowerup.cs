using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unlockable Powerup", menuName = "Unlockables/Powerup")]
public class UnlockablePowerup : ScriptableObject
{
    public PowerupItem Powerup;
    public int price;
    public bool unlocked = false;
}
