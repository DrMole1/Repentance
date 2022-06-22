using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roue : MonoBehaviour
{
    public float speed = 0f;


    // Update is called once per frame
    void Update()
    {
        if(speed != 0f)
        {
            transform.Rotate(0, 0, speed, Space.Self);
        }
    }
}
