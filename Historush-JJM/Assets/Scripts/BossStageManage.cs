using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BossStageManage : MonoBehaviour
{
    public GameObject[] playerSpawnPos;
    public GameObject[] bossSpawnPos;

    float timer; 
    public GameObject player, qna;
    public GameObject[] _boss; // 보스들
    public GameObject answerOne, answerOneBackground, answerTwo, answerTwoBackground;
    public GameObject mainCamera;
    public GameObject question, questionBackground, questionTimer;
    public GameObject BossFinishFlag;
    public GameObject boss; // 현재 스테이지에 스폰되어 있는 보스 오브젝트
    private bool isChecked = false;

    static public int answer;
    static public string mode = "Boss";
    static private bool isBossGen = false;
    static public bool isWrong = false;
    static public int curStage; // 4, 8, 12, 16, 20, 24, 28
    static public int patternIndex = -1; // 기본값 -1

    // 퀴즈
    // 고조선 - 단군
    // 삼국시대 - 주몽
    // 통일신라 - 장보고
    // 고려 - 강감찬
    // 조선 - 세종
    // 일제강점기 - 이토
    // 현대사 - 김일성
    static Dictionary<int, string> Question = new Dictionary<int, string>()
    {   // Key = curStage + 문제 번호 + (문제=0 OR 1번선지=1 OR 2번선지=2 OR 정답=3)
        // 고조선 퀴즈
        {410, "Q1. 고조선은 기원전 [   ] 년에 건국되었다."},
        {411, "2333"},
        {412, "3333"},
        {413, "1"},

        {420, "Q2. 고조선을 건국한 사람은?"},
        {421, "단군왕검"},
        {422, "곰"},
        {423, "1"},

        {430, "Q3. 고조선의 나라를 다스린 정신은?"},
        {431, "자유민주주의"},
        {432, "홍익인간"},
        {433, "2"},

        {440, "Q4. 단군왕검 이야기가 실려 있는 책은 [삼국유사]이다?"},
        {441, "O"},
        {442, "X"},
        {443, "1"},

        // 삼국시대 퀴즈
        {810, "Q1. 정사암에 모여 재상을 선출한 나라는?"},
        {811, "고구려"},
        {812, "백제"},
        {813, "2"},

        {820, "Q2. 마립간이라는 칭호를 처음 사용한 왕은?"},
        {821, "법흥왕"},
        {822, "내물왕"},
        {823, "2"},

        {830, "Q3. 화랑도를 국가적 조직으로 개편한 왕은?"},
        {831, "지증왕"},
        {832, "진흥왕"},
        {833, "2"},

        {840, "Q4. 삼국시대 역사서인 <삼국사기>를 편찬한 사람은?"},
        {841, "김부식"},
        {842, "박완서"},
        {843, "1"},

        // 통일신라 퀴즈
        {1210, "Q1. 신라 최고 교육기관인 국학을 설치한 왕은 신문왕이다."},
        {1211, "O"},
        {1212, "X"},
        {1213, "1"},

        {1220, "Q2. 통일 신라는 행정구역을 9주 5소경으로 나누었다."},
        {1221, "O"},
        {1222, "X"},
        {1223, "1"},

        {1230, "Q3. 삼국을 통일한 신라의 왕은?"},
        {1231, "무열왕"},
        {1232, "문무왕"},
        {1233, "2"},

        {1240, "Q4. 김흠돌의 난을 진압한 신라의 왕은?"},
        {1241, "성덕왕"},
        {1242, "신문왕"},
        {1243, "2"},

        // 고려 퀴즈
        {1610, "Q1. 다음 중 고려시대 토지제도에 대한 설명으로 옳지 않은 것은?"},
        {1611, "전시과에서는 문무관리, 군인, 향리 등을 18등급으로 나누어, 토지의 수조권을 지급하였다."},
        {1612, "직업군인에게 군인전을 지급하고, 3등급 이하의 준공신에게 별사전을 지급하였다."},
        {1613, "2"},

        {1620, "Q2. 노비안검법을 실시한 고려 왕은?"},
        {1621, "성종"},
        {1622, "광종"},
        {1623, "2"},

        {1630, "Q3. 성종에게 시무 28조를 건의한 사람은?"},
        {1631, "최승로"},
        {1632, "최치원"},
        {1633, "1"},

        {1640, "Q4. 강감찬 장군이 거란에 맞서싸워 대승을 거둔 전투는?"},
        {1641, "귀주대첩"},
        {1642, "여몽전쟁"},
        {1643, "1"},

        // 조선 퀴즈
        {2010, "Q1. 조선시대 8대 왕은?"},
        {2011, "세조"},
        {2012, "태종"},
        {2013, "1"},

        {2020, "Q2.조선시대의 성리학에 대한 설명으로 틀린 것은?"},
        {2021, "지배 계급간의 권력과 역할 분담을 다룸"},
        {2022, "유교 사상을 기반으로 한 인간 중심적 철학"},
        {2023, "1"},

        {2030, "Q3. 조선시대 문화재 중 한라산성 돌담길, 창덕궁 등은 어떤 시기에 건립되었나요?"},
        {2031, "조선 초기"},
        {2032, "조선 중기"},
        {2033, "2"},

        {2040, "Q4. 조선시대 성리학자로 유명한 인물은 누구인가요?"},
        {2041, "이황"},
        {2042, "율곡 이이"},
        {2043, "1"},

        // 일제강점기 퀴즈
        {2410, "Q1. 1919년 3.1 운동에 대한 설명으로 옳지 않은 것은?"},
        {2411, "국내에서는 물론 해외에서도 항일운동이 일어나면서 일제의 집권 체제가 도태되기 시작했다."},
        {2412, "3.1 운동이 끝난 후에는 이에 참여한 사람들이 '모두' 일본으로 유배되거나 처형되었다."},
        {2413, "2"},

        {2420, "Q2. 1910년 8월 29일, 대한제국의 국권이 일제에 의해 박탈되었다. 이에 대한 말로 적절하지 않은 것은?"},
        {2421, "대한민국이 탄생했다."},
        {2422, "대한제국은 사실상 해체되었다."},
        {2423, "1"},

        {2430, "Q3. 일제강점기 당시 항일운동을 전개한 독립운동가 중 한명으로, '대한독립만세'라는 구호로 독립을 외치며 운동했다. 이 인물은?"},
        {2431, "박영효"},
        {2432, "유관순"},
        {2433, "1"},

        {2440, "Q4. 다음 중 일제강점기 때 일어난 사건으로 옳지 않은 것은?"},
        {2441, "3.1 운동"},
        {2442, "국회 해산"},
        {2443, "2"},

        // 현대사 퀴즈
        {2810, "Q1. 대한민국에서 5대 대통령 중 가장 긴 임기를 가진 인물은 누구인가요?"},
        {2811, "박정희"},
        {2812, "박근혜"},
        {2813, "1"},

        {2820, "Q2. 6.25 전쟁을 시작한 북한 지도자는 누구인가요?"},
        {2821, "김일성"},
        {2822, "김정일"},
        {2823, "1"},

        {2830, "Q3. 대한민국에서 가장 오래된 정당은 무엇인가요?"},
        {2831, "새누리당"},
        {2832, "민주당"},
        {2833, "2"},

        {2840, "Q4. 1997년 대한민국을 강타한 금융위기의 원인 중 하나는 IMF가 대출을 중단한 것입니다. IMF은 어떤 기관의 약자인가요?"},
        {2841, "International Monetary Fund"},
        {2842, "Internal Market Foundation"},
        {2843, "1"},

    };

    void Start()
    {
        mainCamera.GetComponent<Camera>().orthographicSize = 12.0f; // 원래 값 6.12
        PlayerSpawn(curStage);
    }

    void Update()
    {
        if (mode == "Quiz") timer -= Time.deltaTime;
        else timer = 10.0f; // 문제 5초 보여주고 5초 동안 답 고르기
        Process();
    }

    private void Process() {
        switch(mode){
            case "Boss":
                ProcessBoss();
                break;
            case "Quiz":
                ProcessQuiz();
                break;
            case "Clear":
                ProcessClear();
                break;
        }
    }
    private void ProcessBoss() {
        if (isBossGen == false) {
            boss = Instantiate(_boss[(curStage/4)-1], bossSpawnPos[(curStage/4)-1].transform.position, bossSpawnPos[(curStage/4)-1].transform.rotation);
        }

        isBossGen = true;
        answerOne.SetActive(false);
        answerTwo.SetActive(false);
        answerOneBackground.SetActive(false);
        answerTwoBackground.SetActive(false);
        question.SetActive(false);
        questionTimer.SetActive(false);
        questionBackground.SetActive(false);
    }

    private void ProcessQuiz() {
        answerOne.SetActive(true);
        answerTwo.SetActive(true);
        answerOneBackground.SetActive(true);
        answerTwoBackground.SetActive(true);
        question.SetActive(true);
        questionTimer.SetActive(true);
        questionBackground.SetActive(true);
        isBossGen = false;
        Destroy(boss);
        // 정답 선택 효과
        if (answer == 1) {
            answerOne.GetComponent<TMP_Text>().outlineColor = Color.black;
            answerOne.GetComponent<TMP_Text>().outlineWidth = 0.2f;
            answerTwo.GetComponent<TMP_Text>().outlineWidth = 0;

        }
        else if (answer == 2) {
            answerTwo.GetComponent<TMP_Text>().outlineColor = Color.black;
            answerTwo.GetComponent<TMP_Text>().outlineWidth = 0.2f;
            answerOne.GetComponent<TMP_Text>().outlineWidth = 0;
        }

        // 문제 설정
        if (timer > 0 && timer < 10) {
            SetQuiz(curStage, patternIndex);
        }

        // 타이머 설정
        if (timer < 0) questionTimer.GetComponent<TMP_Text>().text = 0.ToString();
        else if (timer < 6) questionTimer.GetComponent<TMP_Text>().text = ((int)timer).ToString();
        else questionTimer.SetActive(false);

        // 정답 체크
        if (timer < 0 && isChecked == false) {
            CheckAnswer(curStage, patternIndex);
            isChecked = true;
        }

        // 보스로 전환
        if (timer < -5.0f)  {
            mode = "Boss";
            isChecked = false;
        }
    }
    private void SetQuiz(int curStage, int patternIndex) { // 퀴즈 설정
        question.GetComponent<TMP_Text>().text = Question[Int32.Parse(curStage.ToString() + (patternIndex+1).ToString() + 0.ToString())];
        answerOne.GetComponent<TMP_Text>().text = Question[Int32.Parse(curStage.ToString() + (patternIndex+1).ToString() + 1.ToString())];
        answerTwo.GetComponent<TMP_Text>().text = Question[Int32.Parse(curStage.ToString() + (patternIndex+1).ToString() + 2.ToString())];
    }

    private void ProcessClear() { // 클리어 시
        BossFinishFlag.SetActive(true);
    }

    private void CheckAnswer(int curStage, int patternIndex) { // 정답 체크
        if (answer == Int32.Parse(Question[Int32.Parse(curStage.ToString() + (patternIndex+1).ToString() + 3.ToString())])) { // 정답이면
            question.GetComponent<TMP_Text>().text = "정답입니다 !!";
        }
        else { // 오답이면
            question.GetComponent<TMP_Text>().text = "오답입니다...";
            isWrong = true;
        }
        if (patternIndex == 3) {
            mode = "Clear";
            question.GetComponent<TMP_Text>().text = "정답입니다!! 다음 스테이지로 이동하세요 !!";
        }
    }

    // Player 스폰 위치 설정
    public void PlayerSpawn(int curStage) {
        player.transform.position = playerSpawnPos[curStage/4-1].transform.position;
        qna.transform.position = playerSpawnPos[curStage/4-1].transform.position;
        patternIndex = -1;
        mode = "Boss";
        isBossGen = false;
        Debug.Log($"CurStage : {curStage}");
        Debug.Log($"patterIdx : {patternIndex}");

    }

    public void Respawn() {
        Destroy(boss);
        mode = "Boss";
        isBossGen = false;
        answerOne.SetActive(false);
        answerTwo.SetActive(false);
        answerOneBackground.SetActive(false);
        answerTwoBackground.SetActive(false);
        question.SetActive(false);
        questionTimer.SetActive(false);
        questionBackground.SetActive(false);
        patternIndex = -1;
    }

}