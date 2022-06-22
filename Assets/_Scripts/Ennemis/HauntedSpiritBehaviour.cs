using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class HauntedSpiritBehaviour : MonoBehaviour
{
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    [Header("Nombre de coups de boule que doit se prendre l'ennemi")]
    public int vie = 2;
    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 0.05f;
    [Header("Nombre d'ame que lache l'aberration une fois détruire")]
    public int nbAme = 1;
    [Space]
    public GameObject ptcFantomePrefab;
    public GameObject ptcDamagePrefab;
    public GameObject gameController;
    public GameObject ame;
    public GameObject flaque;
    public GameObject ptcFantomeBigPrefab;
    public GameObject ptcMonsterKilledPrefab;
    public GameObject eyes;
    public GameObject player;
    public GameObject soundMortPrefab;

    private GameObject globalLight;
    private CameraShake cameraShake;

    private Vector3 initPos;




    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        transform.GetChild(0).gameObject.SetActive(false);

        // Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();

        eyes.transform.localScale = new Vector3(1f, 0f, 1f);
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
                float randomScale = Random.Range(1, 2);
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
                ptcMonsterKilledInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Destroy(ptcMonsterKilledInstance, 5f);

                // Son de mort
                GameObject soundMortInstance;
                soundMortInstance = Instantiate(soundMortPrefab, transform.position, Quaternion.identity);
                Destroy(soundMortInstance, 5f);

                Destroy(gameObject);
            }
        }
    }


    IEnumerator StopRepousse()
    {
        yield return new WaitForSeconds(0.2f);

        transform.GetChild(0).gameObject.SetActive(false);
    }



    public void WakeUp()
    {
        StartCoroutine(StartMove());
    }


    public IEnumerator StartMove()
    {
        LeanTween.value(gameObject, 0f, 1f, 2f).setOnUpdate((float val) =>
        {
            eyes.transform.localScale = new Vector3(1f, val, 1f);
        });

        yield return new WaitForSeconds(2f);

        StartCoroutine(Deplacement());
    }



    IEnumerator Deplacement()
    {
        initPos = transform.position;
        float randomX = Random.Range(-2f, 2f);
        float randomY = Random.Range(-2f, 2f);

        LeanTween.value(gameObject, initPos.x, player.transform.position.x + randomX, 1f).setOnUpdate((float valX) =>
        {
            transform.position = new Vector3(valX, transform.position.y, transform.position.z);
        });

        LeanTween.value(gameObject, initPos.y, player.transform.position.y + randomY, 1f).setOnUpdate((float valY) =>
        {
            transform.position = new Vector3(transform.position.x, valY, transform.position.z);
        });

        yield return new WaitForSeconds(1f);

        initPos = transform.position;
        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);

        LeanTween.value(gameObject, initPos.x, initPos.x + randomX, 3f).setOnUpdate((float valX) =>
        {
            transform.position = new Vector3(valX, transform.position.y, transform.position.z);
        });

        LeanTween.value(gameObject, initPos.y, initPos.y + randomY, 3f).setOnUpdate((float valY) =>
        {
            transform.position = new Vector3(transform.position.x, valY, transform.position.z);
        });

        yield return new WaitForSeconds(3f);

        StartCoroutine(Deplacement());
    }
}
