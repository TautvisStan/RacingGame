using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float Power;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Transform PlayerTransform = collision.gameObject.GetComponent<ModelScript>().GetPlayerController().GetTransform();            
            PlayerTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, Power, 0);
            Destroy(gameObject);
        }
    }
}
