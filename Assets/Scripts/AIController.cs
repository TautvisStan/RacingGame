using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Rigidbody ModelRigidbody;
    [SerializeField] private GameObject path;
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
    private List<GameObject> nodes;
    private int current;
    [SerializeField] private float SensorLength;
    [SerializeField] private Vector3 FrontSPos = new Vector3(0, 0.2f, 0.5f);
    [SerializeField] private float SideSPos;
    [SerializeField] private float SAngle;
    private bool isAvoiding = false;
    private bool respawned = false;
    private float steerAngle = 0;
    [SerializeField] private float steerSpeed = 5f;

    private void Start()
    {
        ModelRigidbody = Model.GetComponent<Rigidbody>();
        path = FindObjectOfType<AIPath>().gameObject;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<GameObject>();
        foreach (Transform node in pathTransforms)
        {
            if (node != path.transform)
            {
                nodes.Add(node.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        
        if(respawned)
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
            HandleSteering();
            HandleMotor();
            CheckDistance();
            HandleSensors();
            LerpToAngle();
            UpdateWheels();
            ModelRigidbody.isKinematic = false;
            Vector3 rotation = Model.transform.rotation.eulerAngles;
            if ((rotation.x >= 90 && rotation.x <= 270) || (rotation.z >= 90 && rotation.z <= 270))
            {
                respawned = true;
                Model.transform.localRotation = Quaternion.Euler(0, Model.transform.rotation.eulerAngles.y, 0);
            }
        }
    }

    private void HandleSensors()
    {
        RaycastHit[] raycasts;
        float avoidPower = 0;
        Vector3 SensorPos = Model.transform.position + Model.transform.forward * FrontSPos.z + Model.transform.up * FrontSPos.y;
        isAvoiding = false;
        int activeSensors = 0;
        SensorPos += Model.transform.right * SideSPos;
        raycasts = Physics.RaycastAll(SensorPos, Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Powerup"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += -1f;
                activeSensors++;
                break;
            }
        }
        raycasts = Physics.RaycastAll(SensorPos, Quaternion.AngleAxis(SAngle, Model.transform.up) * Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Powerup"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += -0.5f;
                activeSensors++;
                break;
            }
        }
        SensorPos -= 2 * SideSPos * Model.transform.right;
        raycasts = Physics.RaycastAll(SensorPos, Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Powerup"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += 1f;
                activeSensors++;
                break;
            }
        }
        raycasts = Physics.RaycastAll(SensorPos, Quaternion.AngleAxis(-SAngle, Model.transform.up) * Model.transform.forward, SensorLength);
        foreach (RaycastHit hit in raycasts)
        {
            if (!hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Powerup"))
            {
                Debug.DrawLine(SensorPos, hit.point);
                isAvoiding = true;
                avoidPower += 0.5f;
                activeSensors++;
                break;
            }
        }
        if (isAvoiding && avoidPower == 0)
        {
            SensorPos = Model.transform.position + Model.transform.forward * FrontSPos.z + Model.transform.up * FrontSPos.y;
            raycasts = Physics.RaycastAll(SensorPos, Model.transform.forward, SensorLength);
            foreach (RaycastHit hit in raycasts)
            {
                if (!hit.collider.CompareTag("Checkpoint") && !hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Powerup"))
                {
                    Debug.DrawLine(SensorPos, hit.point);
                    activeSensors++;
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
            if(activeSensors == 5)
            {
                avoidPower *= -1;
                ApplyTorques(-1);
            }
            if(avoidPower > 1)
            {
                avoidPower = 1;
            }
            if(avoidPower < -1)
            {
                avoidPower = -1;
            }
            steerAngle = maxSteerAngle * avoidPower;
        }
    }

    private void HandleSteering()
    {
        if (isAvoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[current].transform.position); 
        currentSteerAngle = maxSteerAngle * (relativeVector.x / relativeVector.magnitude);
        steerAngle = currentSteerAngle;
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
        if (Vector3.Distance(Model.transform.position, nodes[current].transform.position) < 3f)
        {
            if(current == nodes.Count - 1)
            {
                current = 0;
            }
            else
            {
                current++;
            }
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

    private void LerpToAngle()
    {
        FLCol.steerAngle = Mathf.Lerp(FLCol.steerAngle, steerAngle, Time.deltaTime * steerSpeed);
        FRCol.steerAngle = Mathf.Lerp(FRCol.steerAngle, steerAngle, Time.deltaTime * steerSpeed);
    }
}
