using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamantAberration : MonoBehaviour
{

    public GameObject ptcEvaporationPrefab;
    public GameObject aberrationPrefab;




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {


            GameObject ptcEvaporationInstance;
            ptcEvaporationInstance = Instantiate(ptcEvaporationPrefab, transform.position, Quaternion.identity);
            Destroy(ptcEvaporationInstance, 5f);

            GameObject aberrationInstance;
            aberrationInstance = Instantiate(aberrationPrefab, transform.position, Quaternion.identity);
            aberrationInstance.transform.localScale = new Vector2(0.5f, 0.5f);

            Destroy(gameObject);

        }
    }
 }
