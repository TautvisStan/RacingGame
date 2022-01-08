using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    int hits = 0;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            hits++;
            Debug.Log(this.gameObject.name + " was hit " + hits.ToString() + " times");
            if (hits == 3)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                foreach (Transform tf in this.transform)
                {
                    GameObject obj = tf.gameObject;
                    StartCoroutine(DestroyObjectAfter(obj, 10f));
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
