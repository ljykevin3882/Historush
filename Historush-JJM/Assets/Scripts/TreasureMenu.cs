using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMenu : MonoBehaviour
{
    public GameObject Treasure_Menu, Main_Menu, Gojoseon_Treasure, ThreeGuk_Treasure, Korea_Treasure, Joseon_Treasure, Japan_Treasure, Modern_Treasure;
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
    public void ClickGoJosun()
    {
        Gojoseon_Treasure.SetActive(true);
    }
    public void CloseGoJosun()
    {
        Gojoseon_Treasure.SetActive(false);
    }

    public void Click3Guk()
    {
        ThreeGuk_Treasure.SetActive(true);
    }
    public void Close3Guk()
    {
        ThreeGuk_Treasure.SetActive(false);
    }

    public void ClickKorea()
    {
        Korea_Treasure.SetActive(true);
    }
    public void CloseKorea()
    {
        Korea_Treasure.SetActive(false);
    }
    public void ClickJosun()
    {
        Joseon_Treasure.SetActive(true);
    }
    public void CloseJosun()
    {
        Joseon_Treasure.SetActive(false);
    }
    public void ClickJapan()
    {
        Japan_Treasure.SetActive(true);
    }
    public void CloseJapan()
    {
        Japan_Treasure.SetActive(false);
    }
    public void ClickModern()
    {
        Modern_Treasure.SetActive(true);
    }
    public void CloseModern()
    {
        Modern_Treasure.SetActive(false);
    }
}
