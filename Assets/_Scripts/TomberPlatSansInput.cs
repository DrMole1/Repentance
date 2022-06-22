using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomberPlatSansInput : MonoBehaviour
{
    public GameObject ptcTrigger;
    bool once = true;

    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    private CameraShake cameraShake;

    private void Start()
    {
        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && once)
        {
            _audioManager.PlayPlateformeTombe();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;

            Instantiate(ptcTrigger, transform.position, Quaternion.identity);

            once = false;

            cameraShake.TouchFallingPlatform();
        }
    }
}
