using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class KillPlayer : MonoBehaviour
{
    public GameObject light;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            light.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 0;

        }

        //if(collision)
    }
}
