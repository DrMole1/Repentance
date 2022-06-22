using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Bougie : MonoBehaviour
{

    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    private bool _hasPlayed = false;


    void Awake()
    {
        transform.GetChild(1).gameObject.SetActive(false);

        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(_hasPlayed == false)
            {
                _audioManager.PlayToucherBougie();
                _hasPlayed = true;
            }
            
            transform.GetChild(0).GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 1;

            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
