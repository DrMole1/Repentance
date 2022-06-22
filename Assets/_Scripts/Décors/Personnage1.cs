using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage1 : MonoBehaviour
{
    private bool once = true;


    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<FollowingEyes>().SetUp();
        transform.GetChild(1).gameObject.GetComponent<FollowingEyes>().SetUp();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && once)
        {
            LeanTween.alpha(gameObject, 0f, 1f);

            Destroy(gameObject, 1f);
        }
    }
}
