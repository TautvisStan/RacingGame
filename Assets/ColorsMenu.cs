using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsMenu : AbstractUnlockTab
{
    private Singleton singleton;
    private UnlockableMaterial[] materials;
    [SerializeField] private Image MaterialImage;
    public override void LeftItem()
    {
        if (ID == 0)
        {
            ID = materials.Length - 1;
        }
        else
        {
            ID--;
        }
        SetItemToStage();
    }
    public override void RightItem()
    {
        if (ID == materials.Length - 1)
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
        materials = singleton.PassUnlockabeMaterials();
    }
    public override void SetItemToStage()
    {
        Price = materials[ID].price;
        Unlocked = materials[ID].unlocked;
        MaterialImage.material = materials[ID].material;
    }
    public override void UnlockItem()
    {
        PlayerPrefs.SetInt("Material" + ID, 1);
        materials = singleton.PassUnlockabeMaterials();
    }
}
