using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalTimePlayed : MonoBehaviour
{
    private float seconds;
    private float minutes;
    private float hours;

    public TextMeshProUGUI texteDisplay;

    private void Start()
    {
        hours = PlayerPrefs.GetFloat("Heures");
        minutes = PlayerPrefs.GetFloat("Minutes");
        seconds = PlayerPrefs.GetFloat("Secondes");
    }

    private void Update()
    {


        seconds += Time.deltaTime;

        if(seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }

        if(minutes >=60)
        {
            hours++;
            minutes = 0;
            seconds = 0;
        }


        //Sauvegarde les players prefs

        Refresh();

        //Affichage
        texteDisplay.text = PlayerPrefs.GetFloat("Heures").ToString("0") + "h " + 
                            PlayerPrefs.GetFloat("Minutes").ToString("0") + "min " + 
                            PlayerPrefs.GetFloat("Secondes").ToString("0") + "s.";
    }

    private void Refresh()
    {
        PlayerPrefs.SetFloat("Secondes", seconds);
        PlayerPrefs.SetFloat("Minutes", minutes);
        PlayerPrefs.SetFloat("Heures", hours);
    }






}
