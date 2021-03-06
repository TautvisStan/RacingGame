using System;
using UnityEngine;

public class MaterialsKeeper : MonoBehaviour
{
    [SerializeField] private UnlockableMaterial[] Materials;

    private void Start()
    {
        UpdateMaterialStatus();
    }

    private void UpdateMaterialStatus()
    {
        for (int i = 0; i < Materials.Length; i++)
        {
            int status = PlayerPrefs.GetInt("Material" + i, -1);
            if (status == -1)
            {
                PlayerPrefs.SetInt("Material" + i, Materials[i].unlocked ? 1 : 0);
            }
            else
            {
                Materials[i].unlocked = Convert.ToBoolean(status);
            }
        }
    }

    public UnlockableMaterial[] GetUnlockableMaterials()
    {
        UpdateMaterialStatus();
        return Materials;
    }
}