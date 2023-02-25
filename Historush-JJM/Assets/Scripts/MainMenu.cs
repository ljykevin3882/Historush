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
    public GameObject Main_Menu,Stage_Menu,Book_Menu,Setting_Menu,Custom_Menu;
    public CustomMenu custom_Menu;
    public StageMenu StageMenu;
    public GameManager gameManager;
    private GameObject CamObject;
    public AudioSource BGM, Effect;

    public void Start()
    {
        CamObject = GameObject.Find("Main Camera");
        gameManager.LoadPlayerDataFromJson();
        if (gameManager.playerData.BGM == false)
        {
            BGM.mute = true;
        }
        else
        {
            BGM.mute = false;
        }
        if (gameManager.playerData.SoundEffect == false)
        {
            Effect.mute = true;
        }
        else
        {
            Effect.mute = false;
        }
    }

    public void Update()
    {

    }


 
    public void ClickStart()
    {
        string bgmName = "2";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(true);
        Main_Menu.SetActive(false);
        StageMenu.StartStageMenu();
        //for (int i = 0; i < gameManager.Stages.Length; i++)
        //{
        //    gameManager.Stages[i].SetActive(false);
        //}
    }

    public void ClickCustom()
    {
        Main_Menu.SetActive(false);
        Custom_Menu.SetActive(true);
        print("Custom 버튼 누름");

        gameManager.LoadPlayerDataFromJson();
        if (gameManager.playerData.avatar == 0)
        {
            custom_Menu.OpenBearcustom();
        }
        else if (gameManager.playerData.avatar == 1)
        {
            custom_Menu.OpenHumancustom();
        }
    }

    public void ClickCollection()
    {
        Book_Menu.SetActive(true);
        Main_Menu.SetActive(false);
        print("Collection 버튼 누름");
    }

    public void ClickSettings()
    {
        Setting_Menu.SetActive(true);
        Main_Menu.SetActive(false);
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