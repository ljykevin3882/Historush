using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Color black;
    public Color white;
    public Image[] ButtonImages;
    public GameObject[] LockImages;


    public void Start()
    {

    }

    public void Update()
    {

    }


 
    public void ClickStart()
    {
        
        print("Start 버튼 누름");
    }

    public void ClickCustom()
    {

        print("Custom 버튼 누름");
    }

    public void ClickCollection()
    {

        print("Collection 버튼 누름");
    }

    public void ClickSettings()
    {

        print("Settings 버튼 누름");
    }

    public void ClickQuit()
    {

        print("Quit 버튼 누름");

        // unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // android
#else
        Application.Quit();
#endif
    }
}