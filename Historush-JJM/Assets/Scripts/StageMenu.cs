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
    public GameObject Stage1, Stage2, Stage3;
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
        print("�ܱ���ȭ ��ư ����");
        Stage_Menu.SetActive(false);
        Stage1.SetActive(true);
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
        print("������ ��ư ����");
        Stage_Menu.SetActive(false);
        Stage2.SetActive(true);
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
        print("�ﱹ�ô� ��ư ����");
        Stage_Menu.SetActive(false);
        Stage3.SetActive(true);
        Player.SetActive(true);
        gameManager.LoadPlayerDataFromJson();
        gameManager.ItemSet();
        gameManager.stageIndex = 5;
        gameManager.StageName(gameManager.stageIndex);
        playerController.avatarColorChange();
        playerController.avatarChange();
    }

    // ���ϽŶ� ��ư
    public void ClickUnifiedSilla()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        print("���ϽŶ� ��ư ����");
    }

    // ��� ��ư
    public void ClickKoryo()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        print("��� ��ư ����");
    }

    // ���� ��ư
    public void ClickJoseon()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        print("���� ��ư ����");
    }

    // ���������� ��ư
    public void ClickJapanOccupation()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        print("���������� ��ư ����");
    }

    // ����� ��ư
    public void ClickMordernHistory()
    {
        string bgmName = "3";
        CamObject.GetComponent<PlayBGMOpe>().PlayBGM(bgmName);
        print("������ ��ư ����");
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
