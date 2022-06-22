using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Ame : MonoBehaviour
{
    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;

    public float intensityToAdd = 0.02f;
    public GameObject player;
    public int type = 0;
    public GameObject ptcFeedbackPrefab;

    public float incrementAmeBleue= 0.4f;

    GameObject globalLight;

    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");

        

        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Joue le son de récupération d'âme
            _audioManager.PlayCollecterAme();

            // On mets une particule de feedback
            GameObject ptcFeedbackInstance;
            ptcFeedbackInstance = Instantiate(ptcFeedbackPrefab, other.gameObject.transform.position, Quaternion.identity, other.gameObject.transform);
            Destroy(ptcFeedbackInstance, 5f);


            if (type == 1)
            {
                PlayerPrefs.SetInt("AmesJaunes", PlayerPrefs.GetInt("AmesJaunes") + 1);
                if (globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity < 1.6f)
                {
                    globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity += intensityToAdd;
                }
            }
            else if(type == 2)
            {
                PlayerPrefs.SetInt("AmesBleues", PlayerPrefs.GetInt("AmesBleues") + 1);
                other.gameObject.GetComponent<Player_Movement>().collectedSoul += incrementAmeBleue;
            }



            Destroy(gameObject);
        }
    }
}
