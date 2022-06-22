using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedAgroArea : MonoBehaviour
{
    private bool once = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aire d'aggro
        if (other.gameObject.tag == "Player" && once)
        {
            once = false;

            StartCoroutine(DelayStart());
        }
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.GetComponent<HauntedSpiritBehaviour>().WakeUp();
    }
}
