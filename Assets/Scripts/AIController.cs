using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private Transform path;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private WheelCollider FLCol;
    [SerializeField] private WheelCollider FRCol;
    [SerializeField] private WheelCollider RLCol;
    [SerializeField] private WheelCollider RRCol;
    [SerializeField] private Transform FL;
    [SerializeField] private Transform FR;
    [SerializeField] private Transform RL;
    [SerializeField] private Transform RR;
    [SerializeField] private GameObject Model;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private bool isBraking = false;
    private List<Transform> nodes;
    private int current;
    [SerializeField] private float SensorLength;
    [SerializeField] private Vector3 FrontSPos = new Vector3(0, 0.2f, 0.5f);
    [SerializeField] private float SideSPos;
    [SerializeField] private float SAngle;
    private bool isAvoiding = false;
    private void Start()
    {
        path = FindObjectOfType<AIPath>().transform;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        foreach (Transform node in pathTransforms)
        {
            if (node != path.transform)
            {
                nodes.Add(node);
            }
        }
    }
    private void FixedUpdate()
    {
        HandleSteering();
        HandleMotor();
        CheckDistance();        
        HandleSensors();
        UpdateWheels();
    }
    private void HandleSensors()
    {
        RaycastHit[] raycasts;
        float avoidPower = 0;
        Vector3 SensorPos = Model.transform.position + Model.transform.forward * FrontSPos.z + Model.transform.up * FrontSPos.y;
        isAvoiding = false;


        SensorPos += Model.transform.right * SideSPos;
        raycasts = Physics.RaycastAll(SensorPos, Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CarColorable") && !hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += -1f;
                break;
            }
        }
        raycasts = Physics.RaycastAll(SensorPos, Quaternion.AngleAxis(SAngle, Model.transform.up) * Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CarColorable") && !hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += -0.5f;
                break;
            }
        }
        SensorPos -= 2 * SideSPos * Model.transform.right;
        raycasts = Physics.RaycastAll(SensorPos, Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CarColorable") && !hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += 1f;
                break;
            }
        }
        raycasts = Physics.RaycastAll(SensorPos, Quaternion.AngleAxis(-SAngle, Model.transform.up) * Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CarColorable") && !hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += 0.5f;
                break;
            }
        }
        if (isAvoiding && avoidPower == 0)
        {
            SensorPos = Model.transform.position + Model.transform.forward * FrontSPos.z + Model.transform.up * FrontSPos.y;
            raycasts = Physics.RaycastAll(SensorPos, Model.transform.forward, SensorLength);
            foreach (RaycastHit hit in raycasts)
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("CarColorable") && !hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain"))
                {
                    Debug.DrawLine(SensorPos, hit.point);
                    if (hit.normal.x < 0)
                    {
                        avoidPower = -1f;
                    }    
                    else
                    {
                        avoidPower = 1f;
                    }
                    break;
                }
            }
        }
        if (isAvoiding)
        {
            if(avoidPower > 1)
            {
                avoidPower = 1;
            }
            if(avoidPower < -1)
            {
                avoidPower = -1;
            }
            FLCol.steerAngle = maxSteerAngle * avoidPower;
            FRCol.steerAngle = maxSteerAngle * avoidPower;
        }
    }
    private void HandleSteering()
    {
        if (isAvoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[current].position);
        currentSteerAngle = maxSteerAngle * (relativeVector.x / relativeVector.magnitude);
        FLCol.steerAngle = currentSteerAngle;
        FRCol.steerAngle = currentSteerAngle;
    }
    private void HandleMotor()
    {
        float force = 0;
        if (isBraking)
        {
            currentBrakeForce = brakeForce; 
        }
        else
        {
            currentBrakeForce = 0f;
            force = 1f;
        }
        ApplyTorques(force);
    }
    private void ApplyTorques(float force)
    {
        FRCol.brakeTorque = currentBrakeForce;
        FLCol.brakeTorque = currentBrakeForce;
        RLCol.brakeTorque = currentBrakeForce;
        RRCol.brakeTorque = currentBrakeForce;
        FLCol.motorTorque = force * motorForce;
        FRCol.motorTorque = force * motorForce;
        RLCol.motorTorque = force * motorForce;
        RRCol.motorTorque = force * motorForce;
    }
    private void CheckDistance()
    {
        if (Vector3.Distance(Model.transform.position, nodes[current].position) < 6f)
        {
            if(current == nodes.Count - 1)
            {
                current = 0;
            }
            else
            {
                current++;
            }
            Debug.Log(current);
        }
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
