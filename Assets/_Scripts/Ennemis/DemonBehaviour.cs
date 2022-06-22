using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DemonBehaviour : MonoBehaviour
{
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    [Header("Nombre de coups de boule que doit se prendre l'ennemi")]
    public int vie = 3;
    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 0.05f;
    [Header("Nombre d'ame que lache l'aberration une fois détruire")]
    public int nbAme = 2;
    [Space]
    public GameObject ptcFantomePrefab;
    public GameObject ptcDamagePrefab;
    public GameObject gameController;
    public GameObject ame;
    public GameObject flaque;
    public GameObject ptcFantomeBigPrefab;
    public GameObject ptcMonsterKilledPrefab;
    [Header("Sound")]
    public GameObject soundMortPrefab;
    public GameObject soundFrappePrefab;
    public GameObject soundFrappePrefab2;
    public GameObject soundDamagePrefab;


    private GameObject globalLight;
    private CameraShake cameraShake;
    private Animator animator;

    private float initPosX;
    [HideInInspector] public bool isFighting = false;



    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        transform.GetChild(0).gameObject.SetActive(false);

        initPosX = transform.position.x;

        // Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();

        // Gère l'animator
        animator = GetComponent<Animator>();

    }



    void Start()
    {
        StartCoroutine(DeplacerGauche());
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche le démon
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


        // Quand une boule touche le démon
        if (other.gameObject.tag == "Boule")
        {
            //On joue le son de dégats au démon
            _audioManager.PlayFaireDegats();

            // Baisse de la vie
            vie--;

            // Particule de dégâts
            GameObject ptcDamageInstance;
            ptcDamageInstance = Instantiate(ptcDamagePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(ptcDamageInstance, 2f);

            // Diminution du temps
            GameObject.Find("_GameController").GetComponent<TimeManager>().DoSlowMotion();

            // Si l'aberration n'a plus de vie
            if (vie == 0)
            {
                // Fait spawn les ames
                for (int i = 0; i < nbAme; i++)
                {
                    GameObject ameInstance = Instantiate(ame, transform.position, Quaternion.identity);

                    // Ajout d'une force à l'item
                    float forceX = 0;
                    forceX = Random.Range(-1.5f, 1.5f);
                    ameInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 1.5f), ForceMode2D.Impulse);
                }

                // Spawn une flaque
                float zRotation = Random.Range(-60, 60);
                float randomScale = Random.Range(2, 4);
                GameObject flaqueInstance;
                flaqueInstance = Instantiate(flaque, new Vector2(transform.position.x + 0.3f, transform.position.y - 1.2f), Quaternion.Euler(0, 0, zRotation));
                flaqueInstance.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                // On mets une particule de mort
                GameObject ptcFantomeBigInstance;
                ptcFantomeBigInstance = Instantiate(ptcFantomeBigPrefab, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
                Destroy(ptcFantomeBigInstance, 2f);

                // On mets une secondes particule de mort
                GameObject ptcMonsterKilledInstance;
                ptcMonsterKilledInstance = Instantiate(ptcMonsterKilledPrefab, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
                Destroy(ptcMonsterKilledInstance, 5f);

                if (isFighting)
                {
                    // Son de mort
                    GameObject soundMortInstance;
                    soundMortInstance = Instantiate(soundMortPrefab, transform.position, Quaternion.identity);
                    Destroy(soundMortInstance, 5f);
                }

                Destroy(gameObject);
            }
            else
            {
                if (isFighting)
                {
                    // Son de damage
                    GameObject soundDamageInstance;
                    soundDamageInstance = Instantiate(soundDamagePrefab, transform.position, Quaternion.identity);
                    Destroy(soundDamageInstance, 5f);
                }
            }
        }
    }


    IEnumerator StopRepousse()
    {
        yield return new WaitForSeconds(0.2f);

        transform.GetChild(0).gameObject.SetActive(false);
    }


    IEnumerator DeplacerGauche()
    {
        animator.SetInteger("Etat", 1);

        float randomTime = 0f;
        float newPos = 0f;
        randomTime = Random.Range(1f, 2f);
        newPos = transform.position.x - (randomTime * 2);

        LeanTween.value(gameObject, transform.position.x, newPos, randomTime).setOnUpdate((float val) =>
        {
            transform.position = new Vector2(val, transform.position.y);
        });

        yield return new WaitForSeconds(randomTime);

        StartCoroutine(Frapper());
    }

    IEnumerator DeplacerDroite()
    {
        animator.SetInteger("Etat", 2);

        float randomTime = 0f;
        float newPos = 0f;
        randomTime = Random.Range(1f, 2f);
        newPos = transform.position.x + (randomTime * 2);

        LeanTween.value(gameObject, transform.position.x, newPos, randomTime).setOnUpdate((float val) =>
        {
            transform.position = new Vector2(val, transform.position.y);
        });

        yield return new WaitForSeconds(randomTime);

        StartCoroutine(Frapper());
    }

    IEnumerator Frapper()
    {
        animator.SetInteger("Etat", 3);

        if(isFighting)
        {
            int cas = 0;
            cas = Random.Range(0, 2);
            if(cas == 0)
            {
                // Son de frappe
                GameObject soundFrappeInstance;
                soundFrappeInstance = Instantiate(soundFrappePrefab, transform.position, Quaternion.identity);
                Destroy(soundFrappeInstance, 5f);
            }
            else
            {
                // Son de frappe
                GameObject soundFrappeInstance2;
                soundFrappeInstance2 = Instantiate(soundFrappePrefab2, transform.position, Quaternion.identity);
                Destroy(soundFrappeInstance2, 5f);
            }
        }

        yield return new WaitForSeconds(2f);

        if(transform.position.x < initPosX)
        {
            StartCoroutine(DeplacerDroite());
        }
        else
        {
            StartCoroutine(DeplacerGauche());
        }
    }
}
