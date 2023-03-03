using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    //FPS 표시
    [Range(10, 150)]
    public int fontSize = 30;
    public Color color = new Color(.0f, .0f, .0f, 1.0f);
    public float width, height;
    //---------------------
    public int stagePoint; //현재 점수
    public Text SookGarlicCount;

    public int stageIndex; //현재 스테이지
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
    public GameObject BossStageManager; // 보스 스테이지 오브젝트

    // Start is called before the first frame update
    /* 해상도 설정하는 함수 */
    private void Start()
    {
        
    }
    void OnGUI() //FPS 표시
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
        int setWidth = 1920; // 사용자 설정 너비
        int setHeight = 1080; // 사용자 설정 높이
        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장
        Screen.SetResolution(setWidth,setHeight, true); // SetResolution 함수 제대로 사용하기

        
    }
    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson() //DB저장함수
    {
        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Path.Combine(Application.persistentDataPath, "playerData1.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("Reset Json Data")]
    public void ResetJson() //DB 초기화함수
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
    public  void LoadPlayerDataFromJson() //DB 불러오기 함수
    {

        string path = Path.Combine(Application.persistentDataPath, "playerData1.json");

        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists)//DB 존재하면 
        {
            print("DB 존재합니다");
            string jsonData = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else //DB 없음
        {
            print("DB 없습니다.");
            ResetJson();
            string jsonData = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        
        
        

        

        //print(System.IO.File.Exists(path) + "-> 존재여부");
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
        //여기 최적화해서 웬만하면 다 빼자
        //UIPoint.text = (totalPoint + stagePoint).ToString();
        //SookGarlicCount.text = (player.SookGarlic).ToString();

    }
    public void ItemSet()
    {
        //먹은 유물 비활성화
        for (int i = 0; i < 30; i++)
        {
            if (playerData.items[i] == true) //이미 먹었으면
            {
                items[i].SetActive(false);
            }
        }
    }
    public void NextStage()
    {
        if (stageIndex < Stages.Length-1)
        {
            if ((stageIndex+1) %4==0) //다음스테이지가 보스스테이지면
            {

                Stages[stageIndex].SetActive(false);
                stageIndex++;  //다음스테이지로  보스 스테이지: 4, 8, 12, 16
                SceneManager.LoadScene("LoadingSceneToBoss");
                StageName(stageIndex); //스테이지 이름변경
                BossStageManage.curStage = stageIndex;
            }
            else
            {
                Stages[stageIndex].SetActive(false);
                stageIndex++;  
                Stages[stageIndex].SetActive(true);
                PlayerReposition();
                StageName(stageIndex); //스테이지 이름변경
                ItemSet();
                BossStageManage.curStage = stageIndex;
            }
        }
        else
        {
            //게임 종료
            Time.timeScale = 0;
            Debug.Log("게임클리어");
            UIRestartBtn.SetActive(true);
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            UIRestartBtn.SetActive(true);
        }
        //Calculalte Point
        //totalPoint += stagePoint;
        stagePoint = 0;
    }
    public void StageName(int stage_Index) //화면 중간 위에 뜨는 현재 스테이지 이름 지정 함수
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
            UIStage.text = "단군신화";
            BackGroundSpriteRenderer.sprite = BackGround[0]; 
        }
        else if (stage_Index <= 3) // 1 2 3
        {
            UIStage.text = "고조선"+ (stageIndex);
            BackGroundSpriteRenderer.sprite = BackGround[1];
        }
        else if (stage_Index == 4)
        {
            UIStage.text = "고조선 보스" ;
        }
        else if (stage_Index <= 7)
        {
            UIStage.text = "삼국시대" + (stageIndex-4);
            BackGroundSpriteRenderer.sprite = BackGround[2];
        }
        else if (stage_Index == 8)
        {
            UIStage.text = "삼국시대 보스";
        }
        else if (stage_Index <=11)
        {
            UIStage.text = "통일신라" + (stageIndex-8);
            BackGroundSpriteRenderer.sprite = BackGround[3];
        }
        else if (stage_Index == 12)
        {
            UIStage.text = "통일 신라 보스";
        }
        else if (stage_Index <= 15)
        {
            UIStage.text = "고려" + (stageIndex-12);
            BackGroundSpriteRenderer.sprite = BackGround[4];
        }
        else if (stage_Index == 16)
        {
            UIStage.text = "고려 보스";
        }
        else if (stage_Index <= 19)
        {
            UIStage.text = "조선" + (stageIndex-16);
            BackGroundSpriteRenderer.sprite = BackGround[5];
        }
        else if (stage_Index == 20)
        {
            UIStage.text = "조선 보스";
        }
        else if (stage_Index <= 23)
        {
            UIStage.text = "일제강점기" + (stageIndex-20);
            BackGroundSpriteRenderer.sprite = BackGround[6];
        }
        else if (stage_Index == 24)
        {
            UIStage.text = "일제강점기 보스";
        }
        else if (stage_Index <= 27)
        {
            UIStage.text = "현대사" + (stageIndex-24);
            BackGroundSpriteRenderer.sprite = BackGround[7];
        }
        else if (stage_Index == 28)
        {
            UIStage.text = "현대사 보스";
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
            //health ui 다끄기
            UIhealth[0].color = new Color(1, 0, 0, 0.3f);
            player.OnDie();
            //죽은 UI
            Debug.Log("죽었습니다.");
            UIRestartBtn.SetActive(true);
            UIRespawnBtn.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")//낭떠러지
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
    {   if (BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0&&stageIndex%4==0) { // 보스 스테이지 일 경우
            BossStageManager.GetComponent<BossStageManage>().PlayerSpawn(BossStageManage.curStage);
            player.VelocityZero();
            return;
        }
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
    public void Restart() //죽고 메인 메뉴로 가는 함수
    {
        Time.timeScale = 1;
        if (BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0 && stageIndex % 4 == 0) SceneManager.LoadScene(0); // 보스 스테이지에서 죽을 시 바로 이동
        else
        {
            for (int i = 0; i < Stages.Length; i++)
            {
                Stages[i].SetActive(false);
            }
        }

        SceneManager.LoadScene(0);
    }
    public void Regame() //죽고 다시시작하는 함수
    {
        if (BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0 && stageIndex % 4 == 0) { // 보스 스테이지일 경우
            BossStageManager.GetComponent<BossStageManage>().Respawn();
        }
        LoadPlayerDataFromJson(); //DB 저장된 부분까지 초기화 시키기
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
    public void MapReset() //죽고 다시시작할때 아이템 원상복귀
    {   
        if(BossStageManage.curStage % 4 == 0 && BossStageManage.curStage > 0) return; // 보스 스테이지면 실행 X
        int child_num = Stages[stageIndex].transform.childCount;
        for (int i = 0; i < child_num; ++i)
        {
            Stages[stageIndex].transform.GetChild(i).gameObject.SetActive(true); //먹거나 사라졌던 아이템들 복귀
        }

    }

}
[System.Serializable] //저장 가능, 편집가능한 형태로 변경
public class PlayerData //사용자 데이터 베이스 
{
    public int MaxStageLevel;
    public int[] stagePoints=new int[31];
    public bool[] items=new bool[44];
    public int avatar; //곰일때는 0, 사람일때는 1
    public int avatar_color; //아바타 색 나타내는 값
    public int avatar_accessory; //아바타 머리장식 나타내는 값
    public bool BGM;
    public bool SoundEffect;
}
