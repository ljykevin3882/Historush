using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BossStageManage : MonoBehaviour
{
    public GameObject[] playerSpawnPos;
    public GameObject[] bossSpawnPos;
    static public int curStage = 4; // 4, 8, 12, 16, 20, 24, 28

    float timer; 
    public GameObject player, qna;
    public GameObject[] _boss;
    public GameObject answerOne, answerOneBackground, answerTwo, answerTwoBackground;
    public GameObject mainCamera;
    public GameObject question, questionBackground, questionTimer;
    public GameObject BossFinishFlag;
    private GameObject boss;
    private bool isChecked = false;

    static public int answer;
    static public string mode = "Boss";
    static private bool isBossGen = false;
    static public bool isWrong = false;
    static public int patternIndex = -1; // 기본값 -1

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
        {810, "Q1. 관려전을 지급하고, 녹읍을 폐지한 왕은?"},
        {811, "진흥왕"},
        {812, "신문왕"},
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
        // 고려 퀴즈
        // 조선 퀴즈
        // 일제강점기 퀴즈
        // 현대사 퀴즈

    };

    void Start()
    {
        mainCamera.GetComponent<Camera>().orthographicSize = 12.0f; // 원래 값 6.12
        PlayerSpawn();
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
            Debug.Log($"CurrentStage : {curStage}");
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

    private void PlayerSpawn() {
        if (curStage == 4) {
            player.transform.position = playerSpawnPos[0].transform.position;
            qna.transform.position = playerSpawnPos[0].transform.position;
        }
        else if (curStage == 8) {
            player.transform.position = playerSpawnPos[1].transform.position;
            qna.transform.position = playerSpawnPos[1].transform.position;
        }
    }

}