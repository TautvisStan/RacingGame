using UnityEngine;
using UnityEngine.UI;

public class PowerupsMenu : AbstractUnlockTab
{
    private Singleton singleton;
    public UnlockablePowerup[] powerups;
    [SerializeField] private Image PowerupImage;

    public override void LeftItem()
    {
        if (ID == 0)
        {
            ID = powerups.Length - 1;
        }
        else
        {
            ID--;
        }
        SetItemToStage();
    }

    public override void RightItem()
    {
        if (ID == powerups.Length - 1)
        {
            ID = 0;
        }
        else
        {
            ID++;
        }
        SetItemToStage();
    }

    private void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        powerups = singleton.PassUnlockablePowerups();
    }

    public override void SetItemToStage()
    {
        Price = powerups[ID].price;
        Unlocked = powerups[ID].unlocked;
        PowerupImage.sprite = powerups[ID].Powerup.PowerupImage;
    }

    public override void UnlockItem()
    {
        PlayerPrefs.SetInt("Powerup" + ID, 1);
        powerups = singleton.PassUnlockablePowerups();
    }
}
