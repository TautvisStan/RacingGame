using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject Ramp;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var animation = Ramp.GetComponent<Animation>();
            animation.Play();
            Destroy(this.gameObject);
        }

    }
}
