using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommeChapeau : MonoBehaviour
{
    private bool once = true;
    public GameObject hand;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && once)
        {
            StartCoroutine(Fade());

            LeanTween.rotateZ(hand, -65f, 4f);
        }
    }


    IEnumerator Fade()
    {
        yield return new WaitForSeconds(5f);

        LeanTween.alpha(gameObject, 0f, 1f);
        LeanTween.alpha(hand, 0f, 1f);

        Destroy(gameObject, 1f);
    }
}
