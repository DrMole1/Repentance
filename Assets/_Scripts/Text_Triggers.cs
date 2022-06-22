using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Triggers : MonoBehaviour
{
    public Affichage_Texte affichageTexte;

    //Gère les sons
    private AUDIO_Manager_GAME _audioManager;
    bool isPlayingSound = false;

    private string[] _texte1 = new string[1];
    private string[] _texte2 = new string[1];
    private string[] _texte3 = new string[1];
    private string[] _texte4 = new string[1];

    private void Start()
    {
        INIT_txt();
        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_Manager_GAME>();
    }

    void INIT_txt()
    {
        // Traduction française

        //_texte1[0] = "La mort .. L'avantage avec l'Au-delà, c'est que l'on a tout le temps pour expier ses fautes .. Maintenant va, pêcheur, et répare ton âme.";
        //_texte2[0] = "Accepter son passé représente le premier pas pour changer. Changer le monde .. et surtout soi-même.";
        //_texte3[0] = "Porte secours aux âmes que tu as peinées. Vois, pêcheur, et corrige les aberrations que tu as laissées en ce monde déjà bien meurtri ..";
        //_texte4[0] = "Vois, pêcheur, le reflet de ton passé souiller cette terre. Vois qui tu as jadis été ...";

        // Traduction anglaise

        _texte1[0] = "Death .. The benefit of the afterlife, it's that we have a lot of time to atone for our mistakes .. Now walk, sinner, and fix your soul.";
        _texte2[0] = "Accepting your past represents the first step of change. Change the world ... and yourself.";
        _texte3[0] = "Help souls you've hurt. See, sinner, and correct the aberrations you've left in this already damaged world ...";
        _texte4[0] = "See, sinner, the reflection of your past, soiling this ground. See what you've been ...";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && isPlayingSound == false)
        {
            _audioManager.PlayLireSepulture();
            isPlayingSound = true;

            switch(this.tag)
            {
                case "S1":
                    affichageTexte.ZoneDeTexte(_texte1);
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    break;
                case "S2":
                    affichageTexte.ZoneDeTexte(_texte2);
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    break;
                case "S3":
                    affichageTexte.ZoneDeTexte(_texte3);
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    break;
                case "S4":
                    affichageTexte.ZoneDeTexte(_texte4);
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    break;

                default:
                    break;
            }

            StartCoroutine(SoundCanPlay());
        }
    }

    IEnumerator SoundCanPlay()
    {
        yield return new WaitForSeconds(2f);

        isPlayingSound = false;
    }
}



//Les gens peuvent probablement penser qu'après le trépas, ils partent sans nettoyer leurs bavures, sans réparer leurs fautes ...
//Mais ils ont tort. Tout le monde doit payer.