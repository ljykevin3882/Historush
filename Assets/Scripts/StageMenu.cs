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
    public int stage_phase = -1;

    public void Start()
    {
        //ó�� �ܱ���ȭ ���� �� ���
        if (stage_phase == -1)
        {
            for (int i = 0; i < 8; i++)
            {
                ButtonImages[i].color = black;
                LockImages[i].gameObject.SetActive(true);
            }
        }
    }

    public void Update()
    {
        //�������� Ŭ������, ��ư �� ���� �� �ڹ��� �ر�
        for (int i = 0; i < 8; i++)
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
