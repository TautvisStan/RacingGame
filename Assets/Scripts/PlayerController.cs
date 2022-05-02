using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private GameObject Model;
    private Rigidbody ModelRigidbody;
    [SerializeField] private KeyCode Forwards;
    [SerializeField] private KeyCode Backwards;
    [SerializeField] private KeyCode Left;
    [SerializeField] private KeyCode Right;
    [SerializeField] private KeyCode Reset;
    [SerializeField] private KeyCode Brake;
    [SerializeField] private KeyCode Confirm;
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private bool isBraking;
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private WheelCollider FLCol;
    [SerializeField] private WheelCollider FRCol;
    [SerializeField] private WheelCollider RLCol;
    [SerializeField] private WheelCollider RRCol;
    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform RL;
    [SerializeField] private Transform RR;
    private bool respawned = false;

    private void Start()
    {
        ModelRigidbody = Model.GetComponent<Rigidbody>();
        Player = GetComponent<Player>();
    }

    public Transform GetTransform()
    {
        return Model.transform;
    }

    private void FixedUpdate()
    {
        if (respawned)
        {
            FLCol.brakeTorque = float.MaxValue;
            FRCol.brakeTorque = float.MaxValue;
            RLCol.brakeTorque = float.MaxValue;
            RRCol.brakeTorque = float.MaxValue;
            ModelRigidbody.isKinematic = true;
            respawned = false;
        }
        else
        {
            ModelRigidbody.isKinematic = false;
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
            if (Input.GetKey(Reset))
            {
                respawned = true;
                Model.transform.localRotation = Quaternion.Euler(0, Model.transform.rotation.eulerAngles.y, 0);
            }
            if (Input.GetKey(Confirm))
            {
                Player.ActivatePowerup();
            }
        }
    }

    public void SetControls(Dictionary<string, KeyCode> controls)
    {
        Forwards = controls["Forwards"];
        Backwards = controls["Backwards"];
        Left = controls["Left"];
        Right = controls["Right"];
        Reset = controls["Reset"];
        Brake = controls["Brake"];
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
        isBraking = Input.GetKey(Brake);
    }

    private void HandleMotor()
    {
        FLCol.motorTorque = verticalInput * motorForce;
        FRCol.motorTorque = verticalInput * motorForce;
        RLCol.motorTorque = verticalInput * motorForce;
        RRCol.motorTorque = verticalInput * motorForce;
        if (isBraking)
        {
            currentBrakeForce = brakeForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        FRCol.brakeTorque = currentBrakeForce;
        FLCol.brakeTorque = currentBrakeForce;
        RLCol.brakeTorque = currentBrakeForce;
        RRCol.brakeTorque = currentBrakeForce;
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
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        rot *= Quaternion.Euler(new Vector3(0, 0, 90));
        wheelTransform.SetPositionAndRotation(pos, rot);
    }
}