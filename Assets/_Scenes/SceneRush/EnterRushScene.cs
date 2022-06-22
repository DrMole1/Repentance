using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRushScene : MonoBehaviour
{
    public GameObject transitionPanel;

    private void Start()
    {
        LeanTween.alpha(transitionPanel.GetComponent<RectTransform>(), 0, 0);
        transitionPanel.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(transition());
        }
       
        IEnumerator transition()
        {
            transitionPanel.SetActive(true);
            LeanTween.alpha(transitionPanel.GetComponent<RectTransform>(), 1, 2f);
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneRush01");

        }

    }
}
