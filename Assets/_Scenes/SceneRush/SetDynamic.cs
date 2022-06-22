using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDynamic : MonoBehaviour
{
    public ParticleSystem pctExplosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(SetState());
            Instantiate(pctExplosionPrefab, collision.transform.position, Quaternion.identity);
        }
    }

    IEnumerator SetState()
    {
        yield return new WaitForSeconds(0.7f);
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
        
    }
}
