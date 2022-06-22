using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AUDIO_managerMenu : MonoBehaviour
{
    public AudioSource _audioSource;

    [Header("Les clips")]
    public AudioClip onClick;
    public AudioClip onHover;



    private void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        
        
    }


    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(onClick);
    }

    public void PlayHoverSound()
    {
        _audioSource.PlayOneShot(onHover);
       // _audioSource.volume = 0;
    }
}
