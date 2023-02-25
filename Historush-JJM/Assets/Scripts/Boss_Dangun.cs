using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Dangun : MonoBehaviour
{
    public GameObject garlic, sook;
    Vector2[] garlic_pos = new Vector2[] {new Vector2(-1.77f, 8.5f), new Vector2(7.56f, 8.5f), new Vector2(16.96f, 8.5f), new Vector2(26.23f, 8.5f)}; // 랜덤 마늘 공격 위치
    // -134
    Vector2[] sook_pos = new Vector2[] {new Vector2(-6, 0), new Vector2(31,0)}; // 랜덤 쑥 공격 위치
    GameObject player;
    public int curPatternCount; // 현재 공격 패턴의 반복 횟수
    public int[] maxPatternCount; // 공격 패턴 개수
    private int tempPIdx; // 오답 처리 시에 BossStageManage.patternIndex 저장용
    Vector3 pos; //현재위치
    public float delta; // 좌(우)로 이동가능한 (x)최대값
    public float maxSpeed; // 이동속도
    Rigidbody2D rb;

 

    private void Start()
    {
        pos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        Invoke("Think", 2);
    }

    private void Update()
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
                PhaseOne();
                break;

            case 3:
                PhaseTwo();
                break;

            case 4:
                Wrong();
                break;
        }
    }

    private void PhaseOne() // 마늘 떨어트리기
    {
        GameObject bullet = Instantiate(garlic, transform.position, transform.rotation);
        int rand_pos = Random.Range(0,4);
        bullet.transform.position = garlic_pos[rand_pos]; // 4개의 위치 중 랜덤하게 공격

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(0, -1), ForceMode2D.Impulse);

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseOne", 2f);
        else
            BossStageManage.mode = "Quiz";
    }
    private void PhaseTwo() // 쑥 휘두르기
    {
        GameObject bullet = Instantiate(sook, transform.position, transform.rotation);
        int rand_pos = Random.Range(0,2);

        bullet.transform.position = sook_pos[rand_pos]; // 왼쪽 오른쪽 중 랜덤하게 공격
        if (rand_pos == 1) bullet.transform.Rotate(new Vector3(0,-180,0)); // 오른쪽의 경우 좌우 반전

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseTwo", 3f);
        else
            BossStageManage.mode = "Quiz";
    }

    private void Wrong() {
        for (int index = 0; index < 3; index++)
        {
            GameObject bullet = Instantiate(garlic, transform.position, transform.rotation);
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-5f, 5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 50, ForceMode2D.Impulse);
        }
        curPatternCount++;
        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex]) {
            Invoke("Wrong", 0.1f);
        }
        else {
            BossStageManage.patternIndex = tempPIdx;
            BossStageManage.isWrong = false;
            Invoke("Think", 3.0f);
        }
    }
}
