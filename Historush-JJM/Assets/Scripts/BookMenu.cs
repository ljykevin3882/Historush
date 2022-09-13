using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BookMenu : MonoBehaviour
{
    public GameObject mainmenu, bookmenu, GoJoSun, ThreeGuk, Tongil, Korea, JoSun, Japan, Modern;

    // 고조선 버튼
    public void ClickGojoseon()
    {
        bookmenu.SetActive(false);
        GoJoSun.SetActive(true);

    }

    // 삼국시대 버튼
    public void ClickThreeState()
    {
        bookmenu.SetActive(false);
        ThreeGuk.SetActive(true);

    }

    // 통일신라 버튼
    public void ClickUnifiedSilla()
    {
        bookmenu.SetActive(false);
        Tongil.SetActive(true);

    }

    // 고려 버튼
    public void ClickKorea()
    {
        bookmenu.SetActive(false);
        Korea.SetActive(true);
    }

    // 조선 버튼
    public void ClickJoseon()
    {
        bookmenu.SetActive(false);
        JoSun.SetActive(true);

    }

    // 일제강점기 버튼
    public void ClickJapan()
    {
        bookmenu.SetActive(false);
        Japan.SetActive(true);

    }

    // 현대사 버튼
    public void ClickModern()
    {
        bookmenu.SetActive(false);
        Modern.SetActive(true);

    }

    // 뒤로가기 버튼
    public void ClickBack()
    {
        bookmenu.SetActive(false);
        mainmenu.SetActive(true);
    }
}
