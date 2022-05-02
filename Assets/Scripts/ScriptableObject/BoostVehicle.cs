using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup Object", menuName = "Powerups/BoostVehicle")]
public class BoostVehicle : PowerupItem
{
    [SerializeField] private float Power;

    public override bool Activate(Player player)
    {
        player.GetPlayerTransform().GetComponent<Rigidbody>().velocity = player.GetPlayerTransform().GetComponent<Rigidbody>().velocity.magnitude * Power * player.GetPlayerTransform().transform.forward;
        return true;
    }
}
