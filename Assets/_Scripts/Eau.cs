using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Eau : MonoBehaviour
{
    
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;
    bool isPlayingSound = false;


    GameObject globalLight;

    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'eau
        if (other.gameObject.tag == "Player" && isPlayingSound == false)
        {

            _audioManager.PlayTomberDansEau();
            isPlayingSound = true;

            globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 0.5f;
            StartCoroutine(SoundCanPlay());
        }
    }

    IEnumerator SoundCanPlay()
    {
        yield return new WaitForSeconds(1.5f);

        isPlayingSound = false;
    }
}
