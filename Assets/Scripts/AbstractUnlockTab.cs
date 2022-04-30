using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnlockTab : MonoBehaviour
{
    public int Price;
    public bool Unlocked;
    public int ID = 0;
    abstract public void UnlockItem();
    abstract public void LeftItem();
    abstract public void RightItem();
    abstract public void SetItemToStage();

}
