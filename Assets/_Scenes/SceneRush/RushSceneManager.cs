using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RushSceneManager : MonoBehaviour
{

    private CinemachineVirtualCamera cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        cinemachine = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCamera(float size)
    {
        LeanTween.value(gameObject, 5f, size, 0.6f).setOnUpdate((float val) =>
        {
            cinemachine.m_Lens.OrthographicSize = val;
        }
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenCamera(8);
            GameObject.Find("Player").GetComponent<Player_Movement>().changedSpeed = 7.5f;
            StartCoroutine(waitToDestroy());
        }
    }

    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<BoxCollider2D>().enabled = false;
        
    }
    
}
