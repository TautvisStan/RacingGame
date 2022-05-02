using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerupController : MonoBehaviour
{
    private UnlockablePowerup[] Items;
    private Singleton singleton;

    private void Start()
    {
        singleton = FindObjectOfType<Singleton>();
        Items = singleton.PassUnlockablePowerups();
    }

    public void PickupPowerup(GameObject powerupObj, Player player)
    {
        powerupObj.GetComponentInParent<ParticleSystem>().Play();
        powerupObj.SetActive(false);
        CreatePowerup(powerupObj);
        int randomItem = Random.Range(0, Items.Length);
        while(!Items[randomItem].unlocked)
        {
            randomItem++;
            if(randomItem == Items.Length)
            {
                randomItem = 0;
            }
        }
        player.Powerup = Items[randomItem].Powerup;
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
