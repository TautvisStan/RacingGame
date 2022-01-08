using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField] 
    private GameObject Powerup;
    public void CreatePowerup(Vector3 coord)
    {
        StartCoroutine(Respawn(3f, coord));
    }
    public IEnumerator Respawn(float delayInSecs, Vector3 coord)
    {
        yield return new WaitForSeconds(delayInSecs);
        Instantiate(Powerup, coord, Powerup.transform.rotation, gameObject.transform);
        Powerup.SetActive(true);
        
    }
}
