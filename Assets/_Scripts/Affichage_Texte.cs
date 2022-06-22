using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Affichage_Texte : MonoBehaviour
{
    private GameObject player;
    private GameObject boule1;
    
    private Vector2   basePos = new Vector2(0,0);
    private bool      canPass = false;
    private bool      hasText = false;
    private int       current = 0;
    private bool      isClosed = true;
    private bool      isOpen = false;

    private Player_Movement pm;

    [Header("Zone d'affichage de texte des sépultures")]
    public GameObject Zone;
    [Header("Zone de texte des sépultures")]
    public TextMeshProUGUI area;

    private void Start()
    {
        
        boule1 = GameObject.Find("bouleAcceptation");
        player = GameObject.Find("Player");
        pm = player.GetComponent<Player_Movement>();

        Zone.SetActive(false);
        


    }

    private void Update()
    {
        /*
        //AFFICHAGE
        if (Input.GetKeyDown(KeyCode.A))
        {
            isDisplayed = !isDisplayed;

            if (isDisplayed == true)
            {
                OpenZone();
            }
            else if (isDisplayed == false)
            {
                CloseZone();
            }
        }
        */

        //-------------------//

        if (hasText == true)
        {
            if (current < tabGlobal.Length)
            {
                area.text = tabGlobal[current];
                if (canPass)
                {
                    GetInput();
                }

            }
        }


    }


    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (current + 1 != tabGlobal.Length)
            {
                current += 1;
            }
            else
            {
                CloseZone();
                pm.canMove = true;
                isClosed = true;
                current = 0;
            }

        }


    }

    string[] tabGlobal;
    /// <summary>
    /// Fonction qui prend en paramètre un tableau de chaines, permettant d'afficher la zone de texte, contenant pour chaque indice, son affichage.
    /// 
    /// </summary>
    public void ZoneDeTexte(string[] tab)
    {
        //Récupère un tableau de chaines. 
        //Ouvre la zone de texte
        //Le premier indice est affiché tout de suite. 
        //A chaque SPACE appuyé, le texte suivant s'affiche
        //Quand le dernier texte est affiché et que SPACE est appuyé, ferme la zone.
        hasText = true;
        tabGlobal = new string[tab.Length];

        tabGlobal = tab;

        OpenZone();

        area.text = tabGlobal[0];

        canPass = true;

    }

    private void testText()
    {
        string[] a = new string[3];


        a[0] = "ouais ouais";
        a[1] = "ah ok boomer";
        a[2] = "aya nakamura";

        ZoneDeTexte(a);
    }


    public void OpenZone()
    {
        Zone.SetActive(true);
        player.GetComponent<Player_Movement>().canMove = false;
        LeanTween.alpha(player, 0.2f, 0.5f);
        LeanTween.alpha(boule1, 0.2f, 0.5f);

        pm.canMove = false;

        isClosed = false;
        isOpen = true;
        LeanTween.moveLocalX(Zone, basePos.x, 0.5f);
    }

    public void CloseZone()
    {
        player.GetComponent<Player_Movement>().canMove = true;
        LeanTween.alpha(player, 1f, 0.5f);
        LeanTween.alpha(boule1, 1f, 0.5f);
        isOpen = false;
        isClosed = true;
        LeanTween.moveLocalX(Zone, basePos.x - 2000f, 0.5f);
        StartCoroutine("wait");
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        Zone.SetActive(false);
    }
}
