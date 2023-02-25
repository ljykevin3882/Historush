using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingMenu : MonoBehaviour
{
    public GameObject Setting_Menu, Main_Menu;
    public AudioSource BGM, Effect;
    public Toggle BGM_toggle, Effect_toggle;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.LoadPlayerDataFromJson();
        if (gameManager.playerData.BGM == true)
        {
            BGM_toggle.isOn=true;
        }
        else
        {
            BGM_toggle.isOn = false;
        }
        if (gameManager.playerData.SoundEffect == true)
        {
            Effect_toggle.isOn = true;
        }
        else
        {
            Effect_toggle.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BGM_Toggle()
    {
        if (BGM_toggle.isOn)
        {
            BGM.mute = false;
            gameManager.playerData.BGM = true;
        }
        else
        {
            BGM.mute = true ;
            gameManager.playerData.BGM = false;
        }
        gameManager.SavePlayerDataToJson();
    }
    public void Effect_Toggle()
    {
        if (Effect_toggle.isOn)
        {
            Effect.mute = false;
            gameManager.playerData.SoundEffect = true;
        }
        else
        {
            Effect.mute = true;
            gameManager.playerData.SoundEffect = false;
        }
        gameManager.SavePlayerDataToJson();
    }
    public void GoBack()
    {
        Main_Menu.SetActive(true);
        Setting_Menu.SetActive(false);
    }
}
