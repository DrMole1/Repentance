using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomber_Plateformes : MonoBehaviour
{
    public GameObject plateforme;
    public Transform ouAller;

    public Rigidbody2D rb;

    [Header("Particule quand touché")]
    public ParticleSystem exp;
    [Header("Particule qui reste sur la zone")]
    public ParticleSystem particuleAmbiante;

    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;


    private bool once = false;

    private void Start()
    {
        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boule") && once == false)
        {
            //Joue le son de destruction de chaine
            _audioManager.PlayDetruireChaine();

            rb.bodyType = RigidbodyType2D.Dynamic;
            ParticleSystem x = Instantiate(exp, transform.position, Quaternion.identity);
            plateforme.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            LeanTween.moveY(plateforme, ouAller.position.y, 1f);
            Destroy(particuleAmbiante);
            once = true;
        }
    }
}
