using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsKeeper : MonoBehaviour
{
    [SerializeField] private PowerupItem[] Items;
    public PowerupItem[] GetPowerups()
    {
        return Items;
    }
}
