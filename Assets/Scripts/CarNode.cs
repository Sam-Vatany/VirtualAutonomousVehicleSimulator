using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNode : MonoBehaviour{

    public CarNode previousWaypont;
    public CarNode nextWaypoint;

    public CarNode link;


    public Vector3 getPosition(){
        Vector3 minBound = transform.position ;
        Vector3 MAXBound = transform.position ;
    
        return Vector3.Lerp(minBound,MAXBound,Random.Range(0,1));
    }    

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position ,0.5f);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, forward);
    }

}
