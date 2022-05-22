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
    public int stage_phase = 0;
    public GameObject Stage_Menu,Player;
    public GameObject Stage1, Stage2, Stage3;
    public void Start()
    {
        //ó�� �ܱ���ȭ ���� �� ���
        if (stage_phase == 0)
        {

            for (int i = 1; i < 8; i++)
            {
                ButtonImages[i].color = black;
                LockImages[i].gameObject.SetActive(true);
            }
        }
    }

    public void Update()
    {
        //�������� Ŭ������, ��ư �� ���� �� �ڹ��� �ر�
        for (int i = 1; i < 8; i++)
        {
            if (stage_phase == i)
            {
                ButtonImages[i].color = new Color (1, 1, 1, 1);
                LockImages[i].gameObject.SetActive(false);
            }
        }
    }


    // �ܱ���ȭ ��ư
    public void ClickDangun()
    {
        print("�ܱ���ȭ ��ư ����");
        Stage_Menu.SetActive(false);
        Stage1.SetActive(true);
        Player.SetActive(true);
    }

    // ������ ��ư
    public void ClickGojoseon()
    {
        print("������ ��ư ����");
    }

    // �ﱹ�ô� ��ư
    public void ClickThreeState()
    {
        print("�ﱹ�ô� ��ư ����");
    }

    // ���ϽŶ� ��ư
    public void ClickUnifiedSilla()
    {
        print("���ϽŶ� ��ư ����");
    }

    // ��� ��ư
    public void ClickKoryo()
    {
        print("��� ��ư ����");
    }

    // ���� ��ư
    public void ClickJoseon()
    {
        print("���� ��ư ����");
    }

    // ���������� ��ư
    public void ClickJapanOccupation()
    {
        print("���������� ��ư ����");
    }

    // ����� ��ư
    public void ClickMordernHistory()
    {
        print("������ ��ư ����");
    }
}
