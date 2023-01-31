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
    public GameObject Stage_Menu, Player,Main_Menu;
    public GameObject Dangun_stage,GoJosun_stage, ThreeGuk_stage, TongilShila_stage, Korea_stage, Josun_stage, Japan_stage, Modern_stage;
    public GameManager gameManager;
    public PlayerController playerController;
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
        if (MaxStageindex > 0)
        { //�ѹ� �÷����ؼ� DB�� �����������
            for (int i = 1; i < Mathf.CeilToInt((MaxStageindex - 1) / 4) + 2; i++)
            {
                ButtonImages[i].color = white;
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
        // �ƹ�Ÿ ����
        //��¼�� ��¼��
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

    // ������ ��ư
    public void ClickGojoseon()
    {
        string bgmName = "3";
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
        string bgmName = "3";
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
        string bgmName = "3";
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
        string bgmName = "3";
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
    public void ClickMordern()
    {
        string bgmName = "3";
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
