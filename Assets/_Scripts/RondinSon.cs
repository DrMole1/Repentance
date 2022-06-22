using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RondinSon : MonoBehaviour
{

    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    bool soundIsPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && soundIsPlaying == false)
        {
            _audioManager.PlayToucherRondinPendu();

            soundIsPlaying = true;
            StartCoroutine(SoundCanPlay());
        }

    }

    IEnumerator SoundCanPlay()
    {
        yield return new WaitForSeconds(5f);

        soundIsPlaying = false;
    }
}