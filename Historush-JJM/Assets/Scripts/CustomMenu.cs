using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomMenu : MonoBehaviour
{
    
    public Toggle red_toggle, orange_toggle, yellow_toggle, lightgreen_toggle, green_toggle, skyblue_toggle, blue_toggle, nam_toggle, purple_toggle, gray_toggle, white_toggle; //색 토글들
    public Toggle blank, garlic, one, two, three; //머리 장식 선택 토글
    public Color red,orange,yellow,lightgreen,green,skyblue,blue,nam,purple,gray,white; //색상들
    public SpriteRenderer avatar,head;
    public Sprite garlic_sprite, bowl, what, koreajungja,heart; //머리에 달 유물들 사진 넣으면 됨
    public GameManager gameManager;
    public GameObject Main_menu, Custom_menu,Bear_custom,Human_custom,change_lock;
    public Button change_Button;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.LoadPlayerDataFromJson();

    }
    void Update()
    {
        
    }
    public void OpenBearcustom()
    {
        Human_custom.SetActive(false);
        Bear_custom.SetActive(true);
        gameManager.playerData.avatar = 0;
        gameManager.playerData.avatar_color = 0;
        gameManager.SavePlayerDataToJson();
        if (gameManager.playerData.MaxStageLevel == 0)
        {
            change_Button.interactable = false;
            change_lock.SetActive(true);
        }
        else
        {
            change_Button.interactable = true;
            change_lock.SetActive(false);
        }
    }
    public void OpenHumancustom()
    {
        gameManager.LoadPlayerDataFromJson();
        Bear_custom.SetActive(false);
        Human_custom.SetActive(true);
        gameManager.playerData.avatar = 1;
        LoadUserSetting();
        gameManager.SavePlayerDataToJson();
    }
    public void LoadUserSetting()
    {
        if (gameManager.playerData.avatar_color == 0) //흰색이면
        {
            white_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 1) //빨간색이면
        {
            red_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 2) //주황색이면
        {
            orange_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 3) //노란색이면
        {
            yellow_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 4) //연두색이면
        {
            lightgreen_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color ==5) //초록색이면
        {
            green_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 6) //하늘색이면
        {
            lightgreen_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 7) //파랑색이면
        {
            green_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 8) //남색이면
        {
            nam_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 9) //보라색이면
        {
            purple_toggle.isOn = true;
        }
        else if (gameManager.playerData.avatar_color == 10) //회색이면
        {
            gray_toggle.isOn = true;
        }
    }
    public void GoBack()
    {
        Main_menu.SetActive(true);
        Custom_menu.SetActive(false);
    }
    //머리장식 선택 함수 크기조정이 필요할듯
    public void garlicSelect()
    {
        head.sprite = garlic_sprite;

    }
    public void bowlSelect()
    {
        head.sprite =bowl;
    }
    public void heartSelect()
    {
        head.sprite = heart;
    }





    //아래로는 색 선택 함수
    public void whiteSelect()
    {
        avatar.color = white;
        gameManager.playerData.avatar_color = 0;
        gameManager.SavePlayerDataToJson();
    }
    public void redSelect()
    {
        avatar.color = red;
        gameManager.playerData.avatar_color = 1;
        gameManager.SavePlayerDataToJson();
    }
    public void orangeSelect()
    {
        avatar.color = orange;
        gameManager.playerData.avatar_color = 2;
        gameManager.SavePlayerDataToJson();
    }
    public void yellowSelect()
    {
        avatar.color = yellow;
        gameManager.playerData.avatar_color = 3;
        gameManager.SavePlayerDataToJson();
    }
    public void lightgreenSelect()
    {
        avatar.color = lightgreen;
        gameManager.playerData.avatar_color = 4;
        gameManager.SavePlayerDataToJson();
    }
    public void greenSelect()
    {
        avatar.color = green;
        gameManager.playerData.avatar_color = 5;
        gameManager.SavePlayerDataToJson();
    }
    public void skyblueSelect()
    {
        avatar.color = skyblue;
        gameManager.playerData.avatar_color = 6;
        gameManager.SavePlayerDataToJson();
    }
    public void blueSelect()
    {
        avatar.color = blue;
        gameManager.playerData.avatar_color = 7;
        gameManager.SavePlayerDataToJson();
    }
    public void namSelect()
    {
        avatar.color = nam;
        gameManager.playerData.avatar_color = 8;
        gameManager.SavePlayerDataToJson();
    }
    public void purpleSelect()
    {
        avatar.color = purple;
        gameManager.playerData.avatar_color = 9;
        gameManager.SavePlayerDataToJson();
    }
    public void graySelect()
    {
        avatar.color = gray;
        gameManager.playerData.avatar_color = 10;
        gameManager.SavePlayerDataToJson();
    }

}


