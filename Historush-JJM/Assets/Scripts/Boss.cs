using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject Hangle_1, Hangle_2, Hangle_3, Hangle_4, Hangle_5, Hangle_6, Hangle_7, Hangle_8, Hangle_9, Hangle_10, Hangle_11, Hangle_12, Hangle_13, Hangle_14; 

    const float moveDelay = 5f; // ?вп? ??????? ???
    float moveTimer = 0;
    GameObject player;

    static public int patternIndex = -1;
    public int curPatternCount;
    public int[] maxPatternCount;
    static public bool isWrong = false;
    private int tempPIdx;
    private int moveSpeedOne = 180;
    private float moveSpeedTwo = 0.3f;
    private int changeDir = 0;


    void Start()
    {
        player = GameObject.Find("Player");
        Invoke("Think", 2);

    }
    void Update()
    {
        BossMoveOne();
    }

    void Think() // ???? ???? ????
    {
        if (isWrong == true) {
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
        GameObject bulletR = Instantiate(Hangle_1, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
        bulletR.transform.position = transform.position + Vector3.right * 1.5f;
        GameObject bulletRR = Instantiate(Hangle_1, transform.position, transform.rotation);
        bulletRR.transform.position = transform.position + Vector3.right * 3.5f;
        GameObject bulletL = Instantiate(Hangle_1, transform.position, transform.rotation);
        bulletL.transform.position = transform.position + Vector3.left * 1.5f;
        GameObject bulletLL = Instantiate(Hangle_1, transform.position, transform.rotation);
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
        // Fire 5 Random Shotgun Bullet to Player
        for (int index = 0; index < 7; index++)
        {
            GameObject bullet = Instantiate(Hangle_1, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
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
        GameObject bullet = Instantiate(Hangle_1, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
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
            GameObject bullet = Instantiate(Hangle_1, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
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
            GameObject bullet = Instantiate(Hangle_1, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
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
            isWrong = false;
            Invoke("Think", 3.0f);
        }
    }
    void BossMoveOne() // ???? ?????? ????
    {
        if (moveTimer > moveDelay / 2)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 10); // ??????
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 10); // ????
        }
        if (moveTimer > moveDelay)
        {
            moveTimer = 0;
        }
        moveTimer += Time.deltaTime;
    }
    void BossMoveTwo() {
        if (moveSpeedOne == 0) changeDir = 1;
        else if (moveSpeedOne == 180) changeDir = 0;

        if (changeDir == 1) {
            moveSpeedOne++;
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeedOne * moveSpeedTwo);
        }
        else if (changeDir == 0) {
            moveSpeedOne--;
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeedOne * moveSpeedTwo);
        }
            // GameObject bullet = Instantiate(BlueBullet, transform.position, transform.rotation); // ??? ???? (???? ???????, Vecter3 ??, ?????=????)
            // bullet.transform.position = transform.position;

            // Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            // Vector2 dirVec = player.transform.position - transform.position;
            // Vector2 ranVec = new Vector2(Random.Range(-5f, 5f), Random.Range(0f, 2f));
            // dirVec += ranVec;
            // rigid.AddForce(dirVec.normalized * 12, ForceMode2D.Impulse);
    }

}