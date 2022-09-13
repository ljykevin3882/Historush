using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookEach : MonoBehaviour
{
    public GameObject[] btns;  // 초기 물음표박스 상자 10개
    public bool[] IsMat = new bool[10];  // 유물을 먹었는지 확인
    public Sprite[] MatImages; // 바꿀 유물이미지
    public GameObject bookmenu;


    void Update()
    {
        // 게임 중 유물을 먹었는지 확인 후 먹었다면 잠금해제
        for (int i = 0; i < 10; i++)
        {
            if (IsMat[i] == true)
            {
                btns[i].GetComponent<Image>().sprite = MatImages[i];
            }

        }

    }

    // 뒤로가기 버튼
    public void ClickBack()
    {

    }

}
