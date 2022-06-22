using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVague : MonoBehaviour
{

    public GameObject vague;
    public float timeVague;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeanTween.moveLocalX(vague, -70f, timeVague);
    }
}
