using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionOnLoad : MonoBehaviour
{
    [Header("Panel de transition")]
    public GameObject panel;

    [Header("Particules d'explosion du boss")]
    public GameObject ptcExplosionPrefab;

    [Header("Face du boss")]
    public GameObject faceBossPrefab;
    GameObject boss;

    [Header("Durée de la transition au début de la scène")]
    public float transitionTime = 1f;
    bool once = true;



    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1, 0);
        panel.SetActive(true);
        LeanTween.alpha(panel.GetComponent<RectTransform>(), 0, transitionTime);

        boss = GameObject.Find("boss");
    
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SceneJeu")
        {
            if (GameObject.Find("mainBoss") == null && once)
            {
                LeanTween.alpha(boss, 0, 0.4f);
                StartCoroutine(EndScene1());
                LeanTween.alpha(boss, 0, 0.4f);

                once = false;

            }
        }
    }


    // Concerne le boss avarice
    IEnumerator EndScene1()
    {

        GameObject faceBossInstance;
        faceBossInstance = Instantiate(faceBossPrefab, new Vector2(-412f, -6f), Quaternion.identity);
        LeanTween.value(gameObject, 3f, 20f, 3f).setOnUpdate((float val) =>
        {
            faceBossInstance.transform.localScale = new Vector2(val, val);
        });
        Destroy(faceBossInstance, 3.25f);

        for(int i = 0; i < 30; i++)
        {
            GameObject ptcExplosionInstance;
            float randomX = Random.Range(-422f, -402f);
            float randomY = Random.Range(-15, 0f);
            ptcExplosionInstance = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            Destroy(ptcExplosionInstance, 5f);

            GameObject ptcExplosionInstance2;
            randomX = Random.Range(-422f, -402f);
            randomY = Random.Range(-15, 0f);
            ptcExplosionInstance2 = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            Destroy(ptcExplosionInstance2, 5f);

            GameObject ptcExplosionInstance3;
            randomX = Random.Range(-422f, -402f);
            randomY = Random.Range(-15, 0f);
            ptcExplosionInstance3 = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            Destroy(ptcExplosionInstance3, 5f);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5f);

        LeanTween.alpha(panel.GetComponent<RectTransform>(), 1, transitionTime);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("MAIN_MENU", LoadSceneMode.Single);
    }
    
}
