using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingPopup : MonoBehaviour
{
    public GameObject Setting_Popup;
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
        Setting_Popup.SetActive(false);
        Time.timeScale=1;
    }
}
