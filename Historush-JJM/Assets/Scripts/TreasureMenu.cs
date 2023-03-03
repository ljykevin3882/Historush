using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureMenu : MonoBehaviour
{
    public GameObject Treasure_Menu, Main_Menu, Collection,Treasure_Detail;
    public Text Collection_title;
    public Image Collection_treasure_1_1, Collection_treasure_1_2, Collection_treasure_2_1, Collection_treasure_2_2, Collection_treasure_3_1, Collection_treasure_3_2,Treasure_datail;
    public Button treasure_Button_1_1, treasure_Button_1_2, treasure_Button_2_1, treasure_Button_2_2, treasure_Button_3_1, treasure_Button_3_2;
    public Sprite[] treasure_list;
    public GameManager gamemanager;
    public TreasurePopUp Treasure_popup;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ItemCheck(int start_num)
    {
        gamemanager.LoadPlayerDataFromJson();
        if (gamemanager.playerData.items[start_num] == false)
        {
            Collection_treasure_1_1.color = Color.black;
            treasure_Button_1_1.interactable = false;
        }
        else
        {
            Collection_treasure_1_1.color = Color.white;
            treasure_Button_1_1.interactable = true;
        }

        if (gamemanager.playerData.items[start_num + 1] == false)
        {
            Collection_treasure_1_2.color = Color.black;
            treasure_Button_1_2.interactable = false;
        }
        else
        {
            Collection_treasure_1_2.color = Color.white;
            treasure_Button_1_2.interactable = true;
        }

        if (gamemanager.playerData.items[start_num + 2] == false)
        {
            Collection_treasure_2_1.color = Color.black;
            treasure_Button_2_1.interactable = false;
        }
        else
        {
            Collection_treasure_2_1.color = Color.white;
            treasure_Button_2_1.interactable = true;
        }
        if (gamemanager.playerData.items[start_num + 3] == false)
        {
            Collection_treasure_2_2.color = Color.black;
            treasure_Button_2_2.interactable = false;
        }
        else
        {
            Collection_treasure_2_2.color = Color.white;
            treasure_Button_2_2.interactable = true;
        }
        if (gamemanager.playerData.items[start_num + 4] == false)
        {
            Collection_treasure_3_1.color = Color.black;
            treasure_Button_3_1.interactable = false;
        }
        else
        {
            Collection_treasure_3_1.color = Color.white;
            treasure_Button_3_1.interactable = true;
        }
        if (gamemanager.playerData.items[start_num + 5] == false)
        {
            Collection_treasure_3_2.color = Color.black;
            treasure_Button_3_2.interactable = false;
        }
        else
        {
            Collection_treasure_3_2.color = Color.white;
            treasure_Button_3_2.interactable = true;
        }
    }
    public void GoBack()
    {
        Main_Menu.SetActive(true);
        Treasure_Menu.SetActive(false);
    }
    public void CloseCollection()
    {
        Collection.SetActive(false);
    }
    public void ClickGoJosun()
    {

        Collection.SetActive(true);
        ItemCheck(1);
        Collection_title.text = "고조선의 유물";
        Collection_treasure_1_1.sprite = treasure_list[1];
        Collection_treasure_1_2.sprite = treasure_list[2];
        Collection_treasure_2_1.sprite = treasure_list[3];
        Collection_treasure_2_2.sprite = treasure_list[4];
        Collection_treasure_3_1.sprite = treasure_list[5];
        Collection_treasure_3_2.sprite = treasure_list[6];
        //Collection_treasure_1_1 = Resourece.load 이런거 써보자
        treasure_Button_1_1.onClick.RemoveAllListeners();
        treasure_Button_1_1.onClick.AddListener(clickTreasure1);
        treasure_Button_1_2.onClick.RemoveAllListeners();
        treasure_Button_1_2.onClick.AddListener(clickTreasure2);
        treasure_Button_2_1.onClick.RemoveAllListeners();
        treasure_Button_2_1.onClick.AddListener(clickTreasure3);
        treasure_Button_2_2.onClick.RemoveAllListeners();
        treasure_Button_2_2.onClick.AddListener(clickTreasure4);
        treasure_Button_3_1.onClick.RemoveAllListeners();
        treasure_Button_3_1.onClick.AddListener(clickTreasure5);
        treasure_Button_3_2.onClick.RemoveAllListeners();
        treasure_Button_3_2.onClick.AddListener(clickTreasure6);

    }


    public void Click3Guk()
    {

        Collection.SetActive(true);
        ItemCheck(7);
        Collection_title.text = "삼국시대의 유물";
        Collection_treasure_1_1.sprite = treasure_list[7];
        Collection_treasure_1_2.sprite = treasure_list[8];
        Collection_treasure_2_1.sprite = treasure_list[9];
        Collection_treasure_2_2.sprite = treasure_list[10];
        Collection_treasure_3_1.sprite = treasure_list[11];
        Collection_treasure_3_2.sprite = treasure_list[12];

        treasure_Button_1_1.onClick.RemoveAllListeners();
        treasure_Button_1_1.onClick.AddListener(clickTreasure7);
        treasure_Button_1_2.onClick.RemoveAllListeners();
        treasure_Button_1_2.onClick.AddListener(clickTreasure8);
        treasure_Button_2_1.onClick.RemoveAllListeners();
        treasure_Button_2_1.onClick.AddListener(clickTreasure9);
        treasure_Button_2_2.onClick.RemoveAllListeners();
        treasure_Button_2_2.onClick.AddListener(clickTreasure10);
        treasure_Button_3_1.onClick.RemoveAllListeners();
        treasure_Button_3_1.onClick.AddListener(clickTreasure11);
        treasure_Button_3_2.onClick.RemoveAllListeners();
        treasure_Button_3_2.onClick.AddListener(clickTreasure12);
    }
    public void ClickTongil()
    {
        Collection.SetActive(true);
        ItemCheck(13);
        Collection_title.text = "통일신라시대의 유물";
        Collection_treasure_1_1.sprite = treasure_list[13];
        Collection_treasure_1_2.sprite = treasure_list[14];
        Collection_treasure_2_1.sprite = treasure_list[15];
        Collection_treasure_2_2.sprite = treasure_list[16];
        Collection_treasure_3_1.sprite = treasure_list[17];
        Collection_treasure_3_2.sprite = treasure_list[18];

        treasure_Button_1_1.onClick.RemoveAllListeners();
        treasure_Button_1_1.onClick.AddListener(clickTreasure13);
        treasure_Button_1_2.onClick.RemoveAllListeners();
        treasure_Button_1_2.onClick.AddListener(clickTreasure14);
        treasure_Button_2_1.onClick.RemoveAllListeners();
        treasure_Button_2_1.onClick.AddListener(clickTreasure15);
        treasure_Button_2_2.onClick.RemoveAllListeners();
        treasure_Button_2_2.onClick.AddListener(clickTreasure16);
        treasure_Button_3_1.onClick.RemoveAllListeners();
        treasure_Button_3_1.onClick.AddListener(clickTreasure17);
        treasure_Button_3_2.onClick.RemoveAllListeners();
        treasure_Button_3_2.onClick.AddListener(clickTreasure18);
    }
    public void ClickKorea()
    {
        Collection.SetActive(true);
        ItemCheck(19);
        Collection_title.text = "고려시대의 유물";
        Collection_treasure_1_1.sprite = treasure_list[19];
        Collection_treasure_1_2.sprite = treasure_list[20];
        Collection_treasure_2_1.sprite = treasure_list[21];
        Collection_treasure_2_2.sprite = treasure_list[22];
        Collection_treasure_3_1.sprite = treasure_list[23];
        Collection_treasure_3_2.sprite = treasure_list[24];

        treasure_Button_1_1.onClick.RemoveAllListeners();
        treasure_Button_1_1.onClick.AddListener(clickTreasure19);
        treasure_Button_1_2.onClick.RemoveAllListeners();
        treasure_Button_1_2.onClick.AddListener(clickTreasure20);
        treasure_Button_2_1.onClick.RemoveAllListeners();
        treasure_Button_2_1.onClick.AddListener(clickTreasure21);
        treasure_Button_2_2.onClick.RemoveAllListeners();
        treasure_Button_2_2.onClick.AddListener(clickTreasure22);
        treasure_Button_3_1.onClick.RemoveAllListeners();
        treasure_Button_3_1.onClick.AddListener(clickTreasure23);
        treasure_Button_3_2.onClick.RemoveAllListeners();
        treasure_Button_3_2.onClick.AddListener(clickTreasure24);
    }

    public void ClickJosun()
    {
        Collection.SetActive(true);
        ItemCheck(25);
        Collection_title.text = "조선시대의 유물";
        Collection_treasure_1_1.sprite = treasure_list[25];
        Collection_treasure_1_2.sprite = treasure_list[26];
        Collection_treasure_2_1.sprite = treasure_list[27];
        Collection_treasure_2_2.sprite = treasure_list[28];
        Collection_treasure_3_1.sprite = treasure_list[29];
        Collection_treasure_3_2.sprite = treasure_list[30];

        treasure_Button_1_1.onClick.RemoveAllListeners();
        treasure_Button_1_1.onClick.AddListener(clickTreasure25);
        treasure_Button_1_2.onClick.RemoveAllListeners();
        treasure_Button_1_2.onClick.AddListener(clickTreasure26);
        treasure_Button_2_1.onClick.RemoveAllListeners();
        treasure_Button_2_1.onClick.AddListener(clickTreasure27);
        treasure_Button_2_2.onClick.RemoveAllListeners();
        treasure_Button_2_2.onClick.AddListener(clickTreasure28);
        treasure_Button_3_1.onClick.RemoveAllListeners();
        treasure_Button_3_1.onClick.AddListener(clickTreasure29);
        treasure_Button_3_2.onClick.RemoveAllListeners();
        treasure_Button_3_2.onClick.AddListener(clickTreasure30);
    }

    public void ClickJapan()
    {
        Collection.SetActive(true);
        ItemCheck(31);
        Collection_title.text = "일제강점기의 유물";
        Collection_treasure_1_1.sprite = treasure_list[31];
        Collection_treasure_1_2.sprite = treasure_list[32];
        Collection_treasure_2_1.sprite = treasure_list[33];
        Collection_treasure_2_2.sprite = treasure_list[34];
        Collection_treasure_3_1.sprite = treasure_list[35];
        Collection_treasure_3_2.sprite = treasure_list[36];

        treasure_Button_1_1.onClick.RemoveAllListeners();
        treasure_Button_1_1.onClick.AddListener(clickTreasure31);
        treasure_Button_1_2.onClick.RemoveAllListeners();
        treasure_Button_1_2.onClick.AddListener(clickTreasure32);
        treasure_Button_2_1.onClick.RemoveAllListeners();
        treasure_Button_2_1.onClick.AddListener(clickTreasure33);
        treasure_Button_2_2.onClick.RemoveAllListeners();
        treasure_Button_2_2.onClick.AddListener(clickTreasure34);
        treasure_Button_3_1.onClick.RemoveAllListeners();
        treasure_Button_3_1.onClick.AddListener(clickTreasure35);
        treasure_Button_3_2.onClick.RemoveAllListeners();
        treasure_Button_3_2.onClick.AddListener(clickTreasure36);
    }

    public void ClickModern()
    {
        Collection.SetActive(true);
        ItemCheck(37);
        Collection_title.text = "현대시대의 유물";
        Collection_treasure_1_1.sprite = treasure_list[37];
        Collection_treasure_1_2.sprite = treasure_list[38];
        Collection_treasure_2_1.sprite = treasure_list[39];
        Collection_treasure_2_2.sprite = treasure_list[40];
        Collection_treasure_3_1.sprite = treasure_list[41];
        Collection_treasure_3_2.sprite = treasure_list[42];

        treasure_Button_1_1.onClick.RemoveAllListeners();
        treasure_Button_1_1.onClick.AddListener(clickTreasure37);
        treasure_Button_1_2.onClick.RemoveAllListeners();
        treasure_Button_1_2.onClick.AddListener(clickTreasure38);
        treasure_Button_2_1.onClick.RemoveAllListeners();
        treasure_Button_2_1.onClick.AddListener(clickTreasure39);
        treasure_Button_2_2.onClick.RemoveAllListeners();
        treasure_Button_2_2.onClick.AddListener(clickTreasure40);
        treasure_Button_3_1.onClick.RemoveAllListeners();
        treasure_Button_3_1.onClick.AddListener(clickTreasure41);
        treasure_Button_3_2.onClick.RemoveAllListeners();
        treasure_Button_3_2.onClick.AddListener(clickTreasure42);
    }
    public void closeDetail()
    {
        Treasure_Detail.SetActive(false);
    }
    public void clickTreasure1()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[1];
    }
    public void clickTreasure2()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[2];
    }
    public void clickTreasure3()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[3];
    }
    public void clickTreasure4()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[4];
    }
    public void clickTreasure5()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[5];
    }
    public void clickTreasure6()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[6];
    }
    public void clickTreasure7()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[7];
    }
    public void clickTreasure8()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[8];
    }
    public void clickTreasure9()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[9];
    }
    public void clickTreasure10()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[10];
    }
    public void clickTreasure11()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[11];
    }
    public void clickTreasure12()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[12];
    }
    public void clickTreasure13()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[13];
    }
    public void clickTreasure14()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[14];
    }
    public void clickTreasure15()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[15];
    }
    public void clickTreasure16()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[16];
    }
    public void clickTreasure17()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[17];
    }
    public void clickTreasure18()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[18];
    }
    public void clickTreasure19()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[19];
    }
    public void clickTreasure20()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[20];
    }
    public void clickTreasure21()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[21];
    }
    public void clickTreasure22()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[22];
    }
    public void clickTreasure23()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[23];
    }
    public void clickTreasure24()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[24];
    }
    public void clickTreasure25()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[25];
    }
    public void clickTreasure26()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[26];
    }
    public void clickTreasure27()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[27];
    }
    public void clickTreasure28()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[28];
    }
    public void clickTreasure29()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[29];
    }
    public void clickTreasure30()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[30];
    }
    public void clickTreasure31()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[31];
    }
    public void clickTreasure32()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[32];
    }
    public void clickTreasure33()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[33];
    }
    public void clickTreasure34()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[34];
    }
    public void clickTreasure35()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[35];
    }
    public void clickTreasure36()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[36];
    }
    public void clickTreasure37()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[37];
    }
    public void clickTreasure38()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[38];
    }
    public void clickTreasure39()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[39];
    }
    public void clickTreasure40()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[40];
    }
    public void clickTreasure41()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[41];
    }
    public void clickTreasure42()
    {
        Treasure_Detail.SetActive(true);
        Treasure_datail.sprite = Treasure_popup.TreasureDetailList[42];
    }


}