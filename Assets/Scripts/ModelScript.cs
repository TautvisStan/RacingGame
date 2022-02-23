using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour
{
    public GameObject ScriptsObject;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            ScriptsObject.GetComponent<Player>().RegisterCheckpoint(collision.gameObject);
        }
    }
      void OnCollisionEnter(Collision collision)
  {
      if (collision.collider.CompareTag("FunCube"))
      {
          Debug.Log("YEET");
          collision.rigidbody.velocity = new Vector3(0, 10, 0);
      }

    /*  if (collision.collider.CompareTag("Fence"))
      {
          soundEffectsController.PlayFenceContactSound();
      }
      if (collision.collider.CompareTag("Player"))
      {
          soundEffectsController.PlayCarContactSound();
      }*/

  }
}
