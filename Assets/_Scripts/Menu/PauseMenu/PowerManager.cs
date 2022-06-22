using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A enlever à la fin
using System.Reflection;


public class PowerManager : MonoBehaviour
{
    //Rajouter au gamecontroller : MenuPauseV2.cs et PowerManager.cs


    private GameObject Player;

    public Transform powers;

    public Dictionary<string, bool> dict_pouvoirs = new Dictionary<string, bool>();

    public GameObject Boule;
    private FloatingObjects bouleScript;
    private float boule_distance;
    private GameObject globalLight;

    private void Start()
    {

        string combine = "Repos" + "Unlocked";
        PlayerPrefs.SetString(combine, "true");


        Player = GameObject.Find("Player");
        globalLight = GameObject.Find("LumiereLanterneGlobale");

        //Global
        InitDictionnaireDePouvoirs();
        bouleScript = Boule.GetComponent<FloatingObjects>();

        //Pureté
        ameJaunePrefab.GetComponent<Ame>().intensityToAdd = 0.02f;
        initBool(states);
        boule_distance = bouleScript.distance;
        speedSave = speed;

        
    }

    [ContextMenu("Afficher dictionnaire")]
    void afficher()
    {
        foreach (KeyValuePair<string, bool> nom in dict_pouvoirs)
        {
            Debug.Log(nom);
        }
    }

    void InitDictionnaireDePouvoirs()
    {
        //Les valeurs booléenes seront à remplacer avec les PlayerPrefs des pouvoirs
        dict_pouvoirs.Add("Patience", false);
        dict_pouvoirs.Add("Reticence", false);
        dict_pouvoirs.Add("Foi", false);
        dict_pouvoirs.Add("Piete", false);
        dict_pouvoirs.Add("Charite", false);
        dict_pouvoirs.Add("Altruisme", false);
        dict_pouvoirs.Add("Verite", false);
        dict_pouvoirs.Add("Purete", false);
        dict_pouvoirs.Add("Repos", false);
        dict_pouvoirs.Add("Pacifisme", false);
    }



    /// <summary>
    /// Active ou désactive un pouvoir en fonction de son nom. 
    /// <para>Les noms : Patience, Reticence, Foi, Piete, Charite, Altruisme, Verite, Purete, Repos, Pacifisme</para>
    /// </summary>
    public void SetPower(string powerName, bool state)
    {
        string unlockedString = powerName + "Unlocked";
        

        if(PlayerPrefs.GetString(unlockedString) == "true")
        {
            dict_pouvoirs[powerName] = state;

            if (state == true)
            {
                ApplyPower(powerName);
            }
            else
            if (state == false)
            {
                WithdrawPower(powerName);
            }
        }
        else
        {
            //Instruction si le bouton clické n'est pas disponible
            Debug.Log("Le pouvoir " + powerName + " n'est pas débloqué");
        }


     
        
    }



    /// <summary>
    /// ACTIVE un pouvoir en fonction de son nom.
    /// <para>Les noms : Patience, Reticence, Foi, Piete, Charite, Altruisme, Verite, Purete, Repos, Pacifisme</para>
    /// </summary>
    public void ApplyPower(string powername)
    {
        //switch
        switch(powername)
        {
            case "Patience":
                PatienceOn();
                break;

            case "Reticence":
                ReticenceOn();
                break;

            case "Foi":
                FoiOn();
                break;

            case "Piete":
                PieteOn();
                break;

            case "Charite":
                ChariteOn();
                break;

            case "Altruisme":
                AltruismeOn();
                break;

            case "Verite":
                VeriteOn();
                break;

            case "Purete":
                PureteOn();
                break;

            case "Repos":
                ReposOn();
                break;

            case "Pacifisme":
                PacifismeOn();
                break;
        }
    }

    /// <summary>
    /// DESACTIVE un pouvoir en fonction de son nom.
    /// <para>Les noms : Patience, Reticence, Foi, Piete, Charite, Altruisme, Verite, Purete, Repos, Pacifisme</para>
    /// </summary>
    public void WithdrawPower(string powername)
    {
        //switch
        switch (powername)
        {
            case "Patience":
                PatienceOff();
                break;

            case "Reticence":
                ReticenceOff();
                break;

            case "Foi":
                FoiOff();
                break;

            case "Piete":
                PieteOff();
                break;

            case "Charite":
                ChariteOff();
                break;

            case "Altruisme":
                AltruismeOff();
                break;

            case "Verite":
                VeriteOff();
                break;

            case "Purete":
                PureteOff();
                break;

            case "Repos":
                ReposOff();
                break;

            case "Pacifisme":
                PacifismeOff();
                break;
        }
    }
    
    bool _reticence = false;
    bool[] states = new bool[5];
    bool once = true;
    bool allowed = false;

    [Header("Réticence")]
    public float distance = 5f;
    public float speed = 0.1f;
    private float speedSave;


    [Tooltip("Pourcentage de la valeur de base ajoutée à chaque frame")]
    [Range(200,900)]
    public float multiplierAller = 1.0f;

    [Tooltip("Pourcentage de la valeur de base ajoutée à chaque frame")]
    [Range(200, 900)]
    public float multiplierRetour = 1.0f;
    
    [Header("Charité")]
    public GameObject ameBleuePrefab;

    [Header("Repos")]
    bool reposState;
    public float toutesLesXsecondes;
    public float valeurArajouter;
    float reposTimer = 0;
    bool moving = false;



    void initBool(bool[] array)
    {
        for(int i = 0; i < array.Length; i++)
        {
            array[i] = false;
        }
    }

