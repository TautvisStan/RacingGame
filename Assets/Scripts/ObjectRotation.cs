using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public Rigidbody ThisObject;
    //Rotational Speed
    public float speed = 0f;

    //Forward Direction
    public bool ForwardX = false;
    public bool ForwardY = false;
    public bool ForwardZ = false;

    //Reverse Direction
    public bool ReverseX = false;
    public bool ReverseY = false;
    public bool ReverseZ = false;

    void Awake()
    {
        //Forward Direction
    /*    if (ForwardX == true)
        {
            ThisObject.AddTorque(Time.deltaTime * speed, 0, 0);
          //  transform.(Time.deltaTime * speed, 0, 0, Space.Self);
        }*/
        if (ForwardY == true)
        {
            Vector3 vector = new Vector3(0, speed, 0);
            ThisObject.AddTorque(vector);
          //  transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
        }
     /*   if (ForwardZ == true)
        {
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }
        //Reverse Direction
        if (ReverseX == true)
        {
            transform.Rotate(-Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if (ReverseY == true)
        {
            transform.Rotate(0, -Time.deltaTime * speed, 0, Space.Self);
        }
        if (ReverseZ == true)
        {
            transform.Rotate(0, 0, -Time.deltaTime * speed, Space.Self);
        }*/

    }
}