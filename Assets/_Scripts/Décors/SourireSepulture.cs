using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourireSepulture : MonoBehaviour
{
    private bool once = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && once)
        {
            once = false;

            StartCoroutine(Smile());
        }
    }


    IEnumerator Smile()
    {
        LeanTween.alpha(gameObject, 1f, 1f);

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
