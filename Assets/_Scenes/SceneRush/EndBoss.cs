using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBoss : MonoBehaviour
{
    public GameObject ptcExplosionPrefab;
    public GameObject transitionPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BossGourmandise")
        {
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            StartCoroutine(Ending());
        }
    }

    IEnumerator Ending()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject ptcExplosionInstance;
            float randomX = Random.Range(transform.position.x - 4f, transform.position.x + 4f);
            float randomY = Random.Range(transform.position.y - 4f, transform.position.y + 4f);
            ptcExplosionInstance = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            Destroy(ptcExplosionInstance, 5f);

            GameObject ptcExplosionInstance2;
            randomX = Random.Range(transform.position.x - 4f, transform.position.x + 4f);
            randomY = Random.Range(transform.position.y - 4f, transform.position.y + 4f);
            ptcExplosionInstance2 = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            Destroy(ptcExplosionInstance2, 5f);

            GameObject ptcExplosionInstance3;
            randomX = Random.Range(transform.position.x - 4f, transform.position.x + 4f);
            randomY = Random.Range(transform.position.y - 4f, transform.position.y + 4f);
            ptcExplosionInstance3 = Instantiate(ptcExplosionPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            Destroy(ptcExplosionInstance3, 5f);

            yield return new WaitForSeconds(0.1f);
        }

        LeanTween.alpha(transitionPanel, 0, 0);
        transitionPanel.SetActive(true);
        LeanTween.alpha(transitionPanel, 1, 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MAIN_MENU");
    }
}
