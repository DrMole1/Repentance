using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerMenu : MonoBehaviour
{

    public ParticleSystem boomPrefab;
    
    public GameObject TitreImage;
    public GameObject Menu;
    public GameObject panelTransition;
    public GameObject firstText;
    public GameObject secondText;
    public GameObject cover;
    

    [Header("Les éléments du menu")]
    public Button BTN_jouer;
    public Button BTN_Niveaux;
    public Button BTN_quitter;
    public Button BTN_options;
    public GameObject IMG_Titre;

    public TextMeshProUGUI texteJouer;
    public TextMeshProUGUI texteQuitter;
    public TextMeshProUGUI optionsQuitter;

    [HideInInspector] public bool[] ETATS = new bool[5];


    // Start is called before the first frame update
    void Start()
    {
        


        Menu.SetActive(false);
        panelTransition.SetActive(false);
        TitreImage.SetActive(true);
        firstText.SetActive(false);
        secondText.SetActive(false);
        cover.SetActive(false);

        LeanTween.alpha(cover, 0, 0);
        LeanTween.alpha(panelTransition.GetComponent<RectTransform>(), 0, 0);
        LeanTween.alpha(BTN_jouer.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(BTN_quitter.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(BTN_options.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(IMG_Titre.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(TitreImage.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(TitreImage.GetComponent<RectTransform>(), 1f, 2f);

        initBoolTab(ETATS); 

        
        StartCoroutine("wait");
    }

    // Update is called once per frame
    void Update()
    {
        if (ETATS[0] == true && ETATS[1] == false)
        {
            AfficherMenu();
            ETATS[0] = false;

        }

    }


    void AfficherMenu()
    {
        Menu.SetActive(true);
        LeanTween.alpha(BTN_jouer.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.alpha(BTN_Niveaux.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.alpha(BTN_options.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.alpha(BTN_quitter.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.alpha(IMG_Titre.GetComponent<RectTransform>(), 1f, 1f);
        StartCoroutine("waitMenu");

    }

    IEnumerator waitMenu()
    {
        yield return new WaitForSeconds(1f);
        TitreImage.SetActive(false);
        ETATS[1] = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.alpha(TitreImage.GetComponent<RectTransform>(), 0f, 2f);
        yield return new WaitForSeconds(2f);
        ETATS[0] = true;
    }

    private void initBoolTab(bool[] tab)
    {
        for(int i = 0; i < tab.Length; i++)
        {
            tab[i] = false;
        }
    }
    

    public void QuitGame()
    {
        ParticleSystem exp = Instantiate(boomPrefab, BTN_quitter.transform.position, Quaternion.identity);
        StartCoroutine("QuitGameNow");
        
    }
    
    public void LaunchGame()
    {
        ParticleSystem exp = Instantiate(boomPrefab, BTN_jouer.transform.position, Quaternion.identity);
          
        StartCoroutine("LANCEMENT");
        
    }
    

    IEnumerator QuitGameNow()
    {
        LeanTween.alpha(panelTransition.GetComponent<RectTransform>(), 1, 1);
        yield return new WaitForSeconds(1f);
        Application.Quit(0);
    }


    IEnumerator LANCEMENT()
    {

        panelTransition.SetActive(true);
        LeanTween.alpha(panelTransition.GetComponent<RectTransform>(), 1, 1);
        yield return new WaitForSeconds(1f);

        BTN_jouer.gameObject.SetActive(false);
        BTN_quitter.gameObject.SetActive(false);
        BTN_options.gameObject.SetActive(false);
        IMG_Titre.gameObject.SetActive(false);

        firstText.gameObject.SetActive(true);

        LeanTween.alpha(panelTransition.GetComponent<RectTransform>(), 0, 1);
        yield return new WaitForSeconds(1f);
        panelTransition.SetActive(false);

        yield return new WaitForSeconds(7f);

        panelTransition.SetActive(true);
        LeanTween.alpha(panelTransition.GetComponent<RectTransform>(), 1, 1);
        yield return new WaitForSeconds(1f);

        firstText.gameObject.SetActive(false);
        secondText.gameObject.SetActive(true);

        LeanTween.alpha(panelTransition.GetComponent<RectTransform>(), 0, 1);
        yield return new WaitForSeconds(1f);
        panelTransition.SetActive(false);

        yield return new WaitForSeconds(3f);

        panelTransition.SetActive(true);
        cover.SetActive(true);
        LeanTween.alpha(panelTransition.GetComponent<RectTransform>(), 1, 1);
        LeanTween.alpha(cover, 1, 1);

        StopLight(GameObject.Find("LightRay01"));
        StopLight(GameObject.Find("LightRay02"));
        StopLight(GameObject.Find("LightRay03"));
        StopLight(GameObject.Find("LightRay04"));
        StopLight(GameObject.Find("LightRay05"));

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("INTRO_cinematique");

    }

    public void StopLight(GameObject light)
    {
        LeanTween.value(light, light.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity, 0f, 1f).setOnUpdate((float val) => {
            light.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });
    }


}
