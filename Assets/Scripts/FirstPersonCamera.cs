using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject CameraPrefab;

    private GameObject camera;

    void Start()
    {
        camera = (GameObject)GameObject.Instantiate(CameraPrefab);
        camera.transform.SetParent(transform);
        camera.transform.localPosition = new Vector3(-2, 1.6f, 0);
        camera.transform.localRotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        rotateCamera();
    }

    private void rotateCamera()
    {
        bool rotateLeft = Input.GetKey(KeyCode.Z);
        bool rotateRight = Input.GetKey(KeyCode.C);
        bool lookBack = Input.GetKey(KeyCode.DownArrow);

        if (rotateLeft)
        {
            camera.transform.localRotation = Quaternion.Euler(0, -45, 0);
        }
        else if (rotateRight)
        {
            camera.transform.localRotation = Quaternion.Euler(0, 45, 0);
        }
        else if (lookBack)
        {
            camera.transform.SetParent(transform);
            camera.transform.localPosition = new Vector3(0, 2, -7);
            camera.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            camera.transform.SetParent(transform);
            camera.transform.localPosition = new Vector3(-2, 1.6f, 0);
            camera.transform.localRotation = Quaternion.identity;
        } 
    }
}
