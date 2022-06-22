using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenAberration : MonoBehaviour
{

    void Start()
    {
        LeanTween.alpha(gameObject, 0f, 1f);
        Destroy(gameObject, 1f);
    }
}
