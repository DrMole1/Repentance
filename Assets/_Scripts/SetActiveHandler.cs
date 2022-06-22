using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveHandler : MonoBehaviour
{

    public GameObject goToDisappear;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche la zone d'activation
        if (other.gameObject.tag == "Player")
        {
            goToDisappear.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quand le joueur sort de la zone d'activation
        if (other.gameObject.tag == "Player")
        {
            goToDisappear.SetActive(false);
        }
    }
}
