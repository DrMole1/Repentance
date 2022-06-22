using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSavePos : MonoBehaviour
{
    public enum scene
    {
        Un, Deux, Trois, Quatre, Test
    };

    public scene Scene;

    void Awake()
    {
        if(this.Scene == scene.Un)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("SavePosX", -2f), PlayerPrefs.GetFloat("SavePosY", -3f));
        }

        if (this.Scene == scene.Deux)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("SavePosX", -2f), PlayerPrefs.GetFloat("SavePosY", -3f));
        }

    }

    [ContextMenu("Reset Respawn")]
    public void ResetRespawn()
    {
        PlayerPrefs.SetFloat("SavePosX", 0f);
        PlayerPrefs.SetFloat("SavePosY", 0f);
    }
}
