using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class CarScriptableObject : ScriptableObject
{
    public GameObject model;
    public Player player;
    
}
