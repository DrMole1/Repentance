using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnlockPower : MonoBehaviour
{
    public GameObject MenuPouvoirUnlocked;
    public TextMeshProUGUI descTxt;
    public TextMeshProUGUI titreTxt;
    public Image logo;

    private bool _once = true;



    [Tooltip("En français, commence par une majuscule, sans accent, ni d'espace.")]
    [Header("Voir tooltip")]
    public string NomDuPouvoir;
    public string TitreDuPouvoir;
    [TextArea]
    public string description;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (_once)
            {
                //Débloque le pouvoir
                string combine = NomDuPouvoir + "Unlocked";
                PlayerPrefs.SetString(combine, "true");

                //Ouvre le menu décrivant le pouvoir et le set
                logo.sprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                MenuPouvoirUnlocked.SetActive(true);
                descTxt.text = description;
                titreTxt.text = "Unlocked : " + " " +TitreDuPouvoir;
                

                _once = false;
            }


        }
    }
}
