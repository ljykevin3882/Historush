using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureMenu : MonoBehaviour
{
    public GameObject Treasure_Menu, Main_Menu, Collection;
    public Text Collection_title;
    public Image Collection_treasure_1_1, Collection_treasure_1_2, Collection_treasure_2_1, Collection_treasure_2_2, Collection_treasure_3_1, Collection_treasure_3_2;
    public Button treasure_Button_1_1, treasure_Button_1_2, treasure_Button_2_1, treasure_Button_2_2, treasure_Button_3_1, treasure_Button_3_2;
    public Sprite[] treasure_list;
    public GameManager gamemanager;

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

        if (gamemanager.playerData.items[start_num+1] == false)
        {
            Collection_treasure_1_2.color = Color.black;
            treasure_Button_1_2.interactable = false;
        }
        else
        {
            Collection_treasure_1_2.color = Color.white;
            treasure_Button_1_2.interactable = true;
        }

        if (gamemanager.playerData.items[start_num+2] == false)
        {
            Collection_treasure_2_1.color = Color.black;
            treasure_Button_2_1.interactable = false;
        }
        else
        {
            Collection_treasure_2_1.color = Color.white;
            treasure_Button_2_1.interactable = true;
        }
        if (gamemanager.playerData.items[start_num+3] == false)
        {
            Collection_treasure_2_2.color = Color.black;
            treasure_Button_2_2.interactable = false;
        }
        else
        {
            Collection_treasure_2_2.color = Color.white;
            treasure_Button_2_2.interactable = true;
        }
        if (gamemanager.playerData.items[start_num+4] == false)
        {
            Collection_treasure_3_1.color = Color.black;
            treasure_Button_3_1.interactable = false;
        }
        else
        {
            Collection_treasure_3_1.color = Color.white;
            treasure_Button_3_1.interactable = true;
        }
        if (gamemanager.playerData.items[start_num+5] == false)
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
    }

}
