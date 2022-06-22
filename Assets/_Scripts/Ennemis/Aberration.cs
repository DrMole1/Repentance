using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Aberration : MonoBehaviour
{
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    [Header("Nombre de coups de boule que doit se prendre l'ennemi")]
    public int vie = 3;
    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 0.1f;
    [Header("Nombre d'ame que lache l'aberration une fois détruire")]
    public int nbAme = 5;
    [Space]
    public GameObject ptcFantomePrefab;
    public GameObject ptcDamagePrefab;
    public GameObject gameController;
    public GameObject ame;
    public GameObject brokenAberration;
    public GameObject ptcMoreDamagePrefab;

    GameObject globalLight;
    private CameraShake cameraShake;

    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        transform.GetChild(0).gameObject.SetActive(false);

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
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(StopRepousse());

            // On mets une particule de dégat
            GameObject ptcFantomeInstance;
            ptcFantomeInstance = Instantiate(ptcFantomePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(ptcFantomeInstance, 2f);

            // On met une secousse de Camera
            cameraShake.HurtByMonster();
        }


        // Quand une boule touche l'aberration
        if (other.gameObject.tag == "Boule")
        {
            //On joue le son de dégats à l'aberration
            _audioManager.PlayFaireDegats();

            // Baisse de la vie
            vie--;

            // Particule de dégâts
            GameObject ptcDamageInstance;
            ptcDamageInstance = Instantiate(ptcDamagePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(ptcDamageInstance, 2f);

            // Particule de dégâts More Damage
            GameObject ptcMoreDamageInstance;
            ptcMoreDamageInstance = Instantiate(ptcMoreDamagePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(ptcMoreDamageInstance, 2f);

            // Diminution du temps
            GameObject.Find("_GameController").GetComponent<TimeManager>().DoSlowMotion();  

            // Si l'aberration n'a plus de vie
            if(vie == 0)
            {
                // Fait spawn les ames
                for(int i = 0; i < nbAme; i++)
                {
                    GameObject ameInstance = Instantiate(ame, transform.position, Quaternion.identity);

                    // Ajout d'une force à l'item
                    float forceX = 0;
                    forceX = Random.Range(-1.5f, 1.5f);
                    ameInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 1.5f), ForceMode2D.Impulse);
                }

                // Instantie le prefab cassé
                GameObject aberrationBrokenInstance;
                aberrationBrokenInstance = Instantiate(brokenAberration, transform.position, Quaternion.identity);
                aberrationBrokenInstance.transform.localScale = transform.localScale;

                Destroy(gameObject);
            }
        }
    }


    IEnumerator StopRepousse()
    {
        yield return new WaitForSeconds(0.2f);

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
