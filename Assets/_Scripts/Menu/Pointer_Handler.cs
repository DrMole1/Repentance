using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pointer_Handler : MonoBehaviour
{
    //Gère les sons
    private AUDIO_managerMenu _audioManager;


    public GameObject jouerOverlay;
    public GameObject quitterOverlay;
    public GameObject optionsOverlay;
    public GameObject niveauxOverlay;

    public TextMeshProUGUI jouerTXT;
    public TextMeshProUGUI quitterTXT;
    public TextMeshProUGUI optionsTXT;
    public TextMeshProUGUI niveauxTXT;

    private bool jouerSelected;
    private bool quitterSelected;
    private bool optionsSelected;
    private bool niveauxSelected;
    

    private ControllerMenu _ControllerMenu;

    private void Start()
    {
        _ControllerMenu = this.GetComponent<ControllerMenu>();

        //Gère les sons
        _audioManager = GameObject.Find("_AudioManager").GetComponent<AUDIO_managerMenu>();
        jouerSelected = true;
    }

    bool once = false;

    private void Update()
    {

        Time.timeScale = 1f;




        if(_ControllerMenu.ETATS[1] == true && once == false)
        {
            once = true;
        }



        if (jouerSelected == true)
        {
            jouerTXT.color = new Color32(255, 255, 255, 255);
            jouerOverlay.SetActive(true);
        }
        else
        {
            jouerTXT.color = new Color32(255, 255, 255, 136);
            jouerOverlay.SetActive(false);
        }

        if (quitterSelected == true)
        {
            quitterTXT.color = new Color32(255, 255, 255, 255);
            quitterOverlay.SetActive(true);
        }
        else
        {
            quitterTXT.color = new Color32(255, 255, 255, 136);
            quitterOverlay.SetActive(false);
        }

        if (optionsSelected == true)
        {
            optionsTXT.color = new Color32(255, 255, 255, 255);
            optionsOverlay.SetActive(true);
        }
        else
        {
            optionsTXT.color = new Color32(255, 255, 255, 136);
            optionsOverlay.SetActive(false);
        }

        if (niveauxSelected == true)
        {
            niveauxTXT.color = new Color32(255, 255, 255, 255);
            niveauxOverlay.SetActive(true);
        }
        else
        {
            niveauxTXT.color = new Color32(255, 255, 255, 136);
            niveauxOverlay.SetActive(false);
        }
    }
    

    public void JouerEnter()
    {
        _audioManager.PlayHoverSound();

        if (jouerSelected != true)
        {
            jouerSelected = true;
            quitterSelected = false;
            optionsSelected = false;
            niveauxSelected = false;
            LeanTween.alpha(jouerOverlay.GetComponent<RectTransform>(), 0f, 0f);
            LeanTween.alpha(jouerOverlay.GetComponent<RectTransform>(), 1f, 1f);
        }
        
    }

    public void OptionsEnter()
    {
        _audioManager.PlayHoverSound();

        if (optionsSelected != true)
        {
            optionsSelected = true;
            jouerSelected = false;
            quitterSelected = false;
            niveauxSelected = false;
            LeanTween.alpha(optionsOverlay.GetComponent<RectTransform>(), 0f, 0f);
            LeanTween.alpha(optionsOverlay.GetComponent<RectTransform>(), 1f, 1f);
        }

    }

    public void QuitterEnter()
    {
        _audioManager.PlayHoverSound();

        if (quitterSelected != true)
        {
            jouerSelected = false;
            quitterSelected = true;
            optionsSelected = false;
            niveauxSelected = false;
            LeanTween.alpha(quitterOverlay.GetComponent<RectTransform>(), 0f, 0f);
            LeanTween.alpha(quitterOverlay.GetComponent<RectTransform>(), 1f, 1f);

        }


    }

    public void NiveauxEnter()
    {
        _audioManager.PlayHoverSound();

        if (niveauxSelected != true)
        {
            jouerSelected = false;
            quitterSelected = false;
            optionsSelected = false;
            niveauxSelected = true;
            LeanTween.alpha(quitterOverlay.GetComponent<RectTransform>(), 0f, 0f);
            LeanTween.alpha(quitterOverlay.GetComponent<RectTransform>(), 1f, 1f);

        }


    }

}
