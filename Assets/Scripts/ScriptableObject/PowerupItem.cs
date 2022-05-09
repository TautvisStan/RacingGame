using UnityEngine;

public abstract class PowerupItem : ScriptableObject
{
    public Sprite PowerupImage;

    public abstract bool Activate(Player player);
}
