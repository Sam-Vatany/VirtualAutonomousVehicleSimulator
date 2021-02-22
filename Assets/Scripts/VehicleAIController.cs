using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAIController : MonoBehaviour{

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    
    public float totalPower;
    private bool isBraking = false;
    public float brakeForce;
    public float horizontal ;

    private float radius = 8 , distance;
    public CarNode currentNode;

    private Vector3 velocity, Destination, lastPosition;

    public float forwardSensorLength = 10;
    public float angledSensorLength = 5;
    public float frontSensorAngle = 30;
    public float targetDistance = 4;
    

    private bool avoiding = false;
    private bool reachedTargetDistance = false;

    void Start() {

    }

    void FixedUpdate() {
        try{
        sensors();
        checkDistance();
        steerVehicle();
        }
        catch{}
    }

    private void sensors() {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward;
        sensorStartPos += transform.up;
        isBraking = false;
        float avoidMultiplier = 0;
        avoiding = false;
        reachedTargetDistance = false;

        //front center sensor
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, forwardSensorLength)) {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red, 0.01f);
            if (hit.collider.CompareTag("Character")) {
                if (frontLeftWheelCollider.rpm > 40) {
                    isBraking = true;
                    applyBraking();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance) == false) {
                    moveToTargetDistance();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance)) {
                    isBraking = true;
                    applyBraking();
                }
            }
        }
        
        //front right sensor
        sensorStartPos += transform.right * 1.1f;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, forwardSensorLength)) {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red, 0.01f);
            if (hit.collider.CompareTag("Character")) {
                if (frontLeftWheelCollider.rpm > 40) {
                    isBraking = true;
                    applyBraking();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance) == false) {
                    moveToTargetDistance();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance)) {
                    isBraking = true;
                    applyBraking();
                }
            }
        }

        //front right angle sensor
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, angledSensorLength)) {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red, 0.01f);
            if (hit.collider.CompareTag("Character")) {
                avoiding = true;
                avoidMultiplier = -0.5f;
            }
        }

        //front left sensor
        sensorStartPos -= transform.right * 2.2f; 
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, forwardSensorLength)) {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red, 0.01f);
            if (hit.collider.CompareTag("Character")) {
                if (frontLeftWheelCollider.rpm > 40) {
                    isBraking = true;
                    applyBraking();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance) == false) {
                    moveToTargetDistance();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance)) {
                    isBraking = true;
                    applyBraking();
                }
            }
        }

        //front left sensor angle
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, angledSensorLength)) {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red, 0.01f);
            if (hit.collider.CompareTag("Character")) {
                avoiding = true;
                avoidMultiplier = -0.5f;
            }
        }

        //front middle right sensor 
        sensorStartPos += transform.right * 1.65f; 
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, forwardSensorLength)) {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red, 0.01f);
            if (hit.collider.CompareTag("Character")) {
                if (frontLeftWheelCollider.rpm > 40) {
                    isBraking = true;
                    applyBraking();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance) == false) {
                    moveToTargetDistance();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance)) {
                    isBraking = true;
                    applyBraking();
                }
            }
        }

        //front middle left sensor
        sensorStartPos -= transform.right * 1.1f; 
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, forwardSensorLength)) {
            Debug.DrawLine(sensorStartPos, hit.point, Color.red, 0.01f);
            if (hit.collider.CompareTag("Character")) {
                if (frontLeftWheelCollider.rpm > 40) {
                    isBraking = true;
                    applyBraking();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance) == false) {
                    moveToTargetDistance();
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, targetDistance)) {
                    isBraking = true;
                    applyBraking();
                }
            }
        }

        if(avoiding) {
            horizontal *= avoidMultiplier;
        }
    }

    private void applyBraking() {
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
            rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0; 
            frontRightWheelCollider.brakeTorque = brakeForce;
            frontLeftWheelCollider.brakeTorque = brakeForce;
            rearLeftWheelCollider.brakeTorque = brakeForce;
            rearRightWheelCollider.brakeTorque = brakeForce;
    }  
    
    private void moveToTargetDistance() {
        frontLeftWheelCollider.motorTorque = totalPower;
        frontRightWheelCollider.motorTorque = totalPower;
        rearLeftWheelCollider.motorTorque = totalPower;
        rearRightWheelCollider.motorTorque = totalPower;
    }


    void checkDistance() {

            if (Vector3.Distance(transform.position , currentNode.transform.position) <= 3) {
                reachedDestination();
            }

        
    }

        
    private void reachedDestination() {
        if (currentNode.nextWaypoint == null ) {
            currentNode = currentNode.previousWaypont;
            return;
        }
        if (currentNode.previousWaypont == null ) {
            currentNode = currentNode.nextWaypoint;
            return;
        }

        if (currentNode.link != null && Random.Range(0 , 100) <= 50)
            currentNode = currentNode.link;
        else
            currentNode = currentNode.nextWaypoint;
    }


    private void steerVehicle() {

        Vector3 relativeVector = transform.InverseTransformPoint(currentNode.transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x  / relativeVector.magnitude) * 2;
        if (avoiding == false){
            horizontal = newSteer;
        }
        if (isBraking == false) {
            frontRightWheelCollider.brakeTorque = 0;
            frontLeftWheelCollider.brakeTorque = 0;
            rearLeftWheelCollider.brakeTorque = 0;
            rearRightWheelCollider.brakeTorque = 0;
            frontLeftWheelCollider.motorTorque = totalPower;
            frontRightWheelCollider.motorTorque = totalPower;
            rearLeftWheelCollider.motorTorque = totalPower;
            rearRightWheelCollider.motorTorque = totalPower;
        }

        if (horizontal > 0 ) {
            frontLeftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            frontRightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        } else if (horizontal < 0 ) {                                                          
            frontLeftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            frontRightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
        } else {
            frontLeftWheelCollider.steerAngle = 0;
            frontRightWheelCollider.steerAngle = 0;
        }

    }

}