    bool isClicked = false;


    private void Update()
    {
        //Repos ------------------
        if(reposState)
        {
            if (!moving)
            {
                reposTimer += Time.deltaTime;

                if (reposTimer >= toutesLesXsecondes)
                {
                    //incrémenter global light
                    if (globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity < 1)
                    {
                        globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity += valeurArajouter;
                        if(globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity > 1)
                        {
                            globalLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 1;
                        }
                    }

                    reposTimer = 0f;
                }
            }

            if (Input.GetAxis("Horizontal") != 0)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }
       
        


        //Reticence -----------------------------------
        if (_reticence == true)
        {
            if (Input.GetMouseButton(0))
            {
                isClicked = true;
            }
            else
            {
                isClicked = false;
            }

            if(isClicked)
            {

                if (once)
                {
                    states[0] = true;
                    once = false;
                }

                if (states[0] == true)
                {
                    //arreter la boule
                    bouleScript.moving = false;
                    bouleScript.reticence = true;
                    states[0] = false;
                    states[1] = true;
                }
                if (states[1] == true)
                {
                    //rapprocher la boule
                    if (bouleScript.distance > 0.9f && bouleScript.distance < 1.01f )
                    {
                        bouleScript.distance = 1f;
                        states[1] = false;
                        states[2] = true;
                        
                    }
                    else
                    {
                        bouleScript.distance -= 0.20f;
                    }
                }
            }

            if (bouleScript.distance == 1f && !states[1] && states[2] && !isClicked)
            {
                allowed = true;
            }

           if(allowed)
            {
                if (states[2] == true)
                {
                    //Tirer la balle
                    if (bouleScript.distance > distance - 0.1f)
                    {
                        bouleScript.distance = distance;
                        states[2] = false;
                        states[3] = true;


                    }
                    else
                    {
                        bouleScript.distance += speed;
                        speed = speedSave * multiplierAller / 100;
                    }


                }

                if (states[3])
                {
                    //Ramener la boule
                    if (bouleScript.distance <= boule_distance + 0.1f && bouleScript.distance <= boule_distance - 0.1f)
                    {
                        bouleScript.distance = distance;
                        states[3] = false;
                        speed = speedSave;
                        allowed = false;
                        once = true;

                    }
                    else
                    {
                        bouleScript.distance -= speed;
                        speed = speedSave * multiplierRetour / 100;
                    }


                }
                //Terminer le processus
                if (once)
                {
                    bouleScript.moving = true;
                    bouleScript.reticence = false;
                    bouleScript.distance = boule_distance;
                }
            }
               
                
            
        }
        
        //---------------------------------------------



    }


    //****************************************************************************//
    //****************************************************************************//

    //******************************* LES POUVOIRS *******************************//

    //****************************************************************************//
    //****************************************************************************//


    //################# Patience #################//
    private void PatienceOn()
    {

    }

    private void PatienceOff()
    {

    }

    //################# Reticence #################//
    private void ReticenceOn()
    {
        _reticence = true;
    }

    private void ReticenceOff()
    {
        _reticence = false;
        bouleScript.distance = boule_distance;
        bouleScript.moving = true;
        bouleScript.reticence = false;
    }

    private void ReticencePower()
    {        
    }
    //################# Foi #################//
    private void FoiOn()
    {

    }


    private void FoiOff()
    {

    }
    //################# Piete #################//
    private void PieteOn()
    {

    }

    private void PieteOff()
    {

    }
    //################# Charite #################//
    private void ChariteOn()
    {
        //incrément ame bleue = nouvelle valeur
        ameBleuePrefab.GetComponent<Ame>().incrementAmeBleue = 0.8f;
        
    }

    private void ChariteOff()
    {
        //incrément ame bleue = ancienne valeur
        ameBleuePrefab.GetComponent<Ame>().incrementAmeBleue = 0.4f;
    }
    //################# Altruisme #################//
    private void AltruismeOn()
    {

    }

    private void AltruismeOff()
    {

    }
    //################# Verite #################//
    private void VeriteOn()
    {

    }

    private void VeriteOff()
    {

    }

    //################# Purete #################//

    [Header("Pureté")]
    public GameObject ameJaunePrefab;

    private void PureteOn()
    {
        //Base value = 0.02f
        float multiplier = 2;
        float value = ameJaunePrefab.GetComponent<Ame>().intensityToAdd;

        ameJaunePrefab.GetComponent<Ame>().intensityToAdd = value * multiplier;
    }

    private void PureteOff()
    {
        ameJaunePrefab.GetComponent<Ame>().intensityToAdd = 0.02f;
    }

    //################# Repos #################//
    private void ReposOn()
    {
        reposState = true;
    }

    private void ReposOff()
    {
        reposState = false;
    }

    //################# Pacifisme #################//
    private void PacifismeOn()
    {

    }

    private void PacifismeOff()
    {

    }


    [ContextMenu("Reset All Unlocked")]
    private void ResetAllUnlocked()
    {
        PlayerPrefs.SetString("Patience", "false");
        PlayerPrefs.SetString("Reticence", "false");
        PlayerPrefs.SetString("Foi", "false");
        PlayerPrefs.SetString("Piete", "false");
        PlayerPrefs.SetString("Charite", "false");
        PlayerPrefs.SetString("Altruisme", "false");
        PlayerPrefs.SetString("Verite", "false");
        PlayerPrefs.SetString("Purete", "false");
        PlayerPrefs.SetString("Repos", "false");
        PlayerPrefs.SetString("Pacifisme", "false");
    }



}
