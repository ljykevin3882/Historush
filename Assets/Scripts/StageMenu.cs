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
        //처음 단군신화 빼고 다 잠금
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
        //스테이지 클리어후, 버튼 색 변경 및 자물쇠 해금
        for (int i = 0; i < 8; i++)
        {
            if (stage_phase == i)
            {
                ButtonImages[i].color = new Color (1, 1, 1, 1);
                LockImages[i].gameObject.SetActive(false);
            }
        }
    }


    // 단군신화 버튼
    public void ClickDangun()
    {
        print("단군신화 버튼 누름");
    }

    // 고조선 버튼
    public void ClickGojoseon()
    {
        print("고조선 버튼 누름");
    }

    // 삼국시대 버튼
    public void ClickThreeState()
    {
        print("삼국시대 버튼 누름");
    }

    // 통일신라 버튼
    public void ClickUnifiedSilla()
    {
        print("통일신라 버튼 누름");
    }

    // 고려 버튼
    public void ClickKoryo()
    {
        print("고려 버튼 누름");
    }

    // 조선 버튼
    public void ClickJoseon()
    {
        print("조선 버튼 누름");
    }

    // 일제강점기 버튼
    public void ClickJapanOccupation()
    {
        print("일제강점기 버튼 누름");
    }

    // 현대사 버튼
    public void ClickMordernHistory()
    {
        print("고조선 버튼 누름");
    }
}
