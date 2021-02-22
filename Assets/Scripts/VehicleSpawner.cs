using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject aiVehicle;
    public int vehiclesToSpawn; // Limit to number of child objects
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        Transform[] children = GetComponentsInChildren<Transform>();
        int childrenCounter = 1;
        while (count < vehiclesToSpawn)
        {
            GameObject obj = Instantiate(aiVehicle);
            Transform child = children[childrenCounter++];
            obj.GetComponent<VehicleAIController>().currentNode = child.GetComponent<CarNode>();
            obj.transform.position = child.position;
            obj.transform.Rotate(0, child.transform.eulerAngles.y, 0);

            if (childrenCounter == children.Length) {
                break;
            }
            
            yield return new WaitForEndOfFrame();
            count++;
        }
    }
}
