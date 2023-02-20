using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Sejong : MonoBehaviour
{
    public GameObject[] hangles;
    GameObject player;
    public int curPatternCount; // 현재 공격 패턴의 반복 횟수
    public int[] maxPatternCount; // 공격 패턴 개수
    private int tempPIdx; // 오답 처리 시에 BossStageManage.patternIndex 저장용
    Vector3 pos; // 현재위치
    public float delta; // 좌(우)로 이동가능한 (x)최대값
    public float maxSpeed; // 이동속도
    Rigidbody2D rb;
    private int BulletIdx = 0;

    void Start()
    {
        pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        Invoke("Think", 2);

    }
    void Update()
    {
        MovePattern_1();
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
                Wrong();
                break;
        }
    }

    private void PhaseOne() // 아래로 4발 발사
    {
        GameObject bulletR = Instantiate(hangles[3], transform.position, transform.rotation);
        bulletR.transform.position = transform.position + Vector3.right * 1.5f;
        GameObject bulletRR = Instantiate(hangles[2], transform.position, transform.rotation);
        bulletRR.transform.position = transform.position + Vector3.right * 3.5f;
        GameObject bulletL = Instantiate(hangles[1], transform.position, transform.rotation);
        bulletL.transform.position = transform.position + Vector3.left * 1.5f;
        GameObject bulletLL = Instantiate(hangles[0], transform.position, transform.rotation);
        bulletLL.transform.position = transform.position + Vector3.left * 3.5f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);


        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex]) {
            Invoke("PhaseOne", 1);
        }
        else {
            BossStageManage.mode = "Quiz";
        }

    }
    private void PhaseTwo() // 7발 플레이어에게 산탄으로 발사
    {

        for (int index = 0; index < 7; index++)
        {
            GameObject bullet = Instantiate(hangles[index], transform.position, transform.rotation);
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-15f, 15f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 12, ForceMode2D.Impulse);
        }

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseTwo", 1.5f);
        else
            BossStageManage.mode = "Quiz";

    }
    private void PhaseThree() // 한발씩 시계방향으로 발사
    {
        // Fire Arc Continue Fire
        GameObject bullet = Instantiate(hangles[BulletIdx], transform.position, transform.rotation);
        if (BulletIdx == 13) BulletIdx = 0;
        else BulletIdx++;
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[BossStageManage.patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseThree", 0.15f);
        else
            BossStageManage.mode = "Quiz";
            //Invoke("Think", 3);

    }
    private void PhaseFour() // 원형으로 발사
    {
        int roundNumA = 40;
        int roundNumB = 30;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        // Fire Around
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = Instantiate(hangles[13], transform.position, transform.rotation);
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                                       , Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
        }

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[BossStageManage.patternIndex])
            Invoke("PhaseFour", 2);
        else
            BossStageManage.mode = "Quiz";

    }
    private void Wrong() { // 오답일 시 공격
        for (int index = 0; index < 10; index++)
        {
            GameObject bullet = Instantiate(hangles[10], transform.position, transform.rotation);
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

    private void MovePattern_1() { // 좌우로 반복 이동
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * maxSpeed);
        transform.position = v;  
    }


}