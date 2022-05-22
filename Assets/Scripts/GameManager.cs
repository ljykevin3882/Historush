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
    private void StageName(int stage_Index) //화면 중간 위에 뜨는 현재 스테이지 이름 지정 함수
    {
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
        SceneManager.LoadScene(0);
    }
    public void Regame() //죽고 다시시작하는 함수
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
    public void MapReset() //죽고 다시시작할때 아이템 원상복귀
    {
        int child_num = Stages[stageIndex].transform.childCount;
        for (int i = 0; i < child_num; ++i)
        {
            Stages[stageIndex].transform.GetChild(i).gameObject.SetActive(true); //먹거나 사라졌던 아이템들 복귀
        }

    }
   
}
