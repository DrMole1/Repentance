using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamant : MonoBehaviour
{

    public GameObject ptcEvaporationPrefab;

    void Start()
    {
        StartCoroutine(DeplacerBas());
    }


    IEnumerator DeplacerHaut()
    {
        LeanTween.moveY(gameObject, transform.position.y + 2f, 2f);

        yield return new WaitForSeconds(2f);

        StartCoroutine(DeplacerBas());
    }


    IEnumerator DeplacerBas()
    {
        LeanTween.moveY(gameObject, transform.position.y - 2f, 2f);

        yield return new WaitForSeconds(2f);

        StartCoroutine(DeplacerHaut());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            GameObject.Find("boss").GetComponent<BossAvarice>().StartMadPhase();

            GameObject ptcEvaporationInstance;
            ptcEvaporationInstance = Instantiate(ptcEvaporationPrefab, transform.position, Quaternion.identity);
            Destroy(ptcEvaporationInstance, 5f);

            Destroy(gameObject);

        }
    }
 }
