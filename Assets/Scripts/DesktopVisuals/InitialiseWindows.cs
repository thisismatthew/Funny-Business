using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseWindows : MonoBehaviour
{
    public Window WelcomeWindow;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var window in FindObjectsOfType<Window>())
        {
            window.CloseWindow();
        }
        WelcomeWindow.OpenWindow();
    }

}
