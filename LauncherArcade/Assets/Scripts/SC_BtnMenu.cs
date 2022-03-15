using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BtnMenu : MonoBehaviour
{
    public SC_Launcher scLauncher;

    public void OnClick(bool goRight)
    {
        scLauncher.changerPage(goRight);
    }
}
