using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeErrante : MonoBehaviour
{
    public GameObject ame;
    public GameObject ptcEspritPrefab;

    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;
    bool isPlayingSound = false;



    private void Start()
    {
        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isPlayingSound == false)
        {
            isPlayingSound = true;
            GameObject ameInstance = Instantiate(ame, transform.position, Quaternion.identity);

            //Joue le son 
            _audioManager.PlayToucherAmeErrante();

            // Ajout d'une force à l'item
            float forceX = 0;
            forceX = Random.Range(-2f, 2f);
            ameInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 2f), ForceMode2D.Impulse);

            GameObject ptcEspritInstance;
            ptcEspritInstance = Instantiate(ptcEspritPrefab, transform.position, Quaternion.identity);
            ptcEspritInstance.transform.rotation = Quaternion.Euler(new Vector2(-90, 0));
            Destroy(ptcEspritInstance, 2f);

            LeanTween.alpha(gameObject, 0f, 1f);

            Destroy(gameObject, 1f);

            StartCoroutine(SoundCanPlay());
        }
    }

    IEnumerator SoundCanPlay()
    {
        yield return new WaitForSeconds(2f);

        isPlayingSound = false;
    }
}
