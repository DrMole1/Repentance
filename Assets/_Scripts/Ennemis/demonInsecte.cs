using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class demonInsecte : MonoBehaviour
{
    public bool isHead;
    public int child = 0;
    public bool canShoot = false;
    public bool canDamage = true;
    [Header("Nombre de coups de boule que doit se prendre l'ennemi à la tête")]
    public int vie = 3;
    [Header("Valeur qui fait baisser la luminosité globale")]
    public float damage = 1f;
    [Header("Nombre d'ame que lache la tete une fois détruite")]
    public int nbAme = 2;
    [Space]
    public GameObject ptcFantomePrefab;
    public GameObject ptcDamagePrefab;
    public GameObject gameController;
    public GameObject ame;
    public GameObject flaque;
    public GameObject ptcFantomeBigPrefab;
    public GameObject ptcMonsterKilledPrefab;
    public GameObject projectilePrefab;

    // Gère les sons
    private AUDIO_Manager_GAME _audioManager;


    GameObject globalLight;
    GameObject player;
    private CameraShake cameraShake;
    private float force = 800f;

    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        player = GameObject.Find("Player");

        // Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();

        // Gère le Camera Shake
        cameraShake = GameObject.Find("CM vcam1").GetComponent<CameraShake>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'aberration
        if (other.gameObject.tag == "Player" && canDamage)
        {
            //Joue le son de dégats au joueur
            _audioManager.PlayMonstreToucheJoueur();


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
        if (other.gameObject.tag == "Boule" && canDamage)
        {

            //Joue le son de dégats au monstre
            _audioManager.PlayFaireDegats();


            // Particule de dégâts
            GameObject ptcDamageInstance;
            ptcDamageInstance = Instantiate(ptcDamagePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(ptcDamageInstance, 2f);

            if(isHead)
            {
                vie--;
            }
            else
            {
                if(child == 0)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 4;
                    Destroy(gameObject, 5f);
                    canDamage = false;
                    GetComponent<Rigidbody2D>().AddForce(transform.up * force);
                }
                else if (child == 1)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 4;
                    Destroy(gameObject, 5f);
                    canDamage = false;
                    GetComponent<Rigidbody2D>().AddForce(transform.up * force);

                    if (transform.childCount == 2)
                    {
                        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().gravityScale = 4;
                        Destroy(transform.GetChild(1).gameObject, 5f);
                        transform.GetChild(1).gameObject.GetComponent<demonInsecte>().canDamage = false;
                        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
                    }
                }
                else if (child == 2)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 4;
                    Destroy(gameObject, 5f);
                    canDamage = false;
                    GetComponent<Rigidbody2D>().AddForce(transform.up * force);

                    if (transform.childCount == 2)
                    {
                        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().gravityScale = 4;
                        Destroy(transform.GetChild(1).gameObject, 5f);
                        transform.GetChild(1).gameObject.GetComponent<demonInsecte>().canDamage = false;
                        transform.GetChild(1).gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);

                        if (transform.GetChild(1).childCount == 2)
                        {
                            transform.GetChild(1).GetChild(1).gameObject.GetComponent<Rigidbody2D>().gravityScale = 4;
                            Destroy(transform.GetChild(1).GetChild(1).gameObject, 5f);
                            transform.GetChild(1).GetChild(1).gameObject.GetComponent<demonInsecte>().canDamage = false;
                            transform.GetChild(1).GetChild(1).gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
                        }
                    }
                }
            }

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

    public void StartShooting()
    {
        if(isHead)
        {
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        GameObject projectileInstance;
        projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
        projectileInstance.GetComponent<Rigidbody2D>().AddForce(dir * 0.03f);
        Destroy(projectileInstance, 20f);

        yield return new WaitForSeconds(4.5f);

        if(canShoot)
        {
            StartCoroutine(Shooting());
        }
    }

    IEnumerator StopRepousse()
    {
        yield return new WaitForSeconds(0.2f);

        transform.GetChild(0).gameObject.SetActive(false);
    }
}