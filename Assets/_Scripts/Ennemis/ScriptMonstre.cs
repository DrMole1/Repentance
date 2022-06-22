using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ScriptMonstre : MonoBehaviour
{
    [Header("Nombre de coups de boule que doit se prendre l'ennemi")]
    public int vie = 3;
    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 1f;
    [Header("Nombre d'ame que lache l'aberration une fois détruire")]
    public int nbAme = 5;
    [Header("Temps qu'il parcourt par allé")]
    public float lifetime = 5;
    [Header("Vitesse de déplacement")]
    public float speed = 2;
    [Space]
    public GameObject ptcFantomePrefab;
    public GameObject ptcDamagePrefab;
    public GameObject gameController;
    public GameObject ame;
    public GameObject flaque;
    public GameObject ptcFantomeBigPrefab;
    public GameObject ptcMonsterKilledPrefab;

    // Gère les sons
    private AUDIO_Manager_GAME _audioManager;


    GameObject globalLight;
    public bool isMoving = false;
    GameObject player;
    private CameraShake cameraShake;

    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        player = GameObject.Find("Player");

        // Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
        
    }

    void Update()
    {

        if(isMoving)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aberration
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


        // Quand une boule touche l'aberration
        if (other.gameObject.tag == "Boule")
        {

            //Joue le son de dégats au monstre
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
                flaqueInstance = Instantiate(flaque, new Vector2(transform.position.x + 0.3f, transform.position.y - 1.2f), Quaternion.Euler(0,0,zRotation));
                flaqueInstance.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                // On mets une particule de mort
                GameObject ptcFantomeBigInstance;
                ptcFantomeBigInstance = Instantiate(ptcFantomeBigPrefab, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
                Destroy(ptcFantomeBigInstance, 2f);

                // On mets une secondes particule de mort
                GameObject ptcMonsterKilledInstance;
                ptcMonsterKilledInstance = Instantiate(ptcMonsterKilledPrefab, new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
                Destroy(ptcMonsterKilledInstance, 5f);

                Destroy(gameObject);
            }
        }
    }

    public void StartMove()
    {
        LeanTween.alpha(gameObject.transform.GetChild(0).gameObject, 0f, lifetime);
        LeanTween.alpha(gameObject.transform.GetChild(0).GetChild(0).gameObject, 0f, lifetime);
        LeanTween.alpha(gameObject.transform.GetChild(0).GetChild(1).gameObject, 0f, lifetime);
        LeanTween.alpha(gameObject.transform.GetChild(0).GetChild(2).gameObject, 0f, lifetime);
        Destroy(gameObject, lifetime);
    }
}