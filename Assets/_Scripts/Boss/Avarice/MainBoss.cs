using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class MainBoss : MonoBehaviour
{
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    private Transform perso;
    [Header("Nombre de coups de boule que doit se prendre l'ennemi")]
    public int vie = 5;
    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 0.1f;
    [Header("Nombre d'ame que lache l'aberration une fois détruire")]
    public int nbAme = 1;
    [Space]
    public GameObject ptcFantomePrefab;
    public GameObject ptcDamagePrefab;
    public GameObject gameController;
    public GameObject ame;
    public GameObject picPrefab;
    [Header("1 for left, 2 for right, 3 for top")]
    public int type = 0;
    public GameObject ptcBigRockPrefab;

    GameObject globalLight;
    private float yPosPlayer = 0f;
    private float yPosInitCage = 0f;
    private CameraShake cameraShake;

    void Awake()
    {
        perso = GameObject.Find("Player").GetComponent<Transform>();
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        transform.GetChild(0).gameObject.SetActive(false);

        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche la main du boss
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

                transform.parent.gameObject.GetComponent<BossAvarice>().NextPhase();
                Destroy(gameObject);
            }
        }
    }


    IEnumerator StopRepousse()
    {
        yield return new WaitForSeconds(0.2f);

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetPhase1()
    {
        StartCoroutine(SetPhase1Coroutine());
    }

    IEnumerator SetPhase1Coroutine()
    {
        if (transform.parent.gameObject.GetComponent<BossAvarice>().phase == 1)
        {
            if(type != 3)
            {
                LeanTween.value(gameObject, 10f, 0f, 3f).setOnUpdate((float val) =>
                {
                    if(type == 1)
                    {
                        val *= -1;
                    }
                    
                    transform.position = new Vector2(transform.parent.position.x + val, transform.position.y);
                });

                yield return new WaitForSeconds(3f);

                LeanTween.value(gameObject, 0f, 10f, 3f).setOnUpdate((float val) =>
                {
                    if (type == 1)
                    {
                        val *= -1;
                    }

                    transform.position = new Vector2(transform.parent.position.x + val, transform.position.y);
                });

                yield return new WaitForSeconds(3f);

                StartCoroutine(SetPhase1Coroutine());
            }
            else
            {
                yield return new WaitForSeconds(5f);

                StartCoroutine(SetPhase1Coroutine());
            }
        }
        else if (transform.parent.gameObject.GetComponent<BossAvarice>().phase == 2)
        {
            if (type != 3)
            {
                LeanTween.value(gameObject, 10f, -10f, 5f).setOnUpdate((float val) =>
                {
                    if (type == 1)
                    {
                        val *= -1;
                    }

                    transform.position = new Vector2(transform.parent.position.x + val, transform.position.y);
                });

                yield return new WaitForSeconds(5f);

                LeanTween.value(gameObject, -10f, 10f, 5f).setOnUpdate((float val) =>
                {
                    if (type == 1)
                    {
                        val *= -1;
                    }

                    transform.position = new Vector2(transform.parent.position.x + val, transform.position.y);
                });

                yield return new WaitForSeconds(5f);

                StartCoroutine(SetPhase1Coroutine());
            }
            
            if(type == 3)
            {
                yPosInitCage = transform.position.y;

                LeanTween.value(gameObject, transform.position.x, perso.position.x, 0.75f).setOnUpdate((float val) =>
                {
                    transform.position = new Vector2(val, transform.position.y);
                });

                yPosPlayer = perso.transform.position.y;

                yield return new WaitForSeconds(0.75f);

                LeanTween.value(gameObject, transform.position.y, transform.position.y + 1, 0.75f).setOnUpdate((float val) =>
                {
                    transform.position = new Vector2(transform.position.x, val);
                });

                yield return new WaitForSeconds(0.75f);

                LeanTween.value(gameObject, transform.position.y, transform.position.y - 8, 0.75f).setOnUpdate((float val) =>
                {
                   transform.position = new Vector2(transform.position.x, val);
                });

                yield return new WaitForSeconds(0.75f);

                cameraShake.CageAvarice();
                // spawn des particules big rock
                GameObject ptcBigRockInstance;
                ptcBigRockInstance = Instantiate(ptcBigRockPrefab, new Vector2(transform.position.x, transform.position.y - 11f), Quaternion.identity);
                Destroy(ptcBigRockInstance, 5f);
                GetComponent<AudioSource>().Play();

                yield return new WaitForSeconds(1.25f);

                LeanTween.value(gameObject, transform.position.y, yPosInitCage, 0.75f).setOnUpdate((float val) =>
                {
                    transform.position = new Vector2(transform.position.x, val);
                });

                yield return new WaitForSeconds(0.75f);

                StartCoroutine(SetPhase1Coroutine());
            }
        }
        else if (transform.parent.gameObject.GetComponent<BossAvarice>().phase == 3)
        {
            if (type != 3)
            {
                LeanTween.value(gameObject, 10f, -10f, 3f).setOnUpdate((float val) =>
                {
                    if (type == 1)
                    {
                        val *= -1;
                    }

                    transform.position = new Vector2(transform.parent.position.x + val, transform.position.y);
                });

                yield return new WaitForSeconds(3f);

                LeanTween.value(gameObject, -10f, 10f, 3f).setOnUpdate((float val) =>
                {
                    if (type == 1)
                    {
                        val *= -1;
                    }

                    transform.position = new Vector2(transform.parent.position.x + val, transform.position.y);
                });

                yield return new WaitForSeconds(3f);

                StartCoroutine(SetPhase1Coroutine());
            }

            if (type == 3)
            {
                yPosInitCage = transform.position.y;

                LeanTween.value(gameObject, transform.position.x, perso.position.x, 0.5f).setOnUpdate((float val) =>
                {
                    transform.position = new Vector2(val, transform.position.y);
                });

                yPosPlayer = perso.transform.position.y;

                yield return new WaitForSeconds(0.5f);

                LeanTween.value(gameObject, transform.position.y, transform.position.y + 1, 0.2f).setOnUpdate((float val) =>
                {
                    transform.position = new Vector2(transform.position.x, val);
                });

                yield return new WaitForSeconds(0.2f);

                LeanTween.value(gameObject, transform.position.y, transform.position.y - 12, 0.5f).setOnUpdate((float val) =>
                {
                    transform.position = new Vector2(transform.position.x, val);
                });

                yield return new WaitForSeconds(0.5f);

                cameraShake.CageAvarice();
                // spawn des particules big rock
                GameObject ptcBigRockInstance;
                ptcBigRockInstance = Instantiate(ptcBigRockPrefab, new Vector2(transform.position.x, transform.position.y - 2f), Quaternion.identity);
                Destroy(ptcBigRockInstance, 5f);
                GetComponent<AudioSource>().Play();

                yield return new WaitForSeconds(0.5f);

                LeanTween.value(gameObject, transform.position.y, yPosInitCage, 0.5f).setOnUpdate((float val) =>
                {
                    transform.position = new Vector2(transform.position.x, val);
                });

                yield return new WaitForSeconds(0.5f);

                StartCoroutine(SetPhase1Coroutine());
            }
        }
    }
}