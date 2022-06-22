using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Traveling : MonoBehaviour
{
    public float timeToTravel = 2f;
    public float timeForOpacity = 3f;



    public void StartSpirit()
    {
        StartCoroutine(DoStart());
    }


    IEnumerator DoStart()
    {
        LeanTween.alpha(gameObject, 1f, 4f);

        yield return new WaitForSeconds(4f);

        StartCoroutine(GoTravel());
        StartCoroutine(ChangeAlpha());
    }


    IEnumerator GoTravel()
    {
        LeanTween.moveY(gameObject, transform.position.y + 1.5f, timeToTravel);

        yield return new WaitForSeconds(timeToTravel);

        LeanTween.moveY(gameObject, transform.position.y - 1.5f, timeToTravel);

        yield return new WaitForSeconds(timeToTravel);

        StartCoroutine(GoTravel());
    }

    IEnumerator ChangeAlpha()
    {
        LeanTween.alpha(gameObject, 0f, timeForOpacity);

        yield return new WaitForSeconds(timeForOpacity);

        LeanTween.alpha(gameObject, 1f, timeForOpacity);

        yield return new WaitForSeconds(timeForOpacity);

        StartCoroutine(ChangeAlpha());
    }
}
