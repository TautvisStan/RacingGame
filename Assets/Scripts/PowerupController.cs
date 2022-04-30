using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerupController : MonoBehaviour
{
    private PowerupItem[] Items;
    private Singleton singleton;

    private void Start()
    {
        singleton = FindObjectOfType<Singleton>();
        Items = singleton.PassPowerups();
    }

    public void PickupPowerup(GameObject powerupObj, Player player)
    {
        powerupObj.GetComponentInParent<ParticleSystem>().Play();
        powerupObj.SetActive(false);
        CreatePowerup(powerupObj);
        int randomItem = Random.Range(0, Items.Length);
        player.Powerup = Items[randomItem];
    }

    public void CreatePowerup(GameObject PowerupObj)
    {
        StartCoroutine(Respawn(3f, PowerupObj));
    }
    
    public IEnumerator Respawn(float delayInSecs, GameObject PowerupObj)
    {
        yield return new WaitForSeconds(delayInSecs);
        PowerupObj.SetActive(true);
    }
}
