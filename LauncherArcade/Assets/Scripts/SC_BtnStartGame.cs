using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class SC_BtnStartGame : MonoBehaviour
{
    // public string PathToExe;
    // public Image imageComponent;

    public string pathToExe;
    public TextMeshProUGUI gameName;


    void Update()
    {

    }

    public TextMeshProUGUI getTextMPro()
    {
        return gameName;
    }

    public void LaunchchGameBtn()
    {
        if (pathToExe != null)
            SC_Launcher.LaunchchGame(pathToExe);
    }
}
