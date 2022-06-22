using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Animator animator;

    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;


    void Awake()
    {
        animator = GetComponent<Animator>();

        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _audioManager.PlayToucherTrampoline();

            animator.SetBool("isTouchByPlayer", true);

            StartCoroutine(Stop());
        }
    }

    IEnumerator Stop()
    {

        yield return new WaitForSeconds(0.4f);

        animator.SetBool("isTouchByPlayer", false);
    }
}
