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

    public int stageIndex; //현재 스테이지
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
    /* 해상도 설정하는 함수 */
    private void Start()
    {
        
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
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("Reset Json Data")]
    public void ResetJson() //DB 초기화함수
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
    public  void LoadPlayerDataFromJson() //DB 불러오기 함수
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
            Stages[stageIndex].SetActive(false);
            stageIndex++;  //다음스테이지로
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
            StageName(stageIndex); //스테이지 이름변경
            ItemSet();
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
        totalPoint += stagePoint;
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
        if (stage_Index < 1)
        {
            UIStage.text = "단군신화";
        }
        else if (stage_Index < 5)
        {
            UIStage.text = "고조선"+ (stageIndex);
        }
        else if (stage_Index < 9)
        {
            UIStage.text = "삼국시대" + (stageIndex-4);
        }
        else if (stage_Index < 13)
        {
            UIStage.text = "통일신라" + (stageIndex-8);
        }
        else if (stage_Index < 17)
        {
            UIStage.text = "고려" + (stageIndex-12);
        }
        else if (stage_Index < 21)
        {
            UIStage.text = "조선" + (stageIndex-16);
        }
        else if (stage_Index < 25)
        {
            UIStage.text = "일제강점기" + (stageIndex-20);
        }
        else if (stage_Index < 29)
        {
            UIStage.text = "현대사" + (stageIndex-24);
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
    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
    public void Restart() //죽고 메인 메뉴로 가는 함수
    {
        Time.timeScale = 1;
        for(int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
        }

        SceneManager.LoadScene(0);
    }
    public void Regame() //죽고 다시시작하는 함수
    {
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
    public int score;
    public bool[] items;
}
