using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Powerup Object", menuName = "Powerups/PlaceBehind")]
public class PlaceObject : PowerupItem
{
    [SerializeField] private GameObject Object;
    [SerializeField] private bool Behind;
    [SerializeField] private float Distance;
    public override void Activate(Player player)
    {
        if (Behind)
        {
            Instantiate(Object, player.GetPlayerTransform().position - player.GetPlayerTransform().forward * Distance, player.GetPlayerTransform().rotation);
        }
        else
        {
            Instantiate(Object, player.GetPlayerTransform().position + player.GetPlayerTransform().forward * Distance, player.GetPlayerTransform().rotation);
        }
    }
}
