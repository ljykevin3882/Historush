using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Jumong : MonoBehaviour
{
    public GameObject FireEveryWhereMovePos;
    public GameObject arrow;
    GameObject player;
    public int curPatternCount; // 현재 공격 패턴의 반복 횟수
    public int[] maxPatternCount; // 공격 패턴 개수
    private int tempPIdx; // 오답 처리 시에 BossStageManage.patternIndex 저장용
    Vector3 pos; //현재위치
    public float delta; // 좌(우)로 이동가능한 (x)최대값
    public float maxSpeed; // 이동속도
    Rigidbody2D rb;

 

    void Start()
    {
        FireEveryWhereMovePos = GameObject.Find("PlayerSpawnPos2");
        pos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        Invoke("Think", 2);
    }

    void Update()
    {
        // if (BossStageManage.patternIndex == 1 || BossStageManage.patternIndex == 2) { // 살짝 아래로 이동
        //     Vector2 movePos = new Vector2(pos.x, pos.y-10);
        //     transform.position = Vector2.MoveTowards(transform.position, movePos, maxSpeed * Time.deltaTime * 10);
        // }
        // if (BossStageManage.patternIndex == 2) { // 좌우로 이동
        //     Vector3 v = pos;
        //     v.x += delta * Mathf.Sin(Time.time * maxSpeed);
        //     transform.position = v;  
        // }

    }


    private void Think() // 보스 공격 패턴 관리
    {
        if (BossStageManage.isWrong == true) { // 틀렸을시
            tempPIdx = BossStageManage.patternIndex;
            BossStageManage.patternIndex = 4;
        }
        else {
            BossStageManage.patternIndex = BossStageManage.patternIndex == 3 ? 0 : BossStageManage.patternIndex + 1;
            // 3이면 0, 아니면 ++
        }
        curPatternCount = 0;

        switch (BossStageManage.patternIndex)
        {
            case 0:
                PhaseOne();
                break;

            case 1:
                PhaseTwo();
                break;

            case 2:
                PhaseThree();
                break;

            case 3:
                PhaseFour();
                break;

            case 4:
                FireWrongAnswer();
                break;
        }
    }

 
    private void PhaseOne() // 부채꼴 모양으로 화살 발사
    {

        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = Instantiate(arrow, transform.position, transform.rotation);
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-10f, 10f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 12, ForceMode2D.Impulse);
        }

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseOne", 1.5f);
        else
            BossStageManage.mode = "Quiz";
    }

    private void PhaseTwo() { // 아래로 무작위 난사
    
        GameObject bullet = Instantiate(arrow, transform.position, transform.rotation);
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = transform.position;
        Vector2 randVec = new Vector2(Random.Range(-1000f, 1000f), Random.Range(-1000f, 0));
        dirVec += randVec;
        rigid.AddForce(dirVec.normalized * 12, ForceMode2D.Impulse);

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseTwo", 0.2f);
        else
            BossStageManage.mode = "Quiz";
    }


    private void PhaseThree() // 아래로 10발 한번에 발사
    {
        for (int index = 0; index < 10; index++)
        {
            GameObject bullet = Instantiate(arrow, transform.position, transform.rotation);
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = transform.position;
            Vector2 randVec = new Vector2(Random.Range(-1000f, 1000f), Random.Range(-1000f, 0));
            dirVec += randVec;
            rigid.AddForce(dirVec.normalized * 12, ForceMode2D.Impulse);
        }

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseThree", 2f);
        else
            BossStageManage.mode = "Quiz";

    }
    void PhaseFour() { // 플레이어에게 박치기
        Vector2 dirVec = player.transform.position - transform.position;
        rb.AddForce(dirVec.normalized * 20, ForceMode2D.Impulse);

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseFour_2", 2f);
        else
            BossStageManage.mode = "Quiz";

    }
    void PhaseFour_2() {
        rb.velocity = new Vector2(0,0);

        Invoke("PhaseFour", 1.0f);
    }
    void FireWrongAnswer() {
        for (int index = 0; index < 10; index++)
        {
            GameObject bullet = Instantiate(arrow, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-5f, 5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 50, ForceMode2D.Impulse);
        }
        curPatternCount++;
        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex]) {
            Invoke("FireWrongAnswer", 0.1f);
        }
        else {
            BossStageManage.patternIndex = tempPIdx;
            BossStageManage.isWrong = false;
            Invoke("Think", 3.0f);
        }
    }

}
