using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHerbe : MonoBehaviour
{
    public GameObject ptcHerbePrefab;

    public Animator animator;
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    private float _random = 0;
    private int _min = 0;//Inclus
    private int _max = 10;//Inclus
    private bool _canPlay = false;


    void Awake()
    {
        animator = GetComponent<Animator>();

        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }

    private void Start()
    {
        

        _random = Random.Range(_min, _max);

        if(this._random == 1)
        {
            _canPlay = true;
        }
        


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(_canPlay == true)
            {
                _audioManager.PlayToucherHerbe();
            }

            //GetComponent<Animator>().enabled = true;

            // On mets une secondes particule de brin d'herbes
            GameObject ptcHerbeInstance;
            ptcHerbeInstance = Instantiate(ptcHerbePrefab, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity, transform);
            Destroy(ptcHerbeInstance, 5f);


            animator.SetBool("isTouchByPlayer", true);

            StartCoroutine(Stop());
        }
    }

    IEnumerator Stop()
    {

        yield return new WaitForSeconds(1f);

        animator.SetBool("isTouchByPlayer", false);

        yield return new WaitForSeconds(4f);

        //GetComponent<Animator>().enabled = true;
    }

    void OnBecameVisible()
    {
        animator.enabled = true;
    }

    void OnBecameInvisible()
    {
        animator.enabled = false;
    }
}
