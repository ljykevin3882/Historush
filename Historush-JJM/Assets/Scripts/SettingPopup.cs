using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingPopup : MonoBehaviour
{
    public GameManager gameManager;
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
    public void GoMainMenu()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            for (int i = 0; i < gameManager.Stages.Length; i++)
            {
                gameManager.Stages[i].SetActive(false);
            }
        }

        SceneManager.LoadScene(0);
    }
}
