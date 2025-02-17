using System.Collections;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class AIcarEngine : MonoBehaviour
{
    public Transform path;
    public float maxsteerAngle = 70f;
    public WheelCollider Frontleft;
    public WheelCollider Frontright;
    public WheelCollider Rearleft;
    public WheelCollider Rearright;
    public float maxMotorTorque = 80f;
    public float maxBreakTorque = 150f;
    public float currentspeed;
    public float maxspeed = 40f;
    private List<Transform> node;
    private int currentnode = 0;
    public Vector3 centerOfMass;
    public bool isBreaking = false;
    public LayerMask mask;
    [Header("Sensors")]
    public float sensorLength = 5f;
    public float sideSensorOffset = 0.5f;
    public float sensorAngle = 30f;
    public Vector3 sensorOffset = new Vector3(0f, 0.5f, 1f);
    private bool isAvoiding = false;
    private bool isBlocked = false;

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathtransform = path.GetComponentsInChildren<Transform>();
        node = new List<Transform>();
        for (int i = 0; i < pathtransform.Length; i++)
        {
            if (pathtransform[i] != transform)
            {
                node.Add(pathtransform[i]);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SensorCheck();
        Applysteer();
        Drive();
        CheckWaypointDistance();
        Braking();
    }

    private void SensorCheck()
    {
        isAvoiding = false;
        isBlocked = false;
        isBreaking = false;

        // Base sensor start position
        Vector3 sensorStartPosition = transform.position + transform.forward * sensorOffset.z + transform.up * sensorOffset.y;

        // Front sensor
        bool frontBlocked = Physics.Raycast(sensorStartPosition, transform.forward, out RaycastHit front, sensorLength);

        // Right front sensor
        bool rightBlocked = Physics.Raycast(sensorStartPosition + transform.right * sideSensorOffset, transform.forward, out RaycastHit left, sensorLength, mask);

        // Left front sensor
        bool leftBlocked = Physics.Raycast(sensorStartPosition - transform.right * sideSensorOffset, transform.forward, out RaycastHit right, sensorLength, mask);

        // Right angled sensor
        bool rightAngledBlocked = Physics.Raycast(sensorStartPosition + transform.right * sideSensorOffset, Quaternion.Euler(0, sensorAngle, 0) * transform.forward, sensorLength, mask);

        // Left angled sensor
        bool leftAngledBlocked = Physics.Raycast(sensorStartPosition - transform.right * sideSensorOffset, Quaternion.Euler(0, -sensorAngle, 0) * transform.forward, sensorLength, mask);

        // Debug rays for visualization
        Debug.DrawRay(sensorStartPosition, transform.forward * sensorLength, Color.blue); // Front
        Debug.DrawRay(sensorStartPosition + transform.right * sideSensorOffset, transform.forward * sensorLength, Color.yellow); // Right
        Debug.DrawRay(sensorStartPosition - transform.right * sideSensorOffset, transform.forward * sensorLength, Color.yellow); // Left
        Debug.DrawRay(sensorStartPosition + transform.right * sideSensorOffset, Quaternion.Euler(0, sensorAngle, 0) * transform.forward * sensorLength, Color.cyan); // Right angled
        Debug.DrawRay(sensorStartPosition - transform.right * sideSensorOffset, Quaternion.Euler(0, -sensorAngle, 0) * transform.forward * sensorLength, Color.cyan); // Left angled

        Debug.Log(frontBlocked);
        if (frontBlocked && !front.collider.CompareTag("Park"))
        {
            isBlocked = true;
            isBreaking = true;
        }
        if ((frontBlocked && front.collider != null && front.collider.CompareTag("Car")) ||
        (rightBlocked && right.collider != null && right.collider.CompareTag("Car")) ||
        (leftBlocked && left.collider != null && left.collider.CompareTag("Car")))
        {
            isBreaking = true;
        }
    }

    private void Applysteer()
    {
        Vector3 relativevector = transform.InverseTransformPoint(node[currentnode].position);
        float newsteer = (relativevector.x / relativevector.magnitude) * maxsteerAngle;
        Frontleft.steerAngle = newsteer;
        Frontright.steerAngle = newsteer;
    }

    private void Drive()
    {
        currentspeed = 2 * Mathf.PI * Frontleft.radius * Frontleft.rpm * 60 / 1000;
        if (currentspeed < maxspeed && !isBreaking)
        {
            Frontleft.motorTorque = maxMotorTorque;
            Frontright.motorTorque = maxMotorTorque;
        }
        else
        {
            Frontleft.motorTorque = 0;
            Frontright.motorTorque = 0;
        }
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, node[currentnode].position) < 1f)
        {
            if (currentnode == node.Count - 1)
            {
                currentnode = 0;
            }
            else
            {
                currentnode++;
            }
        }
    }

    private void Braking()
    {
        if (isBreaking)
        {
            Rearleft.brakeTorque = maxBreakTorque;
            Rearright.brakeTorque = maxBreakTorque;
            Frontleft.brakeTorque = maxBreakTorque;
            Frontright.brakeTorque = maxBreakTorque;
        }
        else
        {
            Rearleft.brakeTorque = 0;
            Rearright.brakeTorque = 0;
            Frontleft.brakeTorque = 0;
            Frontright.brakeTorque = 0;
        }
    }
}
