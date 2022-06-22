using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ButtonHandler : MonoBehaviour
{
    
    public TMP_FontAsset font;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    public MenuPauseV2 menuPause;


    private Image img;
    private TextMeshProUGUI titre;

    public string powerName;


    
    private void Start()
    {

        //Gère le texte du bouton
        titre = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        titre.font = font;
        titre.text = powerName; 

        //Gère l'image
        img = this.gameObject.GetComponent<Image>();
        img.sprite = lockedSprite;
        
    }

    





}
