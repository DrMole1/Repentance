using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AUDIO_Manager_GAME : MonoBehaviour
{

    private AudioSource _audioSource;

    [Header("Les clips de la scène JEU")]
    public AudioClip aberrationTouch;//Done
    public AudioClip collecterAme;//Done
    public AudioClip detruireChaine;//Done
    public AudioClip faireDegats;//Done
    public AudioClip lireSepulture;//Done
    public AudioClip monstreToucheJoueur;//Done
    public AudioClip plateformeTombe;//Done
    public AudioClip tomberDansEau;//Done
    public AudioClip toucherAmeErrante;//Done
    public AudioClip toucherBougie;//Done
    public AudioClip toucherHerbe;//Done
    public AudioClip toucherRondinPendu;
    public AudioClip toucherTrampoline;//Done


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
    }


    //Fonctions jouant des sons
    public void PlayAberrationTouch()
    {
        _audioSource.PlayOneShot(aberrationTouch);
    }
    public void PlayCollecterAme()
    {
        _audioSource.PlayOneShot(collecterAme);
    }
    public void PlayDetruireChaine()
    {
        _audioSource.PlayOneShot(detruireChaine);
    }
    public void PlayFaireDegats()
    {
        _audioSource.PlayOneShot(faireDegats);
    }
    public void PlayLireSepulture()
    {
        _audioSource.PlayOneShot(lireSepulture, 0.5f);
    }
    public void PlayMonstreToucheJoueur()
    {
        _audioSource.PlayOneShot(monstreToucheJoueur);
    }
    public void PlayPlateformeTombe()
    {
        _audioSource.PlayOneShot(plateformeTombe);
    }
    public void PlayTomberDansEau()
    {
        _audioSource.PlayOneShot(tomberDansEau);
    }
    public void PlayToucherAmeErrante()
    {
        _audioSource.PlayOneShot(toucherAmeErrante);
    }
    public void PlayToucherBougie()
    {
        _audioSource.PlayOneShot(toucherBougie);
    }
    public void PlayToucherHerbe()
    {
        _audioSource.PlayOneShot(toucherHerbe, 0.01f);
    }
    public void PlayToucherRondinPendu()
    {
        _audioSource.PlayOneShot(toucherRondinPendu);
    }
    public void PlayToucherTrampoline()
    {
        _audioSource.PlayOneShot(toucherTrampoline);
    }
}



