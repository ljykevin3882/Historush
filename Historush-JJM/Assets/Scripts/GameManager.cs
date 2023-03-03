using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    //FPS ǥ��
    [Range(10, 150)]
    public int fontSize = 30;
    public Color color = new Color(.0f, .0f, .0f, 1.0f);
    public float width, height;
    //---------------------
    public int stagePoint; //���� ����
    public Text SookGarlicCount;

    public int stageIndex; //���� ��������
    public int health;
    public PlayerController player;
    public PlayerData playerData;
    public GameObject[] Stages;
    public GameObject[] items;
    public Sprite[] BackGround;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer BackGroundSpriteRenderer;

    CapsuleCollider2D capsuleCollider;
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn,UIRespawnBtn,Player;
    public GameObject Main_Menu, Stage_Menu,Stage1,SookGarlic;
    public GameManager gamemanager;
    public GameObject BossStageManager; // ���� �������� ������Ʈ

    // Start is called before the first frame update
    /* �ػ� �����ϴ� �Լ� */
    private void Start()
    {
        
    }
    void OnGUI() //FPS ǥ��
    {
        Rect position = new Rect(width, height, Screen.width, Screen.height);

        float fps = 1.0f / Time.deltaTime;
        float ms = Time.deltaTime * 1000.0f;
        string text = string.Format("{0:N1} FPS ({1:N1}ms)", fps, ms);

        GUIStyle style = new GUIStyle();

        style.fontSize = fontSize;
        style.normal.textColor = color;

        GUI.Label(position, text, style);
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
        string path = Path.Combine(Application.persistentDataPath, "playerData1.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("Reset Json Data")]
    public void ResetJson() //DB �ʱ�ȭ�Լ�
    {
        playerData.stagePoints = new int[31];
        playerData.items = new bool[44];
        for (int i = 0; i < playerData.items.Length; i++)
        {
            playerData.items[i] = false;
        }
        for (int i = 0; i < playerData.stagePoints.Length; i++)
        {
            playerData.stagePoints[i] = 0;
        }
        
        playerData.MaxStageLevel = 0;
        playerData.avatar_color = 0;
        playerData.avatar = 0;
        playerData.avatar_accessory = 0;
        playerData.BGM = true;
        playerData.SoundEffect = true;
        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Path.Combine(Application.persistentDataPath, "playerData1.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("From Json Data")]
    public  void LoadPlayerDataFromJson() //DB �ҷ����� �Լ�
    {

        string path = Path.Combine(Application.persistentDataPath, "playerData1.json");

        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists)//DB �����ϸ� 
        {
            print("DB �����մϴ�");
            string jsonData = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else //DB ����
        {
            print("DB �����ϴ�.");
            ResetJson();
            string jsonData = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        
        
        

        

        //print(System.IO.File.Exists(path) + "-> ���翩��");
        //if (System.IO.File.Exists(path))
        //{
        //    string jsonData = File.ReadAllText(path);
        //    playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        //}
        //else
        //{
        //    ResetJson();
        //    string jsonData = File.ReadAllText(path);
        //    playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        //}


    }
    private void Update()
    {
        //���� ����ȭ�ؼ� �����ϸ� �� ����
        //UIPoint.text = (totalPoint + stagePoint).ToString();
        //SookGarlicCount.text = (player.SookGarlic).ToString();

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
            if ((stageIndex+1) %4==0) //�������������� ��������������
            {

                Stages[stageIndex].SetActive(false);
                stageIndex++;  //��������������  ���� ��������: 4, 8, 12, 16
                SceneManager.LoadScene("LoadingSceneToBoss");
                StageName(stageIndex); //�������� �̸�����
                BossStageManage.curStage = stageIndex;
            }
            else
            {
                Stages[stageIndex].SetActive(false);
                stageIndex++;  
                Stages[stageIndex].SetActive(true);
                PlayerReposition();
                StageName(stageIndex); //�������� �̸�����
                ItemSet();
                BossStageManage.curStage = stageIndex;
            }
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
        //totalPoint += stagePoint;
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
        if (stage_Index == 0)
        {
            UIStage.text = "�ܱ���ȭ";
            BackGroundSpriteRenderer.sprite = BackGround[0]; 
        }
        else if (stage_Index <= 3) // 1 2 3
        {
            UIStage.text = "������"+ (stageIndex);
            BackGroundSpriteRenderer.sprite = BackGround[1];
        }
        else if (stage_Index == 4)
        {
            UIStage.text = "������ ����" ;
        }
        else if (stage_Index <= 7)
        {
            UIStage.text = "�ﱹ�ô�" + (stageIndex-4);
            BackGroundSpriteRenderer.sprite = BackGround[2];
        }
        else if (stage_Index == 8)
        {
            UIStage.text = "�ﱹ�ô� ����";
        }
        else if (stage_Index <=11)
        {
            UIStage.text = "���ϽŶ�" + (stageIndex-8);
            BackGroundSpriteRenderer.sprite = BackGround[3];
        }
        else if (stage_Index == 12)
        {
            UIStage.text = "���� �Ŷ� ����";
        }
        else if (stage_Index <= 15)
        {
            UIStage.text = "���" + (stageIndex-12);
            BackGroundSpriteRenderer.sprite = BackGround[4];
        }
        else if (stage_Index == 16)
        {
            UIStage.text = "��� ����";
        }
        else if (stage_Index <= 19)
        {
            UIStage.text = "����" + (stageIndex-16);
            BackGroundSpriteRenderer.sprite = BackGround[5];
        }
        else if (stage_Index == 20)
        {
            UIStage.text = "���� ����";
        }
        else if (stage_Index <= 23)
        {
            UIStage.text = "����������" + (stageIndex-20);
            BackGroundSpriteRenderer.sprite = BackGround[6];
        }
        else if (stage_Index == 24)
        {
            UIStage.text = "���������� ����";
        }
        else if (stage_Index <= 27)
        {
            UIStage.text = "�����" + (stageIndex-24);
            BackGroundSpriteRenderer.sprite = BackGround[7];
        }
        else if (stage_Index == 28)
        {
            UIStage.text = "����� ����";
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
    public void PlayerReposition()
    {   if (BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0&&stageIndex%4==0) { // ���� �������� �� ���
            BossStageManager.GetComponent<BossStageManage>().PlayerSpawn(BossStageManage.curStage);
            player.VelocityZero();
            return;
        }
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
    public void Restart() //�װ� ���� �޴��� ���� �Լ�
    {
        Time.timeScale = 1;
        if (BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0 && stageIndex % 4 == 0) SceneManager.LoadScene(0); // ���� ������������ ���� �� �ٷ� �̵�
        else
        {
            for (int i = 0; i < Stages.Length; i++)
            {
                Stages[i].SetActive(false);
            }
        }

        SceneManager.LoadScene(0);
    }
    public void Regame() //�װ� �ٽý����ϴ� �Լ�
    {
        if (BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0 && stageIndex % 4 == 0) { // ���� ���������� ���
            BossStageManager.GetComponent<BossStageManage>().Respawn();
        }
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
        if(BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0) return; // ���� ���������� ���� X
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
    public int[] stagePoints=new int[31];
    public bool[] items=new bool[44];
    public int avatar; //���϶��� 0, ����϶��� 1
    public int avatar_color; //�ƹ�Ÿ �� ��Ÿ���� ��
    public int avatar_accessory; //�ƹ�Ÿ �Ӹ���� ��Ÿ���� ��
    public bool BGM;
    public bool SoundEffect;
}
