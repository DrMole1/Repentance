using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheDecor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LeanTween.alpha(gameObject, 0f, 1f);
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LeanTween.alpha(gameObject, 1f, 1f);
        }
    }
}
