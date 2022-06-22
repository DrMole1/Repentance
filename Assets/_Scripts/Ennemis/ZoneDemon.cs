using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDemon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aire d'aggro
        if (other.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<DemonBehaviour>().isFighting = true;
            transform.parent.gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quand le joueur quitte l'aire d'aggro
        if (other.gameObject.tag == "Player")
        {
            transform.parent.gameObject.GetComponent<DemonBehaviour>().isFighting = false;
            transform.parent.gameObject.GetComponent<Animator>().enabled = false;
        }
    }
}
