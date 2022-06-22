using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    // Propriétés du Cinemachine Virtual Camera
    // ===================================================
    private CinemachineVirtualCamera cinemachine;
    private bool isShaking = false;
    // ===================================================



    /// <summary>
    /// Initialisation des propriétés de la camera
    /// </summary>
    void Awake()
    {
        cinemachine = gameObject.GetComponent<CinemachineVirtualCamera>();
    }



    // Camera Shake quand Joueur touche Ennemi

    public void HurtByMonster()
    {
        if(!isShaking)
        {
            StartCoroutine(HurtByMonsterCoroutine());
            isShaking = true;
        }
    }

    IEnumerator HurtByMonsterCoroutine()
    {
        float initOrthographicSize = cinemachine.m_Lens.OrthographicSize;

        LeanTween.value(gameObject, initOrthographicSize, 4.5f, 0.3f).setOnUpdate((float val) => {
            cinemachine.m_Lens.OrthographicSize = val;
        });

        yield return new WaitForSeconds(0.3f);

        LeanTween.value(gameObject, 4.5f, initOrthographicSize, 0.3f).setOnUpdate((float val) => {
            cinemachine.m_Lens.OrthographicSize = val;
        });

        yield return new WaitForSeconds(0.3f);

        isShaking = false;
    }



    // Camera Shake quand Joueur entre/sort des grottes

    public void EnterCave(float size)
    {
        LeanTween.value(gameObject, 5f, size, 0.6f).setOnUpdate((float val) => {
            cinemachine.m_Lens.OrthographicSize = val;
        });
    }

    public void ExitCave(float size)
    {
        LeanTween.value(gameObject, size, 5f, 0.6f).setOnUpdate((float val) => {
            cinemachine.m_Lens.OrthographicSize = val;
        });
    }



    // Camera Shake quand Joueur touche Plateforme qui tombe

    public void TouchFallingPlatform()
    {

        StartCoroutine(TouchFallingPlatformCoroutine());
    }

    IEnumerator TouchFallingPlatformCoroutine()
    {
        LeanTween.value(gameObject, 0f, 7f, 0.3f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.3f);

        LeanTween.value(gameObject, 7f, -7f, 0.3f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.3f);

        LeanTween.value(gameObject, -7f, 0f, 0.3f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });
    }




    // Camera Shake quand Joueur commence le Boss 1

    public void StartBoss1()
    {

        StartCoroutine(StartBoss1Coroutine());
    }

    IEnumerator StartBoss1Coroutine()
    {
        LeanTween.value(gameObject, 0f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, -3f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, -3f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, -3f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, -3f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, -3f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, -3f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, -3f, 3f, 0.25f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.25f);

        LeanTween.value(gameObject, 3f, -2.5f, 0.25f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.25f);

        LeanTween.value(gameObject, -2.5f, 2f, 0.35f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.35f);

        LeanTween.value(gameObject, 2f, -1.5f, 0.35f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.35f);

        LeanTween.value(gameObject, -1.5f, 0f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });
    }



    // Secousse de la cage du Boss Avarice

    public void CageAvarice()
    {

        StartCoroutine(CageAvariceCoroutine());
    }

    IEnumerator CageAvariceCoroutine()
    {
        LeanTween.value(gameObject, 0f, 3f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.15f);

        LeanTween.value(gameObject, 3f, -3f, 0.3f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });

        yield return new WaitForSeconds(0.3f);

        LeanTween.value(gameObject, -3f, 0f, 0.15f).setOnUpdate((float val) => {
            cinemachine.m_Lens.Dutch = val;
        });
    }



    // Mad Phase Boss Avarice
    public void MadPhaseBoss1()
    {
        LeanTween.value(gameObject, 6f, 8f, 0.6f).setOnUpdate((float val) => {
            cinemachine.m_Lens.OrthographicSize = val;
        });
    }
}
