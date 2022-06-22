using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolutions : MonoBehaviour
{

    public GameObject GO_Options;
    public GameObject GO_Menu;
    public GameObject GO_Video;

    private bool _isFullScreen = true;

    private int X = 1920;
    private int Y = 1080;

    private void Start()
    {
        GO_Options.SetActive(false);
        GO_Video.SetActive(false);

        Screen.SetResolution(X, Y, true);
    }



    public void setFullScreenTrue()
    {
        _isFullScreen = true;
        Screen.SetResolution(X, Y, _isFullScreen);
    }
    public void setFullScreenFalse()
    {
        _isFullScreen = false;
        Screen.SetResolution(X, Y, _isFullScreen);
    }

    //----------------------------------------------------------------//

    public void rest1920()
    {
        X = 1920;
        Y = 1080;

        Screen.SetResolution(X, Y, _isFullScreen);
    }

    public void rest800Windowed()
    {
        X = 800;
        Y = 600;
        Screen.SetResolution(X, Y, _isFullScreen);
    }

    public void rest1366()
    {
        X = 1366;
        Y = 768;

        Screen.SetResolution(X, Y, _isFullScreen);
    }


    public void rest1600()
    {
        X = 1600;
        Y = 900;

        Screen.SetResolution(X, Y, _isFullScreen);
    }

    //----------------------------------------------------------------//

    public void LaunchOptions()
    {
        GO_Options.SetActive(true);
        GO_Menu.SetActive(false);
    }

    public void RetourToMainMenu()
    {
        GO_Options.SetActive(false);
        GO_Menu.SetActive(true);
        GO_Video.SetActive(false);
    }

    public void Video()
    {
        GO_Options.SetActive(false);
        GO_Video.SetActive(true);


    }

}
