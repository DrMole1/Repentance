using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switching : MonoBehaviour
{

    public GameObject original;
    public GameObject replacement;
    public GameObject following;

    public bool StartSomething;
    public GameObject rideau;

    // Start is called before the first frame update
    void Start()
    {
        original.SetActive(true);
        replacement.SetActive(false);
        following.SetActive(false);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        original.SetActive(false);
        replacement.SetActive(true);
        following.SetActive(true);
        StartVomi();
    }

    private void StartVomi()
    {
        if(StartSomething)
        {
            LeanTween.scaleX(rideau, 2.15f, 2f);
        }

    }
}
