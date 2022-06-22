using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.LWRP;

public class IntroManager : MonoBehaviour
{
    // Déclaration des variables
    //========================================
    [Header("To drag from scene")]
    public GameObject[] lights;
    public GameObject particleCoverage;
    public GameObject leftBorder;
    public GameObject rightBorder;
    public GameObject mainText;
    public GameObject coverageText;
    public GameObject coveragePressText;
    [Header("To write")]
    [TextArea]
    public string[] textToDisplay;

    private bool canContinue = false;
    private int currentSequence = 0;
    //========================================



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnStartOnly());
    }



    IEnumerator OnStartOnly()
    {
        yield return new WaitForSeconds(2);

        StartCoroutine(DisplayText());
    }



    IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(1);

        mainText.GetComponent<TextMeshProUGUI>().text = textToDisplay[currentSequence];

        LeanTween.alpha(coverageText.GetComponent<RectTransform>(), 0f, 2f);
        LeanTween.alpha(leftBorder.GetComponent<RectTransform>(), 1f, 2f);
        LeanTween.alpha(rightBorder.GetComponent<RectTransform>(), 1f, 2f);

        yield return new WaitForSeconds(2f);

        LeanTween.alpha(coveragePressText.GetComponent<RectTransform>(), 0f, 2f);

        canContinue = true;
    }



    IEnumerator EndText()
    {
        LeanTween.alpha(coverageText.GetComponent<RectTransform>(), 1f, 2f);
        LeanTween.alpha(leftBorder.GetComponent<RectTransform>(), 0f, 2f);
        LeanTween.alpha(rightBorder.GetComponent<RectTransform>(), 0f, 2f);
        LeanTween.alpha(coveragePressText.GetComponent<RectTransform>(), 1f, 2f);

        yield return new WaitForSeconds(2f);

        StartCoroutine(DisplayText());
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canContinue == true)
        {
            if (currentSequence == textToDisplay.Length - 1)
            {
                StartCoroutine(EndIntro());
                canContinue = false;
            }
            else
            {
                currentSequence++;

                StartCoroutine(EndText());
                canContinue = false;
            }
        }
    }


    IEnumerator EndIntro()
    {
        LeanTween.alpha(coverageText.GetComponent<RectTransform>(), 1f, 2f);
        LeanTween.alpha(leftBorder.GetComponent<RectTransform>(), 0f, 2f);
        LeanTween.alpha(rightBorder.GetComponent<RectTransform>(), 0f, 2f);
        LeanTween.alpha(coveragePressText.GetComponent<RectTransform>(), 1f, 2f);

        GameObject.Find("LightLargeRay01").GetComponent<LightAnim>().StopGlow();
        GameObject.Find("LightLargeRay02").GetComponent<LightAnim>().StopGlow();
        GameObject.Find("LightLargeRay03").GetComponent<LightAnim>().StopGlow();
        GameObject.Find("LightRay01").GetComponent<LightAnim>().StopGlow();
        GameObject.Find("LightRay02").GetComponent<LightAnim>().StopGlow();
        GameObject.Find("LightRay03").GetComponent<LightAnim>().StopGlow();
        GameObject.Find("LightRay04").GetComponent<LightAnim>().StopGlow();
        GameObject.Find("LightRay05").GetComponent<LightAnim>().StopGlow();

        yield return new WaitForSeconds(3f);

        StopLight(GameObject.Find("LightBG"));

        for(int i = 0; i < lights.Length; i++)
        {
            StopLight(lights[i]);
        }

        LeanTween.alpha(particleCoverage, 1f, 2f);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("SceneJeu", LoadSceneMode.Single);
    }


    public void StopLight(GameObject light)
    {
        LeanTween.value(light, light.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity, 0f, 1f).setOnUpdate((float val) => {light.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = val;
        });
    }


}
