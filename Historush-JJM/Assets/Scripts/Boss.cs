using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject PurpleBullet, BlueBullet, GreenBullet;
    public GameObject player;
    public bool canShoot = true;
    Rigidbody2D rigid;

    const float moveDelay = 5f; // �¿� �͸����� �Ÿ�
    float moveTimer = 0;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Think", 2);
    }
    // Update is called once per frame
    void Update()
    {
        Boss_Move();
        Boss_Finish();
    }

    void Think() // ���� ���� ����
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
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
        }
    }

    void FireForward()
    {
        // Fire 4 Bullet Forward
        GameObject bulletR = Instantiate(BlueBullet, transform.position, transform.rotation); // �Ѿ� ���� (���� ������Ʈ, Vecter3 ��, ȸ����=�⺻��)
        bulletR.transform.position = transform.position + Vector3.right * 1.5f;
        GameObject bulletRR = Instantiate(BlueBullet, transform.position, transform.rotation);
        bulletRR.transform.position = transform.position + Vector3.right * 3.5f;
        GameObject bulletL = Instantiate(BlueBullet, transform.position, transform.rotation);
        bulletL.transform.position = transform.position + Vector3.left * 1.5f;
        GameObject bulletLL = Instantiate(BlueBullet, transform.position, transform.rotation);
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

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 1);
        else
            Invoke("Think", 3);
    }
    void FireShot()
    {
        // Fire 5 Random Shotgun Bullet to Player
        for (int index = 0; index < 7; index++)
        {
            GameObject bullet = Instantiate(BlueBullet, transform.position, transform.rotation); // �Ѿ� ���� (���� ������Ʈ, Vecter3 ��, ȸ����=�⺻��)
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
            Invoke("FireShot", 2);
        else
            Invoke("Think", 3);

    }
    void FireArc()
    {
        // Fire Arc Continue Fire
        GameObject bullet = Instantiate(BlueBullet, transform.position, transform.rotation); // �Ѿ� ���� (���� ������Ʈ, Vecter3 ��, ȸ����=�⺻��)
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);

    }
    void FireAround()
    {
        int roundNumA = 40;
        int roundNumB = 30;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        // Fire Around
        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = Instantiate(BlueBullet, transform.position, transform.rotation); // �Ѿ� ���� (���� ������Ʈ, Vecter3 ��, ȸ����=�⺻��)
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                                       , Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }

        // Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 3);
        else
            Invoke("Think", 3);

    }
    void Boss_Move() // ���� ������ ����
    {
        if (moveTimer > moveDelay / 2)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 10); // ������
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 10); // ����
        }
        if (moveTimer > moveDelay)
        {
            moveTimer = 0;
        }
        moveTimer += Time.deltaTime;
    }


    void Boss_Finish() // ���� Ŭ���� ���� = 60�� ��Ƽ��
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().bossStageTime > 60)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().NextStage();
            Destroy(gameObject);
        }
    }
}

