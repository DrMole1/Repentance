using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackScale : MonoBehaviour
{
    private const float timeToScale = 0.3f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aberration
        if (other.gameObject.tag == "Boule")
        {
            StartCoroutine(DoFeedBack());
        }
    }

    IEnumerator DoFeedBack()
    {
        float initScaleX = transform.localScale.x;
        float initScaleY = transform.localScale.y;

        LeanTween.value(gameObject, transform.localScale.x, transform.localScale.x * 1.1f, timeToScale).setOnUpdate((float val) => {
            transform.localScale = new Vector2(val, transform.localScale.y);
        });

        LeanTween.value(gameObject, transform.localScale.y, transform.localScale.y * 1.1f, timeToScale).setOnUpdate((float val) => {
            transform.localScale = new Vector2(transform.localScale.x, val);
        });

        yield return new WaitForSeconds(timeToScale);

        LeanTween.value(gameObject, transform.localScale.x, initScaleX, timeToScale).setOnUpdate((float val) => {
            transform.localScale = new Vector2(val, transform.localScale.y);
        });

        LeanTween.value(gameObject, transform.localScale.y, initScaleY, timeToScale).setOnUpdate((float val) => {
            transform.localScale = new Vector2(transform.localScale.x, val);
        });
    }
}