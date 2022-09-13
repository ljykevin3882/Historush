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
    public GameObject Main_Menu,Stage_Menu,Book_Menu;

    public void Start()
    {

    }

    public void Update()
    {

    }


 
    public void ClickStart()
    {
        Stage_Menu.SetActive(true);
        Main_Menu.SetActive(false);
        
    }

    public void ClickCustom()
    {

        print("Custom 버튼 누름");
    }

    public void ClickCollection()
    {
        Book_Menu.SetActive(true);
        Main_Menu.SetActive(false);
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