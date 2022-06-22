using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class MenuPauseV2 : MonoBehaviour
{
    public GameObject Menu;
    public GameObject MenuPouvoirUnlocked;

    [Header("Script à désactiver lorsque le jeu est mis sur pause")]
    private GameObject Player;

    private Player_Movement playerMovement;
    private Rigidbody2D playerRB;

    private GameObject boule;
    private FloatingObjects bouleScript;
    private Rigidbody2D bouleRB;

    [Header("Virtual Camera")]
    public CinemachineVirtualCamera vcam;

    [HideInInspector]
    public bool canClose = true;

    //-------------------------------------//

    private bool _menuState = false;


    private void Start()
    {
        boule = GameObject.Find("bouleAcceptation");
        Player = GameObject.Find("Player");
        //ResetPosition();

        playerMovement = Player.GetComponent<Player_Movement>();
        playerRB = Player.GetComponent<Rigidbody2D>();

        bouleScript = boule.GetComponent<FloatingObjects>();
        bouleRB = boule.GetComponent<Rigidbody2D>();

        Menu.SetActive(false);
    }


    private void Update()
    {


        //Set le menu à actif s'il est inactif et inversement
        if (Input.GetKeyDown(KeyCode.Escape) && canClose == true)
        {
            SetMenu();
        }


    }

    public void SetMenu()
    {

        _menuState = !_menuState;
        Menu.SetActive(_menuState);

        if (Menu.activeSelf == true)
        {
            SetPause();
        }
        else
        {
            Resume();

        }

    }

    public void CloseMenuUnlocked()
    {
        MenuPouvoirUnlocked.SetActive(false);
    }

    


    private void SetPause()
    {
        Time.timeScale = 0.0f;

        //Le joueur
        playerMovement.enabled = false;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;

        vcam.Follow = null;

        bouleRB.constraints = RigidbodyConstraints2D.FreezeAll;
        bouleScript.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        playerMovement.enabled = true;
        playerRB.constraints = RigidbodyConstraints2D.None;
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        vcam.Follow = Player.transform;

        bouleRB.constraints = RigidbodyConstraints2D.None;
        bouleScript.enabled = true;
        
    }

    public void SoftResume()
    {
        Menu.SetActive(false);
    }


    public void RestartAtLastCheckPoint()
    {
        // Reload de la scene en court
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);

        // Position de sauvegarde du joueur
        //Player.transform.position = new Vector2(PlayerPrefs.GetFloat("SavePosX", 0f), PlayerPrefs.GetFloat("SavePosY", -3f));
    }

    public void RetourAuMenu()
    {
        Time.timeScale = 1.00f;
        PlayerPrefs.SetFloat("SavePosX", 0f);
        PlayerPrefs.SetFloat("SavePosY", -3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN_MENU");
       
        
    }


    //[ContextMenu("Reset Position")]
    //void ResetPosition()
    //{
    //    PlayerPrefs.SetFloat("SavePosX", 0);
    //    PlayerPrefs.SetFloat("SavePosY", -3f);
    //}
}
