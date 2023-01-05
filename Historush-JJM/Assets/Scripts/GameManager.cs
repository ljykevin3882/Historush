using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    public float bossStageTime = 0;
    public int totalPoint;
    public int stagePoint;
    public Text SookGarlicCount;

    public int stageIndex; //���� ��������
    public int health;
    public PlayerController player;
    public PlayerData playerData;
    public GameObject[] Stages;
    public GameObject[] items; 
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn,UIRespawnBtn,Player;
    public GameObject Main_Menu, Stage_Menu,Stage1,SookGarlic;
    // Start is called before the first frame update
    /* �ػ� �����ϴ� �Լ� */
    private void Start()
    {
        
    }
    public void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth,setHeight, true); // SetResolution �Լ� ����� ����ϱ�

        
    }
    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson() //DB�����Լ�
    {
        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("Reset Json Data")]
    public void ResetJson() //DB �ʱ�ȭ�Լ�
    {
        for (int i = 0; i < playerData.items.Length; i++)
        {
            playerData.items[0] = false;
        }
        playerData.score = 0;
        playerData.MaxStageLevel = 0;
        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("From Json Data")]
    public  void LoadPlayerDataFromJson() //DB �ҷ����� �Լ�
    {
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        string jsonData = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }
    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
        SookGarlicCount.text = (player.SookGarlic).ToString();

        if (stageIndex % 4 == 0 && stageIndex != 0) bossStageTime += Time.deltaTime;
        else bossStageTime = 0;
    }
    public void ItemSet()
    {
        //���� ���� ��Ȱ��ȭ
        for (int i = 0; i < 30; i++)
        {
            if (playerData.items[i] == true) //�̹� �Ծ�����
            {
                items[i].SetActive(false);
            }
        }
    }
    public void NextStage()
    {
        if (stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;  //��������������
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
            StageName(stageIndex); //�������� �̸�����
            ItemSet();
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
    public void StageName(int stage_Index) //ȭ�� �߰� ���� �ߴ� ���� �������� �̸� ���� �Լ�
    {
        if (stage_Index != 0)
        {
            SookGarlic.SetActive(false);
        }
        else
        {
            SookGarlic.SetActive(true); 
        }
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
        for(int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
        }

        SceneManager.LoadScene(0);
    }
    public void Regame() //�װ� �ٽý����ϴ� �Լ�
    {
        LoadPlayerDataFromJson(); //DB ����� �κб��� �ʱ�ȭ ��Ű��
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
[System.Serializable] //���� ����, ���������� ���·� ����
public class PlayerData //����� ������ ���̽� 
{
    public int MaxStageLevel;
    public int score;
    public bool[] items;
}
