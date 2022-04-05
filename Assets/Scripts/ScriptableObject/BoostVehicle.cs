using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Powerup Object", menuName = "Powerups/BoostVehicle")]
public class BoostVehicle : PowerupItem
{
    [SerializeField] private float Power;
    public override void Activate(Player player)
    {
        player.GetPlayerTransform().GetComponent<Rigidbody>().AddForce(player.GetPlayerTransform().transform.forward * Power);
    }
}
