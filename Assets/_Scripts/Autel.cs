using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Autel : MonoBehaviour
{
    public GameObject ptcAutelPrefab;

    private GameObject globalLight;


    void Awake()
    {
        globalLight = GameObject.Find("LumiereLanterneGlobale");
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // Quand le joueur touche l'autel
        if (other.gameObject.tag == "Player")
        {
            GameObject ptcAutelInstance;
            ptcAutelInstance = Instantiate(ptcAutelPrefab, transform.position, Quaternion.identity);
            Destroy(ptcAutelInstance, 5f);

            transform.GetChild(0).gameObject.SetActive(false);

            SetRespawn();

            // Heal du joueur avec luminosité
            if(globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity < 1.6)
            {
                LeanTween.value(gameObject, globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity, 1.6f, 0.5f).setOnUpdate((float val) => {
                    globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
                });
            }        
        }
    }



    public void SetRespawn()
    {
        

        PlayerPrefs.SetFloat("SavePosX", transform.position.x);
        PlayerPrefs.SetFloat("SavePosY", transform.position.y);
    }

    
}
