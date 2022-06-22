using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCamera : MonoBehaviour
{
    public float size = 0f;

    private CameraShake cameraShake;

    void Awake()
    {
        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur entre dans une grotte
        if (other.gameObject.tag == "Player")
        {
            // On agrandit la caméra
            cameraShake.EnterCave(size);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quand le joueur sort d'une grotte
        if (other.gameObject.tag == "Player")
        {
            // On agrandit la caméra
            cameraShake.ExitCave(size);
        }
    }
}
