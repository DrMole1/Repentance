using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreVerticale : MonoBehaviour
{
    public float timeToMove = 2f;
    public float timeToWait = 1f;
    public float magnitude = 4f;

    void Start()
    {
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        LeanTween.moveY(gameObject, transform.position.y + magnitude, timeToMove);

        yield return new WaitForSeconds(timeToWait + timeToMove);

        LeanTween.moveY(gameObject, transform.position.y - magnitude, timeToMove);

        yield return new WaitForSeconds(timeToWait + timeToMove);

        StartCoroutine(Move());
    }
}
