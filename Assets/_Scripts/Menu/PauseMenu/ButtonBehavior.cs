using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    public string nom;

    private PowerManager pm;

    public GameObject linkedPower;


    private void Start()
    {
        pm = GameObject.Find("_GameController").GetComponent<PowerManager>();

    }

    
    public void OnClick()
    {
        //Check initial
        if (pm.dict_pouvoirs[nom] == true)
        {
            pm.SetPower(nom, false);
            this.gameObject.GetComponent<Image>().sprite = GetComponent<ButtonHandler>().lockedSprite;
        }
        else
        {

            SetPower("Patience", "Reticence");
            SetPower("Foi", "Piete");
            SetPower("Charite", "Altruisme");
            SetPower("Verite", "Purete");
            SetPower("Repos", "Pacifisme");

            

               
        }
        
        
        //Gère l'image
        if (pm.dict_pouvoirs[nom] == true)
        {
            this.gameObject.GetComponent<Image>().sprite = GetComponent<ButtonHandler>().unlockedSprite;
            linkedPower.GetComponent<Image>().sprite = linkedPower.GetComponent<ButtonHandler>().lockedSprite;
        }
        
        
    }



    void SetPower(string ch1, string ch2)
    {
        if (pm.dict_pouvoirs[ch1] == false || pm.dict_pouvoirs[ch2] == false)
        {


            if (nom == ch1)
            {
                pm.SetPower(ch2, false);
                pm.SetPower(ch1, true);

            }

            if (nom == ch2)
            {
                pm.SetPower(ch2, true);
                pm.SetPower(ch1, false);

            }
        }
    }



    [ContextMenu("Nbr")]
    int NbrTrueDansDic()
    {
        int cpt = 0;

        for (int i = 0; i <= pm.dict_pouvoirs.Count; i++)
        {
            int index = 0;

            foreach (KeyValuePair<string, bool> value in pm.dict_pouvoirs)
            {
                index++;
                if(index == i)
                {
                    if(value.ToString().Contains("True"))
                    {
                        cpt++;
                    }
                }
            }
        }
        
        return cpt;
    }
}
