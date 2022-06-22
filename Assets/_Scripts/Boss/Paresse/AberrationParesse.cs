using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AberrationParesse : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aberration
        if (other.gameObject.tag == "Ground")
        {
            transform.parent.SetParent(other.gameObject.transform);
            transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}