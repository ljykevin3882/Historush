using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookMenu : MonoBehaviour
{
    public GameObject mainmenu, bookmenu, GoJoSun, ThreeGuk, Tongil, Korea, JoSun, Japan, Modern;

    // ������ ��ư
    public void ClickGojoseon()
    {
        bookmenu.SetActive(false);
        GoJoSun.SetActive(true);

    }

    // �ﱹ�ô� ��ư
    public void ClickThreeState()
    {
        bookmenu.SetActive(false);
        ThreeGuk.SetActive(true);

    }

    // ���ϽŶ� ��ư
    public void ClickUnifiedSilla()
    {
        bookmenu.SetActive(false);
        Tongil.SetActive(true);

    }

    // ��� ��ư
    public void ClickKorea()
    {
        bookmenu.SetActive(false);
        Korea.SetActive(true);
    }

    // ���� ��ư
    public void ClickJoseon()
    {
        bookmenu.SetActive(false);
        JoSun.SetActive(true);

    }

    // ���������� ��ư
    public void ClickJapan()
    {
        bookmenu.SetActive(false);
        Japan.SetActive(true);

    }

    // ����� ��ư
    public void ClickModern()
    {
        bookmenu.SetActive(false);
        Modern.SetActive(true);

    }

    // �ڷΰ��� ��ư
    public void ClickBack()
    {
        bookmenu.SetActive(false);
        mainmenu.SetActive(true);
    }
}
