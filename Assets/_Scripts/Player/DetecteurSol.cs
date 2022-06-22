using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecteurSol : MonoBehaviour
{
    public bool isTouchingGround = true;

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quand le détecteur touche le sol
        if (other.gameObject.tag == "Ground")
            isTouchingGround = false;
    }
}
