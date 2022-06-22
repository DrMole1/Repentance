using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendu : MonoBehaviour
{
    private bool once = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && once)
        {
            LeanTween.alpha(gameObject, 0f, 1f);

            Destroy(gameObject, 1f);
        }
    }
}
