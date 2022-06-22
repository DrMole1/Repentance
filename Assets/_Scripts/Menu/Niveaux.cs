using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveaux : MonoBehaviour
{
    public GameObject transitionPanel;

    

    public void NiveauUn()
    {
        StartCoroutine(TransitionOut("SceneJeu"));
    }

    public void NiveauDeux()
    {
        StartCoroutine(TransitionOut("SceneJeu2"));
    }

    public void NiveauTrois()
    {

    }

    public void NiveauQuatre()
    {

    }

    IEnumerator TransitionOut(string sceneName)
    {
        LeanTween.alpha(transitionPanel, 0, 0);
        transitionPanel.SetActive(true);
        LeanTween.alpha(transitionPanel, 1, 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }



}
