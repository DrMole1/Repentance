using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEyes : MonoBehaviour
{
    // Déclaration des variables
    // ===============================
    public float magnitude = 1f;
    private Transform perso;
    public bool stop = true;

    private bool isSetUp = false;
    private Vector3 startPosition;
    // ===============================

    private void Start()
    {
        perso = GameObject.Find("Player").GetComponent<Transform>();
    }


    public void SetUp()
    {
        startPosition = transform.position;
        isSetUp = true;
    }

    void Update()
    {
        if(!stop && isSetUp)
        {
            Vector3 newPos = Vector3.ClampMagnitude(new Vector3(perso.position.x - startPosition.x, perso.position.y - startPosition.y, startPosition.z), magnitude);

            transform.position = startPosition + newPos;
        }
    }
}
