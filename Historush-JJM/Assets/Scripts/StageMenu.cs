using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMenu : MonoBehaviour
{
    public Color black;
    public Color white;
    public Image[] ButtonImages;
    public GameObject[] LockImages;
    public Button[] StageButtons;
    public GameObject Stage_Menu, Player,Main_Menu,stage_Detail;
    public GameObject Dangun_stage,GoJosun_stage, ThreeGuk_stage, TongilShila_stage, Korea_stage, Josun_stage, Japan_stage, Modern_stage;
    public Image Treasure1_1, Treasure1_2, Treasure2_1, Treasure2_2, Treasure3_1, Treasure3_2;
    public Text Stage1_Point, Stage2_Point, Stage3_Point;
    public GameManager gameManager;
    public PlayerController playerController;
    public TreasureMenu Treasure_Menu;
    public Button Play_Button;
    private GameObject CamObject;
    public void Start()
    {

        CamObject = GameObject.Find("Main Camera");


    }

    public void Update()
    {
        
    }
    public void StartStageMenu()
    {
        

        //�����Ҷ� DB��� ���丮�� ����
        
        for (int i = 1; i < 8; i++)
        {
            ButtonImages[i].color = black;
            LockImages[i].SetActive(true);
            StageButtons[i].interactable = false;

        }
        gameManager.LoadPlayerDataFromJson(); //DB �ҷ����� 
        int MaxStageindex = gameManager.playerData.MaxStageLevel;

        if (MaxStageindex > 0)//�ѹ� �÷����ؼ� DB�� �����������
        { 
            for (int i = 1; i < Mathf.CeilToInt((MaxStageindex - 1) / 4) + 2; i++)
            {
                ButtonImages[i].color = Color.white;
                StageButtons[i].interactable = true;
                LockImages[i].SetActive(false);
            }
            for (int i = Mathf.CeilToInt((MaxStageindex - 1) / 4) + 2; i < 8; i++)
            {

                ButtonImages[i].color = black;
                StageButtons[i].interactable = false;
                LockImages[i].SetActive(true);
            }
        }
        else//�ѹ��� �÷������� ������ 
        {
            gameManager.ResetJson(); //db �����
        }
    }

    // �ܱ���ȭ ��ư
    public void ClickDangun()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        Dangun_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 0;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }
    //stagedetail ���� �Լ�
    public void SettingStageDetail(int stagenum) //stagenum�� 1 5 9 13 17 21 25 �� �־���
    {
        int startnum = (stagenum-1)/4*6+1; //startnum�� ���� ��ȣ�� 1 7 13 19 25 31 37 �� �־��� 
        //���� ��� ����
        Treasure1_1.sprite = Treasure_Menu.treasure_list[startnum];
        Treasure1_2.sprite = Treasure_Menu.treasure_list[startnum+1];
        Treasure2_1.sprite = Treasure_Menu.treasure_list[startnum+2];
        Treasure2_2.sprite = Treasure_Menu.treasure_list[startnum+3];
        Treasure3_1.sprite = Treasure_Menu.treasure_list[startnum+4];
        Treasure3_2.sprite = Treasure_Menu.treasure_list[startnum+5];
        //ȹ������ ���� ���� ���� ó��
        gameManager.LoadPlayerDataFromJson();
        if (gameManager.playerData.items[startnum] == false)
        {
            Treasure1_1.color = Color.black;
        }
        else
        {
            Treasure1_1.color = Color.white;
        }
        if (gameManager.playerData.items[startnum+1] == false)
        {
            Treasure1_2.color = Color.black;
        }
        else
        {
            Treasure1_2.color = Color.white;
        }
        if (gameManager.playerData.items[startnum+2] == false)
        {
            Treasure2_1.color = Color.black;
        }
        else
        {
            Treasure2_1.color = Color.white;
        }
        if (gameManager.playerData.items[startnum+3] == false)
        {
            Treasure2_2.color = Color.black;
        }
        else
        {
            Treasure2_2.color = Color.white;
        }
        if (gameManager.playerData.items[startnum+4] == false)
        {
            Treasure3_1.color = Color.black;
        }
        else
        {
            Treasure3_1.color = Color.white;
        }
        if (gameManager.playerData.items[startnum+5] == false)
        {
            Treasure3_2.color = Color.black;
        }
        else
        {
            Treasure3_2.color = Color.white;
        }
        //���� �ҷ����� 
        Stage1_Point.text = gameManager.playerData.stagePoints[stagenum].ToString(); // startnum �� 1,
        Stage2_Point.text = gameManager.playerData.stagePoints[stagenum+1].ToString();
        Stage3_Point.text = gameManager.playerData.stagePoints[stagenum+2].ToString();
        //����
        stage_Detail.SetActive(true);
    }
    // ������ ��ư
    public void ClickGojoseon()
    {

        SettingStageDetail(1);
        Play_Button.onClick.RemoveAllListeners();
        Play_Button.onClick.AddListener(ClickGojoseonPlay);
    }
    public void ClickGojoseonPlay()
    {
        string bgmName = "4";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        GoJosun_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 1;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }

    // �ﱹ�ô� ��ư
    public void ClickThreeState()
    {
        SettingStageDetail(5);
        Play_Button.onClick.RemoveAllListeners();
        Play_Button.onClick.AddListener(ClickThreeStatePlay);
    }
    public void ClickThreeStatePlay()
    {
        string bgmName = "5";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        ThreeGuk_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 5;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }

    // ���ϽŶ� ��ư
    public void ClickTongilSila()
    {
        SettingStageDetail(9);
        Play_Button.onClick.RemoveAllListeners();
        Play_Button.onClick.AddListener(ClickTongilSilaPlay);
    }
    public void ClickTongilSilaPlay()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        TongilShila_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 9;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }

    // ��� ��ư
    public void ClickKorea()
    {
        SettingStageDetail(13);
        Play_Button.onClick.RemoveAllListeners();
        Play_Button.onClick.AddListener(ClickKoreaPlay);
    }
    public void ClickKoreaPlay()
    {
        string bgmName = "4";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        Korea_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 13;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }

    // ���� ��ư
    public void ClickJoseon()
    {
        SettingStageDetail(17);
        Play_Button.onClick.RemoveAllListeners();
        Play_Button.onClick.AddListener(ClickJoseonPlay);
    }
    public void ClickJoseonPlay()
    {
        string bgmName = "5";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        Josun_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 17;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }

    // ���������� ��ư
    public void ClickJapan()
    {
        SettingStageDetail(21);
        Play_Button.onClick.AddListener(ClickJapanPlay);
    }
    public void ClickJapanPlay()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        Japan_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 21;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }

    // ����� ��ư
    public void ClickModern()
    {
        SettingStageDetail(25);
        Play_Button.onClick.AddListener(ClickModernPlay);
    }
    public void ClickModernPlay()
    {
        string bgmName = "4";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        Modern_stage.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 25;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }
    public void CloseDetail()
    {
        stage_Detail.SetActive(false);
    }
    //�ڷΰ��� ��ư
    public void GoBack()
    {
        string bgmName = "1";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        Stage_Menu.SetActive(false);
        Main_Menu.SetActive(true);
        print("�ڷΰ��� ����");
    }
}
