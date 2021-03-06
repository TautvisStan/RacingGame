using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup Object", menuName = "Powerups/PlaceBehind")]
public class PlaceObject : PowerupItem
{
    [SerializeField] private GameObject Object;
    [SerializeField] private bool Behind;
    [SerializeField] private float Distance;

    public override bool Activate(Player player)
    {
        RaycastHit[] hits;
        bool blocked = false;
        if (Behind)
        {
            hits = Physics.RaycastAll(player.GetPlayerTransform().position, player.GetPlayerTransform().forward * -1 + player.GetPlayerTransform().up, Distance);
            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CarColorable") && !hit.collider.CompareTag("Checkpoint"))
                {
                    blocked = true;
                }
            }
            if (!blocked)
            {
                Instantiate(Object, player.GetPlayerTransform().position - player.GetPlayerTransform().forward * Distance + player.GetPlayerTransform().up, player.GetPlayerTransform().rotation);
                return true;
            }
            return false;
        }
        else
        {
            hits = Physics.RaycastAll(player.GetPlayerTransform().position, player.GetPlayerTransform().forward * +1 + player.GetPlayerTransform().up, Distance);
            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CarColorable") && !hit.collider.CompareTag("Checkpoint"))
                {
                    blocked = true;
                }
            }
            if (!blocked)
            {
                Instantiate(Object, player.GetPlayerTransform().position + player.GetPlayerTransform().forward * Distance + player.GetPlayerTransform().up, player.GetPlayerTransform().rotation);
                return true;
            }
            return false;
        }
    }
}
