using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private int hits = 0;
    [SerializeField] private int MaxHits = 3;
    [SerializeField] private float SecondsToDestroy = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            hits++;
            if (hits == MaxHits)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                foreach (Transform tf in this.transform)
                {
                    GameObject obj = tf.gameObject;
                    StartCoroutine(DestroyObjectAfter(obj, SecondsToDestroy));
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    rb.mass = 100;
                    rb.isKinematic = false;
                }
            }
        }
    }

    IEnumerator DestroyObjectAfter(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
