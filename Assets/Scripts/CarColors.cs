using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColors : MonoBehaviour
{
    private Singleton singleton;
    private UnlockableMaterial[] materials;
    private bool[] inUse;

    void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
        materials = singleton.PassUnlockabeMaterials();
        inUse = new bool[materials.Length];
        for (int i = 0; i < inUse.Length; i++)
        {
            inUse[i] = false;
        }
    }

    public int GetSpareMaterialsNum()
    {
        int num = 0;
        foreach(bool used in inUse)
        {
            if(!used)
            {
                num++;
            }
        }
        return num;
    }

    public int ClearColor(int materialNum)
    {
        inUse[materialNum] = false;
        return -1;
    }

    public int SetColorsNext(GameObject car, int materialNum)
    {
        bool available = false;
        for(int i = 0; i < materials.Length; i++)
        {
            if(!inUse[i] && materials[i].unlocked)
            {
                available = true;
            }
        }
        if (!available)
        {
            return materialNum;
        }
        if (materialNum != -1)
        {
            inUse[materialNum] = false;
        }     
        materialNum++;
        if (materialNum == materials.Length)
        {
            materialNum = 0;
        }
        while (inUse[materialNum] || !materials[materialNum].unlocked)
        {
            materialNum++;
            if (materialNum == materials.Length)
            {
                materialNum = 0;
            }
        }
        SetColor(car, materialNum);
        inUse[materialNum] = true;
        return materialNum;
    }

    public int SetColorsPrevious(GameObject car, int materialNum)
    {
        bool available = false;
        for (int i = 0; i < materials.Length; i++)
        {
            if (!inUse[i] && materials[i].unlocked)
            {
                available = true;
            }
        }
        if (!available)
        {
            return materialNum;
        }
        if (materialNum != -1)
        {
            inUse[materialNum] = false;
        }
        materialNum--;
        if (materialNum == -1)
        {
            materialNum = materials.Length - 1;
        }
        while (inUse[materialNum] || !materials[materialNum].unlocked)
        {
            materialNum--;
            if (materialNum == -1)
            {
                materialNum = materials.Length - 1;
            }
        }
        SetColor(car, materialNum);
        inUse[materialNum] = true;
        return materialNum;
    }

    public void SetColor(GameObject car, int materialNum)
    {
        Transform t = car.transform;
        foreach (Transform tr in t)
        {
            if (tr.CompareTag("CarColorable"))
            {
                if (tr.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    renderer.material = materials[materialNum].material;
                }
            }
            SetColor(tr.gameObject, materialNum);
        }
    }
}
