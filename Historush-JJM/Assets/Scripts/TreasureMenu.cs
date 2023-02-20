using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureMenu : MonoBehaviour
{
    public GameObject Treasure_Menu, Main_Menu, Collection;
    public Text Collection_title;
    public Image Collection_treasure_1_1, Collection_treasure_1_2, Collection_treasure_2_1, Collection_treasure_2_2, Collection_treasure_3_1, Collection_treasure_3_2;
    public GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Collection_title.text = "고조선의 유물";
        //Collection_treasure_1_1 = Resourece.load 이런거 써보자
    }


    public void Click3Guk()
    {
        Collection.SetActive(true);
        Collection_title.text = "삼국시대의 유물";
    }
    public void ClickTongil()
    {
        Collection.SetActive(true);
        Collection_title.text = "통일신라시대의 유물";
    }
    public void ClickKorea()
    {
        Collection.SetActive(true);
        Collection_title.text = "고려시대의 유물";
    }

    public void ClickJosun()
    {
        Collection.SetActive(true);
        Collection_title.text = "조선시대의 유물";
    }

    public void ClickJapan()
    {
        Collection.SetActive(true);
        Collection_title.text = "일제강점기의 유물";
    }

    public void ClickModern()
    {
        Collection.SetActive(true);
        Collection_title.text = "현대시대의 유물";
    }

}
