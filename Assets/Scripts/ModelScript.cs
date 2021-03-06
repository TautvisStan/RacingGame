using UnityEngine;

public class ModelScript : MonoBehaviour
{
    [SerializeField] private GameObject ScriptsObject;
    private SoundEffects soundEffectsController;
    [SerializeField] private Vector3 CenterOfMass;

    private void Start()
    {
        soundEffectsController = FindObjectOfType<SoundEffects>();
        GetComponent<Rigidbody>().centerOfMass = CenterOfMass;
    }

    public PlayerController GetPlayerController()
    {
        return ScriptsObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            ScriptsObject.GetComponent<Player>().RegisterCheckpoint(collision.gameObject);
        }
        if (collision.CompareTag("Powerup"))
        {
            ScriptsObject.GetComponent<Player>().RegisterPowerup(collision.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("FunCube"))
        {
            collision.rigidbody.velocity = new Vector3(0, 10, 0);
        }
        if (collision.collider.CompareTag("Fence"))
        {
            soundEffectsController.PlayFenceContactSound();
        }
        if (collision.collider.CompareTag("Player"))
        {
            soundEffectsController.PlayCarContactSound();
        }
    }
}