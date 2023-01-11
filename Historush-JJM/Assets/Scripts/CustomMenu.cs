using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMenu : MonoBehaviour
{
    public GameObject Main_menu, Custom_menu;
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
        Main_menu.SetActive(true);
        Custom_menu.SetActive(false);
        
    }
}
