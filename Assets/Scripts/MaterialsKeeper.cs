using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsKeeper : MonoBehaviour
{
    [SerializeField] private Material[] Materials;
    public Material[] GetMaterials()
    {
        return Materials;
    }
}
