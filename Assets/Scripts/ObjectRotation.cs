using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody ThisObject;
    [SerializeField] private float power = 0f;
    [SerializeField] private bool ForwardX = false;
    [SerializeField] private bool ForwardY = false;
    [SerializeField] private bool ForwardZ = false;
    [SerializeField] private bool ReverseX = false;
    [SerializeField] private bool ReverseY = false;
    [SerializeField] private bool ReverseZ = false;

    void Awake()
    {
        if (ForwardX == true)
        {
            Vector3 vector = new Vector3(power, 0, 0);
            ThisObject.AddTorque(vector);
        }
        if (ForwardY == true)
        {
            Vector3 vector = new Vector3(0, power, 0);
            ThisObject.AddTorque(vector);
        }
        if (ForwardZ == true)
        {
            Vector3 vector = new Vector3(0, 0, power);
            ThisObject.AddTorque(vector);
        }
        if (ReverseX == true)
        {
            Vector3 vector = new Vector3(-power, 0, 0);
            ThisObject.AddTorque(vector);
        }
        if (ReverseY == true)
        {
            Vector3 vector = new Vector3(0, -power, 0);
            ThisObject.AddTorque(vector);
        }
        if (ReverseZ == true)
        {
            Vector3 vector = new Vector3(0, 0, -power);
            ThisObject.AddTorque(vector);
        }
    }
}