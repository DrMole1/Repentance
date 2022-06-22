using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Pic : MonoBehaviour
{

    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 0.5f;
    [Space]
    public GameObject ptcFantomePrefab;

    // Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    private GameObject globalLight;
    private CameraShake cameraShake;



    void Awake()
    {
        // Gère la luminosité
        globalLight = GameObject.Find("LumiereLanterneGlobale");

        // Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Joue le son de dégats au joueur
            _audioManager.PlayMonstreToucheJoueur();


            // On baisse la luminosité globale
            globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity -= damage;

            // On mets une particule de dégat
            GameObject ptcFantomeInstance;
            ptcFantomeInstance = Instantiate(ptcFantomePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(ptcFantomeInstance, 2f);

            // On met une secousse de Camera
            cameraShake.HurtByMonster();
        }
    }
}
