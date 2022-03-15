using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class SC_BtnStartGame : MonoBehaviour
{
    // public string PathToExe;
    // public Image imageComponent;

    public string pathToExe;
    private SC_Launcher scLauncher;


    void Update()
    {

    }

    public void OnClick()
    {
        // TODO continue to check for launching game----------------------------------------------------------------------
        if (pathToExe == null)
        {
            pathToExe = "chemin null";
        }
        scLauncher.newsZone.text = pathToExe;

        if (pathToExe != null)
            scLauncher.LaunchchGame(pathToExe);
    }
}
