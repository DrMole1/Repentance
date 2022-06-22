using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

// Ce script est utilisé pour faire des variations de lumières sur le temps
public class LightAnim : MonoBehaviour
{
    [Header("Param to change")]
    public float minLight = 0f;
    public float maxLight = 0f;
    public float timeToChange = 0f;
    public float timeToStay = 0f;
    public bool startToAdd = true;
    [HideInInspector] public bool stopGlow = false;



    void Start()
    {
        if(startToAdd)
        {
            GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = minLight;
            StartCoroutine(MoreLight());
        }
        else
        {
            GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = maxLight;
            StartCoroutine(LessLight());
        }
    }


    IEnumerator MoreLight()
    {
        LeanTween.value(gameObject, minLight, maxLight, timeToChange).setOnUpdate((float val) => {
            GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        yield return new WaitForSeconds(timeToChange + timeToStay);

        if(!stopGlow)
            StartCoroutine(LessLight());
    }


    IEnumerator LessLight()
    {
        LeanTween.value(gameObject, maxLight, minLight, timeToChange).setOnUpdate((float val) => {
            GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        yield return new WaitForSeconds(timeToChange + timeToStay);

        if (!stopGlow)
            StartCoroutine(MoreLight());
    }

    public void StopGlow()
    {
        stopGlow = true;
    }
}
