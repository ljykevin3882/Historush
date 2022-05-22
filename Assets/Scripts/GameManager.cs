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
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn,UIRespawnBtn,Player;
    public GameObject Main_Menu, Stage_Menu,Stage1;
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
            StageName(stageIndex);
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
    private void StageName(int stage_Index) //ȭ�� �߰� ���� �ߴ� ���� �������� �̸� ���� �Լ�
    {
        if (stage_Index < 1)
        {
            UIStage.text = "�ܱ���ȭ";
        }
        else if (stage_Index < 5)
        {
            UIStage.text = "������"+ (stageIndex);
        }
        else if (stage_Index < 9)
        {
            UIStage.text = "�ﱹ�ô�" + (stageIndex-4);
        }
        else if (stage_Index < 13)
        {
            UIStage.text = "���ϽŶ�" + (stageIndex-8);
        }
        else if (stage_Index < 17)
        {
            UIStage.text = "���" + (stageIndex-12);
        }
        else if (stage_Index < 21)
        {
            UIStage.text = "����" + (stageIndex-16);
        }
        else if (stage_Index < 25)
        {
            UIStage.text = "����������" + (stageIndex-20);
        }
        else if (stage_Index < 29)
        {
            UIStage.text = "�����" + (stageIndex-24);
        }

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
            UIRespawnBtn.SetActive(true);
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
    public void Restart() //�װ� ���� �޴��� ���� �Լ�
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Regame() //�װ� �ٽý����ϴ� �Լ�
    {
        PlayerReposition();
        player.Respawn();
        UIRespawnBtn.SetActive(false);
        UIRestartBtn.SetActive(false);
        health = 3;
        for(int i = 0; i < 3; i++)
        {
            UIhealth[i].color = new Color(1,1, 1, 1);
        }
        stagePoint = 0;
        MapReset();

    }
    public void MapReset() //�װ� �ٽý����Ҷ� ������ ���󺹱�
    {
        int child_num = Stages[stageIndex].transform.childCount;
        for (int i = 0; i < child_num; ++i)
        {
            Stages[stageIndex].transform.GetChild(i).gameObject.SetActive(true); //�԰ų� ������� �����۵� ����
        }

    }
   
}
