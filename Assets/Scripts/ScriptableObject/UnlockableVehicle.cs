using UnityEngine;

[CreateAssetMenu(fileName = "New Unlockable Vehicle", menuName = "Unlockables/Vehicle")]
public class UnlockableVehicle : ScriptableObject
{
    public GameObject Vehicle;
    public int price;
    public bool unlocked = false;
}
