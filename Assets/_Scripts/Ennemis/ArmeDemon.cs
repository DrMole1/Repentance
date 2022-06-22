using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ArmeDemon : MonoBehaviour
{
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 0.05f;
    [Space]
    public GameObject ptcFantomePrefab;
    public GameObject ptcDamagePrefab;
    public GameObject gameController;
    public GameObject orb;
    public GameObject ptcEvaporationPrefab;

    private GameObject globalLight;
    private CameraShake cameraShake;
    private bool canDestroyOrb = true;



    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        transform.GetChild(1).gameObject.SetActive(false);
        orb = GameObject.Find("bouleAcceptation");

        // Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aberration
        if (other.gameObject.tag == "Player")
        {
            //On joue le son de dégats au joueur
            _audioManager.PlayAberrationTouch();

            // On baisse la luminosité globale
            globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity -= damage;

            // On repousse UNIQUEMENT le joueur
            transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(StopRepousse());

            // On mets une particule de dégat
            GameObject ptcFantomeInstance;
            ptcFantomeInstance = Instantiate(ptcFantomePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(ptcFantomeInstance, 2f);

            // On met une secousse de Camera
            cameraShake.HurtByMonster();
        }


        // Quand une boule touche l'aberration
        if (other.gameObject.tag == "Boule" && canDestroyOrb)
        {
            StartCoroutine(ReactivateOrb());
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            //On fait jouer le son d'évaporation
            other.gameObject.GetComponent<FloatingObjects>().PlaySound();

            // On mets une particule d'évaporation
            GameObject ptcEvaporationInstance;
            ptcEvaporationInstance = Instantiate(ptcEvaporationPrefab, other.gameObject.transform.position, Quaternion.identity);
            ptcEvaporationInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(ptcEvaporationInstance, 2f);

            canDestroyOrb = false;
        }
    }


    IEnumerator StopRepousse()
    {
        yield return new WaitForSeconds(0.2f);

        transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator ReactivateOrb()
    {
        yield return new WaitForSeconds(3f);

        orb.transform.GetChild(0).gameObject.SetActive(true);
        orb.GetComponent<CircleCollider2D>().enabled = true;
        canDestroyOrb = true;
    }
}
