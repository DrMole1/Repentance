using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class BossAvarice : MonoBehaviour
{
    // Déclaration des variables
    // ===========================================
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject cadreBoss;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject topHand;
    public GameObject murFermeture;
    public Sprite bossAvarice2;
    public Sprite bossAvarice3;
    public Sprite bossAvarice4;
    public GameObject picPrefab;
    public GameObject diamantPrefab;
    public GameObject diamantAberrationPrefab;
    public Traveling[] spiritBehaviour;
    public AudioSource mainTheme;
    public AudioSource clocher;
    public AudioSource pic;
    public Transform[] spirit;
    public GameObject eclair;
    public GameObject aura;


    [HideInInspector] public int phase = 0;
    private CameraShake cameraShake;
    private bool isStarted = false;
    // ===========================================

    void Awake()
    {
        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche la zone de début du boss
        if (other.gameObject.tag == "Player" && isStarted == false)
        {
            //Ferme l'arène (Platforme bloc 299)
            LeanTween.moveY(murFermeture, 0f, 2f);
            StartCoroutine(StartBoss());
            isStarted = true;
        }
    }

    public void NextPhase()
    {
        phase++;

        if(phase == 2)
        {
            clocher.Play();
            gameObject.GetComponent<SpriteRenderer>().sprite = bossAvarice2;
        }
        else if (phase == 3)
        {
            clocher.Play();
            gameObject.GetComponent<SpriteRenderer>().sprite = bossAvarice3;
        }
    }


    IEnumerator StartBoss()
    {
        // Apparition des esprits du boss
        spiritBehaviour[0].StartSpirit();
        spiritBehaviour[1].StartSpirit();
        spiritBehaviour[2].StartSpirit();
        spiritBehaviour[3].StartSpirit();

        // Changement de musique et son
        mainTheme.Stop();
        GetComponent<AudioSource>().Play();
        clocher.Play();

        cameraShake.StartBoss1();

        LeanTween.value(gameObject, -19f, -11f, 5f).setOnUpdate((float val) => {
            transform.position = new Vector2(transform.position.x, val);
        });

        LeanTween.value(gameObject, 0f, 4f, 5f).setOnUpdate((float val) => {
            cadreBoss.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        yield return new WaitForSeconds(5f);

        LeanTween.value(gameObject, 2f, 0f, 2f).setOnUpdate((float val) => {
            cadreBoss.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });

        leftEye.GetComponent<FollowingEyes>().SetUp();
        rightEye.GetComponent<FollowingEyes>().SetUp();
        leftEye.GetComponent<FollowingEyes>().stop = false;
        rightEye.GetComponent<FollowingEyes>().stop = false;

        yield return new WaitForSeconds(2f);

        phase = 1;

        leftHand.GetComponent<MainBoss>().SetPhase1();
        rightHand.GetComponent<MainBoss>().SetPhase1();
        topHand.GetComponent<MainBoss>().SetPhase1();

        StartCoroutine(AttaquePhase1());
    }



    IEnumerator AttaquePhase1()
    {
        yield return new WaitForSeconds(6f);

        int cas = Random.Range(0, 2);

        if(transform.childCount == 9)
        {
            if (cas == 0)
            {
                Instantiate(picPrefab, transform.GetChild(5).GetChild(0).position, Quaternion.identity);
                Instantiate(picPrefab, transform.GetChild(5).GetChild(1).position, Quaternion.identity);
                Instantiate(picPrefab, transform.GetChild(5).GetChild(2).position, Quaternion.identity);
                Instantiate(picPrefab, transform.GetChild(5).GetChild(3).position, Quaternion.identity);
            }
            else
            {
                Instantiate(picPrefab, transform.GetChild(6).GetChild(0).position, Quaternion.identity);
                Instantiate(picPrefab, transform.GetChild(6).GetChild(1).position, Quaternion.identity);
                Instantiate(picPrefab, transform.GetChild(6).GetChild(2).position, Quaternion.identity);
            }
        }

        yield return new WaitForSeconds(2f);

        pic.Play();
        
        if (phase == 1)
        {
            StartCoroutine(AttaquePhase1());
        }
        else if(phase == 2)
        {
            StartCoroutine(AttaquePhase2());
        }
    }


    IEnumerator AttaquePhase2()
    {
        yield return new WaitForSeconds(4f);

        int cas = Random.Range(0, 2);

        if (transform.childCount == 8)
        {
            if (cas == 0)
            {
                Instantiate(picPrefab, transform.GetChild(6).GetChild(0).position, Quaternion.identity);
                Instantiate(picPrefab, transform.GetChild(6).GetChild(1).position, Quaternion.identity);

                yield return new WaitForSeconds(2f);

                if (transform.childCount == 8)
                {
                    pic.Play();

                    Instantiate(picPrefab, transform.GetChild(6).GetChild(2).position, Quaternion.identity);
                    Instantiate(picPrefab, transform.GetChild(6).GetChild(3).position, Quaternion.identity);

                    yield return new WaitForSeconds(2f);

                    pic.Play();                  
                }
            }
            else
            {
                Instantiate(picPrefab, transform.GetChild(7).GetChild(0).position, Quaternion.identity);
                Instantiate(picPrefab, transform.GetChild(7).GetChild(1).position, Quaternion.identity);

                yield return new WaitForSeconds(2f);

                if (transform.childCount == 8)
                {
                    pic.Play();

                    Instantiate(picPrefab, transform.GetChild(7).GetChild(2).position, Quaternion.identity);
                    Instantiate(picPrefab, transform.GetChild(7).GetChild(3).position, Quaternion.identity);
                    Instantiate(picPrefab, transform.GetChild(7).GetChild(4).position, Quaternion.identity);

                    yield return new WaitForSeconds(2f);

                    pic.Play();                
                }
            }
        }


        if (phase == 2)
        {
            StartCoroutine(AttaquePhase2());
        }
        else if (phase == 3)
        {
            StartCoroutine(AttaquePhase3());
        }
    }


    IEnumerator AttaquePhase3()
    {
        yield return new WaitForSeconds(2f);

        GameObject diamantInstance;
        diamantInstance = Instantiate(diamantPrefab, new Vector2(-421f, -8f), Quaternion.identity);
    }

    public void StartMadPhase()
    {
        StartCoroutine(DelayMadPhase());
        StartCoroutine(SpawnEclair());
        clocher.Play();

        gameObject.GetComponent<SpriteRenderer>().sprite = bossAvarice4;
        Destroy(rightEye);
        Destroy(leftEye);

        //Agrandissement caméra
        cameraShake.MadPhaseBoss1();
    }

    IEnumerator DelayMadPhase()
    {
        yield return new WaitForSeconds(2f);

        StartCoroutine(MadPhase());
    }

    IEnumerator MadPhase()
    {
        yield return new WaitForSeconds(0.8f);

        int randomSpirit = Random.Range(0, 4);

        GameObject diamantAberrationInstance;
        diamantAberrationInstance = Instantiate(diamantAberrationPrefab, spirit[randomSpirit].position, Quaternion.identity);

        StartCoroutine(MadPhase());
    }

    // Spawn de l'éclair
    IEnumerator SpawnEclair()
    {
        yield return new WaitForSeconds(1.5f);

        Vector2 posSpawn = new Vector2(-415f, -5.83f);

        GameObject instanceEclair;
        instanceEclair = Instantiate(eclair, posSpawn, transform.rotation);

        // Fait spawn l'aura
        GameObject instanceAura;
        instanceAura = Instantiate(aura, posSpawn, transform.rotation);

        yield return new WaitForSeconds((float)0.04);

        Destroy(instanceEclair);

        yield return new WaitForSeconds((float)0.04);

        GameObject instanceEclair2;
        instanceEclair2 = Instantiate(eclair, posSpawn, transform.rotation);

        yield return new WaitForSeconds((float)0.04);

        Destroy(instanceEclair2);

        yield return new WaitForSeconds((float)0.3);

        StartCoroutine(LessAura(instanceAura));
    }

    // Diminution progressive de l'intensité de l'aura
    IEnumerator LessAura(GameObject instanceAura)
    {
        instanceAura.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity -= (float)0.05;

        yield return new WaitForSeconds((float)0.09);

        if (instanceAura.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity <= 0)
            Destroy(instanceAura);
        else
            StartCoroutine(LessAura(instanceAura));
    }
}
