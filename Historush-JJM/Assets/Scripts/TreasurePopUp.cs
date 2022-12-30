using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TreasurePopUp : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController PlayerController;
    public GameObject TreasurePopUpDetail; // 팝업창 내부 설명 이미지
    public Sprite[] TreasureDetailList;
    public Image DetailImage;
    public bool TreasurePopupBool;
    public int TreasurePopupNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TreasurePopupBool == true)
        {
            print("작동함");
            DetailImage.sprite = TreasureDetailList[TreasurePopupNum];
            Time.timeScale = 0;
        }
        else
        {
           
        }

    }
    public void ClosePopUp()
    {
        Time.timeScale = 1;
        TreasurePopupBool = false;
        PlayerController.TreasurePopupObject.SetActive(false);
    }
}
