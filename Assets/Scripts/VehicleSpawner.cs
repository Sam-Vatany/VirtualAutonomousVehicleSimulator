using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject aiVehicle;
    public int vehiclesToSpawn;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < vehiclesToSpawn)
        {
            GameObject obj = Instantiate(aiVehicle);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<VehicleAIController>().currentNode = child.GetComponent<CarNode>();
            obj.transform.position = child.position;
            obj.transform.Rotate(0 , child.transform.eulerAngles.y , 0);

            yield return new WaitForEndOfFrame();

            count++;
        }
    }
}
