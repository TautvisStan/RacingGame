using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Model;
    [SerializeField] private KeyCode Forwards;
    [SerializeField] private KeyCode Backwards;
    [SerializeField] private KeyCode Left;
    [SerializeField] private KeyCode Right;
    [SerializeField] private KeyCode Reset;
    [SerializeField] private KeyCode Break;
    [SerializeField] private KeyCode Confirm;

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider FLCol;
    [SerializeField] private WheelCollider FRCol;
    [SerializeField] private WheelCollider RLCol;
    [SerializeField] private WheelCollider RRCol;

    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform RL;
    [SerializeField] private Transform RR;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        if (Input.GetKey(Reset))
        {
            Model.transform.localRotation =
           Quaternion.Euler(0, Model.transform.rotation.eulerAngles.y, 0);
        }
    }
    public void SetControls(Dictionary<string, KeyCode> controls)
    {
        Forwards = controls["Forwards"];
        Backwards = controls["Backwards"];
        Left = controls["Left"];
        Right = controls["Right"];
        Reset = controls["Reset"];
        Break = controls["Break"];
        Confirm = controls["Confirm"];
    }

    private void GetInput()
    {
        if (Input.GetKey(Right)) horizontalInput = 1;
        if (Input.GetKey(Left)) horizontalInput = -1;
        if (!Input.GetKey(Right) && !Input.GetKey(Left)) horizontalInput = 0;
        if (Input.GetKey(Forwards)) verticalInput = 1;
        if (Input.GetKey(Backwards)) verticalInput = -1;
        if (!Input.GetKey(Forwards) && !Input.GetKey(Backwards)) verticalInput = 0;
        isBreaking = Input.GetKey(Break);
    }

    private void HandleMotor()
    {
        FLCol.motorTorque = verticalInput * motorForce;
        FRCol.motorTorque = verticalInput * motorForce;
        RLCol.motorTorque = verticalInput * motorForce;
        RRCol.motorTorque = verticalInput * motorForce;
        if (isBreaking)
            currentbreakForce = breakForce;
        else
            currentbreakForce = 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        FRCol.brakeTorque = currentbreakForce;
        FLCol.brakeTorque = currentbreakForce;
        RLCol.brakeTorque = currentbreakForce;
        RRCol.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        FLCol.steerAngle = currentSteerAngle;
        FRCol.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FLCol, FL);
        UpdateSingleWheel(FRCol, FR);
        UpdateSingleWheel(RRCol, RR);
        UpdateSingleWheel(RLCol, RL);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos = wheelTransform.position;
        Quaternion rot = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out pos, out rot);
        rot = rot * Quaternion.Euler(new Vector3(0, 0, 90));
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}