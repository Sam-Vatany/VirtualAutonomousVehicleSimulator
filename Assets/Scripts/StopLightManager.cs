using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLightManager : MonoBehaviour
{
    public float redLightLength = 20.0f;
    private float timer = 0;
    private bool redLight;
    // Start is called before the first frame update
    void Start()
    {
        redLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        //every redLightLength amount of time the tag is switched which determines if the car detects the object as an obstacle or not
        if (timer > redLightLength)
        {
            if (gameObject.tag == "Character")
            {
                gameObject.tag = "Untagged";
            }
            else
            {
                gameObject.tag = "Character";
            }

            //reset timer
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
