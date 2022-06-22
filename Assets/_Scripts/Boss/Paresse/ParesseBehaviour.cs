using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ParesseBehaviour : MonoBehaviour
{
    // Déclaration des variables
    // =============================================
    [Header("Paramètres numériques")]
    public int phase = 0;

    [HideInInspector] public bool isStart = false;
    [HideInInspector] public int life = 13;

    [Header("Membres du boss")]
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject leftEyeFight;
    public GameObject rightEyeFight;
    public GameObject jaw;
    public GameObject jawFight;
    public GameObject stump;
    public GameObject topHeadFight;
    public GameObject bodyFight;

    [Header("Attaques")]
    public GameObject attaque1Prefab;
    public GameObject attaque2Prefab;
    public GameObject aberrationPrefab;

    [Header("Sprites")]
    public Sprite jaw2;
    public Sprite topHead2;
    public Sprite body2;
    public Sprite jaw3;
    public Sprite topHead3;
    public Sprite body3;


    [Header("Autres")]
    public GameObject blocScene1;
    public GameObject blocScene2;
    public GameObject cadreBoss;
    public GameObject player;
    public GameObject wheel;
    public GameObject ptcBugPrefab;
    public GameObject globalLight;
    public AudioSource mainTheme;
    public AudioSource church;
    public GameObject ennemies;
    public GameObject traps;
    public GameObject grass;
    public GameObject ptcExplosionPrefab;

    private CameraShake cameraShake;
    // =============================================



    void Awake()
    {
        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
    }



    // Quand le joueur active le boss
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isStart == false)
        {
            isStart = true;

            // Changement de musique et son
            mainTheme.Stop();
            GetComponent<AudioSource>().Play();
            church.Play();

            // Camera shake
            cameraShake.StartBoss1();

            // Yeux set up
            leftEye.GetComponent<SpriteRenderer>().enabled = true;
            rightEye.GetComponent<SpriteRenderer>().enabled = true;

            leftEye.GetComponent<FollowingEyes>().SetUp();
            rightEye.GetComponent<FollowingEyes>().SetUp();
            leftEyeFight.GetComponent<FollowingEyes>().SetUp();
            rightEyeFight.GetComponent<FollowingEyes>().SetUp();

            // Montée des blocs scene
            LeanTween.value(gameObject, blocScene1.transform.localPosition.y, blocScene1.transform.localPosition.y + 3, 4f).setOnUpdate((float val) =>
            {
                blocScene1.transform.localPosition = new Vector2(blocScene1.transform.localPosition.x, val);
                blocScene2.transform.localPosition = new Vector2(blocScene2.transform.localPosition.x, val);
            });

            // Descente de la gueule du boss
            LeanTween.value(gameObject, jaw.transform.localPosition.y, jaw.transform.localPosition.y - 0.6f, 4f).setOnUpdate((float val) =>
            {
                jaw.transform.localPosition = new Vector2(jaw.transform.localPosition.x, val);
            });

            // Ptc insectes
            GameObject ptcBugInstance;
            ptcBugInstance = Instantiate(ptcBugPrefab, new Vector2(-350.5f, 32.5f), transform.rotation, jaw.transform);
            Destroy(ptcBugInstance, 10f);

            // Désactivation des ennemis et des pièges de la map par soucis de performance
            ennemies.SetActive(false);
            traps.SetActive(false);

            StartCoroutine(StartBoss());
        }
    }



    // Aiguillage des phases selon la vie du boss
    void Update()
    {
        if(stump != null)
        {
            life = stump.GetComponent<Aberration>().vie;

            if (phase == 1 && life == 9) //9
            {
                phase = 2;
                church.Play();
                jawFight.GetComponent<SpriteRenderer>().sprite = jaw2;
                topHeadFight.GetComponent<SpriteRenderer>().sprite = topHead2;
                bodyFight.GetComponent<SpriteRenderer>().sprite = body2;
            }
            else if (phase == 2 && life == 3) //3
            {
                phase = 3;
                church.Play();
            }
        }
        else if(phase == 3)
        {
            phase = 4;
            StartCoroutine(Ending());
        }
    }



    IEnumerator StartBoss()
    {
        // Brillance du cadre boss
        LeanTween.value(gameObject, 0f, 4f, 5f).setOnUpdate((float val) =>
        {
            cadreBoss.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        yield return new WaitForSeconds(5f);

        LeanTween.value(gameObject, 2f, 0f, 2f).setOnUpdate((float val) =>
        {
            cadreBoss.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        yield return new WaitForSeconds(1f);

        LeanTween.value(gameObject, 0.1f, 30f, 1f).setOnUpdate((float val) =>
        {
            globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        jawFight.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1f);

        // Téléportation du joueur dans la roue
        player.transform.position = new Vector2(-337f, 88f);
        globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 0.1f;

        // Mise en place de la phase 1
        phase = 1;
        wheel.GetComponent<Roue>().speed = -0.8f;
        StartCoroutine(Phase1());
    }



    IEnumerator Phase1()
    {
        yield return new WaitForSeconds(5f);

        // Descente de la gueule du boss
        LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y - 0.53f, 1f).setOnUpdate((float val) =>
        {
            jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
        });

        yield return new WaitForSeconds(1f);

        int attaque = Random.Range(0, 2);
        if (attaque == 0)
        {
            jawFight.GetComponent<AudioSource>().Play();
            // Instantiation de l'attaque 01
            GameObject attaque1Instance;
            attaque1Instance = Instantiate(attaque1Prefab, new Vector2(-346f, 93f), transform.rotation);
            Destroy(attaque1Instance, 10f);
        }
        else if (attaque == 1)
        {
            jawFight.GetComponent<AudioSource>().Play();
            // Instantiation de l'attaque 02
            GameObject attaque2Instance;
            attaque2Instance = Instantiate(attaque2Prefab, new Vector2(-346f, 93f), transform.rotation);
            Destroy(attaque2Instance, 10f);
        }

        // Remontée de la gueule du boss
        LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y + 0.53f, 1f).setOnUpdate((float val) =>
        {
            jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
        });

        if (phase == 1)
        {
            StartCoroutine(Phase1());
        }
        else if (phase == 2)
        {
            StartCoroutine(Phase2());
            wheel.GetComponent<Roue>().speed = -1f;
            wheel.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("isAnim", true);
        }
    }



    IEnumerator Phase2()
    {
        yield return new WaitForSeconds(8.5f);

        // Descente de la gueule du boss
        LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y - 0.53f, 1f).setOnUpdate((float val) =>
        {
            jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
        });

        yield return new WaitForSeconds(1f);

        // Attaque du boss de la phase 2 = lancée d'aberrations
        float speed = 0f;
        int attaque = Random.Range(0, 2);
        if (attaque == 0)
        {
            speed = 300f;
        }
        else
        {
            speed = 1500f;
        }

        jawFight.GetComponent<AudioSource>().Play();
        GameObject aberrationInstance;
        aberrationInstance = Instantiate(aberrationPrefab, new Vector2(-346f, 93f), transform.rotation);
        aberrationInstance.GetComponent<Rigidbody2D>().AddForce(aberrationInstance.transform.right * speed);
        aberrationInstance.GetComponent<Rigidbody2D>().AddTorque(900f, ForceMode2D.Impulse);


        // Remontée de la gueule du boss
        LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y + 0.53f, 1f).setOnUpdate((float val) =>
        {
            jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
        });

        if (phase == 2)
        {
            StartCoroutine(Phase2());
        }
        else if (phase == 3)
        {
            yield return new WaitForSeconds(1f);

            // Descente de la gueule du boss
            LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y - 0.53f, 1f).setOnUpdate((float val) =>
            {
                jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
            });


            yield return new WaitForSeconds(1f);

            jawFight.GetComponent<AudioSource>().Play();
            //Explosion de lumière
            LeanTween.value(gameObject, 0.1f, 30f, 1f).setOnUpdate((float val) =>
            {
                globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
            });

            yield return new WaitForSeconds(1f);

            // Changement des sprites du boss
            jawFight.GetComponent<SpriteRenderer>().sprite = jaw3;
            topHeadFight.GetComponent<SpriteRenderer>().sprite = topHead3;
            bodyFight.GetComponent<SpriteRenderer>().sprite = body3;

            // Retour à une luminosité normale
            LeanTween.value(gameObject, 30f, 0.1f, 0.2f).setOnUpdate((float val) =>
            {
                globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
            });

            // Remontée de la gueule du boss
            LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y + 0.53f, 1f).setOnUpdate((float val) =>
            {
                jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
            });

            StartCoroutine(Phase3());

            // Variation progressive de la vitesse
            LeanTween.value(gameObject, -1f, 1.2f, 5f).setOnUpdate((float val) =>
            {
                wheel.GetComponent<Roue>().speed = val;
            });
        }
    }



    IEnumerator Phase3()
    {
        yield return new WaitForSeconds(8.5f);

        if(phase == 3)
        {
            // Descente de la gueule du boss
            LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y - 0.53f, 1f).setOnUpdate((float val) =>
            {
                jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
            });

            yield return new WaitForSeconds(1f);

            // Attaque du boss de la phase 2 = lancée d'aberrations
            float speed = 0f;
            int attaque = Random.Range(0, 2);
            if (attaque == 0)
            {
                speed = 300f;
            }
            else
            {
                speed = 1500f;
            }

            jawFight.GetComponent<AudioSource>().Play();
            GameObject aberrationInstance;
            aberrationInstance = Instantiate(aberrationPrefab, new Vector2(-346f, 93f), transform.rotation);
            aberrationInstance.GetComponent<Rigidbody2D>().AddForce(aberrationInstance.transform.right * speed);
            aberrationInstance.GetComponent<Rigidbody2D>().AddTorque(900f, ForceMode2D.Impulse);


            // Remontée de la gueule du boss
            LeanTween.value(gameObject, jawFight.transform.localPosition.y, jawFight.transform.localPosition.y + 0.53f, 1f).setOnUpdate((float val) =>
            {
                jawFight.transform.localPosition = new Vector2(jawFight.transform.localPosition.x, val);
            });
        }

        if(phase == 3)
        {
            StartCoroutine(Phase3());
        }
    }


    IEnumerator Ending()
    {
        if(phase == 4)
        {
            jawFight.GetComponent<AudioSource>().Play();
            // Explosions de fin de boss
            for (int i = 0; i < 30; i++)
            {
                GameObject ptcExplosionInstance;
                float randomX = Random.Range(-355f, -325f);
                float randomY = Random.Range(105f, 80f);
                ptcExplosionInstance = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
                Destroy(ptcExplosionInstance, 5f);

                GameObject ptcExplosionInstance2;
                randomX = Random.Range(-355f, -325f);
                randomY = Random.Range(105f, 80f);
                ptcExplosionInstance2 = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
                Destroy(ptcExplosionInstance2, 5f);

                GameObject ptcExplosionInstance3;
                randomX = Random.Range(-355f, -325f);
                randomY = Random.Range(105f, 80f);
                ptcExplosionInstance3 = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
                Destroy(ptcExplosionInstance3, 5f);

                yield return new WaitForSeconds(0.1f);
            }

            // Téléportation du joueur, réactivation de la musique et des obstacles, et destruction du boss
            GetComponent<AudioSource>().Stop();
            mainTheme.Play();
            church.Play();

            ennemies.SetActive(true);
            traps.SetActive(true);

            player.transform.position = new Vector2(-350f, 27f);

            Destroy(gameObject);
        }
    }
}
