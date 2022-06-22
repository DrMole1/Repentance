using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles : MonoBehaviour
{
    public GameObject GO_Controles;
    public GameObject GO_Menu;
    public GameObject GO_Options;
    public GameObject GO_Video;
    public GameObject GO_Niveaux;

    private void Start()
    {
        GO_Controles.SetActive(false);
        GO_Options.SetActive(false);
        GO_Niveaux.SetActive(false);
    }

    public void Controle()
    {
        GO_Controles.SetActive(true);
        GO_Options.SetActive(false);
        GO_Menu.SetActive(false);
    }

    public void RetourFromControle()
    {
        GO_Controles.SetActive(false);
        GO_Options.SetActive(false);
        GO_Menu.SetActive(true);
    }
  
    public void Niveaux()
    {
        GO_Menu.SetActive(false);
        GO_Controles.SetActive(false);
        GO_Options.SetActive(false);
        GO_Niveaux.SetActive(true);
    }

    public void RetourFromNiveaux()
    {
        GO_Controles.SetActive(false);
        GO_Options.SetActive(false);
        GO_Niveaux.SetActive(false);
        GO_Menu.SetActive(true);
    }


}
