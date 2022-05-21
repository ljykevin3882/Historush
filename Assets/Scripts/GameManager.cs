using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public Player_Move player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;
    
    // Start is called before the first frame update
    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }
    public void NextStage()
    {
        if (stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE" + (stageIndex + 1);
        }
        else
        {
            //���� ����
            Time.timeScale = 0;
            Debug.Log("����Ŭ����");
            UIRestartBtn.SetActive(true);
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            UIRestartBtn.SetActive(true);
        }
        //Calculalte Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.3f);
        }

        else
        {
            //health ui �ٲ���
            UIhealth[0].color = new Color(1, 0, 0, 0.3f);
            player.OnDie();
            //���� UI
            Debug.Log("�׾����ϴ�.");
            UIRestartBtn.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")//��������
        {
            if (health > 1)
            {
                PlayerReposition(); 
            }
            HealthDown();
        }
    }

    // Update is called once per frame
    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
