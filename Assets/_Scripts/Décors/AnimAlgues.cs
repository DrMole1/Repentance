using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAlgues : MonoBehaviour
{
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


            //animator.speed = 5;

            //StartCoroutine(Stop());
            LeanTween.value(gameObject, 4f, 1f, 3f).setOnUpdate((float val) => {
                animator.speed = val;
            });
        }
    }

    IEnumerator Stop()
    {

        yield return new WaitForSeconds(1f);

        animator.speed = 1;
    }
}
