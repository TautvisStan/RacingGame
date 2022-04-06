using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Powerup Object", menuName = "Powerups/PlaceBehind")]
public class PlaceObject : PowerupItem
{
    [SerializeField] private GameObject Object;
    [SerializeField] private bool Behind;
    [SerializeField] private float Distance;
    public override bool Activate(Player player)
    {
        if (Behind)
        {
            if (!Physics.Raycast(player.GetPlayerTransform().position, player.GetPlayerTransform().forward * -1 + player.GetPlayerTransform().up, Distance))
            {
                Instantiate(Object, player.GetPlayerTransform().position - player.GetPlayerTransform().forward * Distance + player.GetPlayerTransform().up, player.GetPlayerTransform().rotation);
                return true;
            }
            return false;
        }
        else
        {
            if (!Physics.Raycast(player.GetPlayerTransform().position, player.GetPlayerTransform().forward * +1 + player.GetPlayerTransform().up, Distance))
            {
                Instantiate(Object, player.GetPlayerTransform().position + player.GetPlayerTransform().forward * Distance + player.GetPlayerTransform().up, player.GetPlayerTransform().rotation);
                return true;
            }
            return false;
        }
    }
}
