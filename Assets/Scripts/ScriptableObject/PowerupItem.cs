using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerupItem :ScriptableObject
{
    public Sprite PowerupImage;
    public abstract void Activate(Player player);
}
