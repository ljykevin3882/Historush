using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Jumong : MonoBehaviour
{
    public GameObject arrow;

    GameObject player;

    static public int patternIndex = 0;
    public int curPatternCount;
    public int[] maxPatternCount;
    private int tempPIdx;


    Vector3 pos; //현재위치
    public float delta; // 좌(우)로 이동가능한 (x)최대값
    public float maxSpeed; // 이동속도

    Rigidbody2D rb;

 

    void Start()
    {
        pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        //MovePattern_2_1();
        FireShot();
    }

    void Update()
    {
        
    }


    void Think()
    {
        if (BossStageManage.isWrong == true) {
            tempPIdx = patternIndex;
            patternIndex = 4;
        }
        else {
            patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        }
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;

            case 1:
                FireShot();
                break;

            case 2:
                FireArc();
                break;

            case 3:
                FireAround();
                break;

            case 4:
                FireWrongAnswer();
                break;
        }
    }

    void FireForward()
    {
        // Fire 4 Bullet Forward
        GameObject bulletR = Instantiate(arrow, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
        bulletR.transform.position = transform.position + Vector3.right * 1.5f;
        GameObject bulletRR = Instantiate(arrow, transform.position, transform.rotation);
        bulletRR.transform.position = transform.position + Vector3.right * 3.5f;
        GameObject bulletL = Instantiate(arrow, transform.position, transform.rotation);
        bulletL.transform.position = transform.position + Vector3.left * 1.5f;
        GameObject bulletLL = Instantiate(arrow, transform.position, transform.rotation);
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

        if (curPatternCount < maxPatternCount[patternIndex]) {
            Invoke("FireForward", 1);
        }
        else {
            BossStageManage.mode = "Quiz";
            //Invoke("Think", 3);
        }

    }
    void FireShot()
    {

        for (int index = 0; index < 7; index++)
        {
            GameObject bullet = Instantiate(arrow, transform.position, transform.rotation);
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-5f, 5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 12, ForceMode2D.Impulse);
        }

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 1.5f);
        else
            BossStageManage.mode = "Quiz";
            //Invoke("Think", 3);

    }
    void FireArc()
    {
        // Fire Arc Continue Fire
        GameObject bullet = Instantiate(arrow, transform.position, transform.rotation);
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            BossStageManage.mode = "Quiz";
            //Invoke("Think", 3);

    }
    void FireAround()
    {
        int roundNumA = 40;
        int roundNumB = 30;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        // Fire Around
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = Instantiate(arrow, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                                       , Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
        }

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 2);
        else
            BossStageManage.mode = "Quiz";
            //Invoke("Think", 3);

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
        if (curPatternCount < maxPatternCount[patternIndex]) {
            Invoke("FireWrongAnswer", 0.1f);
        }
        else {
            patternIndex = tempPIdx;
            BossStageManage.isWrong = false;
            Invoke("Think", 3.0f);
        }
    }

    void MovePattern_1() { // 좌우로 반복 이동
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * maxSpeed);
        transform.position = v;  
    }
    void MovePattern_2_1() { // 플레이어에게 박치기
        //if (Mathf.Abs(rb.velocity.x) < maxSpeed) rb.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        //else rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        Vector2 dirVec = player.transform.position - transform.position;
        rb.AddForce(dirVec.normalized * 20, ForceMode2D.Impulse);
        Invoke("MovePattern_2_2", 2.0f);
    }
    void MovePattern_2_2() {
        rb.velocity = new Vector2(0,0);

        Invoke("MovePattern_2_1", 1.0f);
    }
}
