using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrail : MonoBehaviour
{

    Vector3 pz;

    public Vector3 offset;



    // Update is called once per frame
    void Update()
    {
         pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        transform.position = pz - offset;
    }
}
