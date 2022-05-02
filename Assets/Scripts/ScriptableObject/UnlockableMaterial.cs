using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unlockable Material", menuName = "Unlockables/Material")]
public class UnlockableMaterial : ScriptableObject
{
    public Material material;
    public int price;
    public bool unlocked = false;
}
