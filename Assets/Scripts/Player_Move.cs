using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public GameManager gameManager;
    public float maxSpeed;
    public float jumpPower;
    public int jumpCount;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    public float speed_Character = 5f;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        //Move by control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);


        if (rigid.velocity.x > maxSpeed) //오른쪽 
        {
            anim.SetBool("IsWalking", true);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);


        }

        else if (rigid.velocity.x < maxSpeed * (-1)) //왼쪽
        {
            anim.SetBool("IsWalking", true);
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        }

        //슬라이드
        if (Input.GetButtonDown("Slide"))
        {
            transform.Rotate(0, 0, 90);

        }
        if (Input.GetButtonUp("Slide"))
        {
            transform.Rotate(0, 0, -90);

        }
        //점프할때
        if (Input.GetButtonDown("Jump") && jumpCount > 0)//&&!anim.GetBool("isJumping")
        {
            jumpCount--;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        //방향키 떼서 멈출때 

        if (Input.GetButtonUp("Horizontal"))
        {

            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //방향전환

        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (rigid.velocity.x < 0.3 && rigid.velocity.x > -0.3) //Math.abs(rigid.velocity.x)<0.3 과 같은 코드
        {
            anim.SetBool("IsWalking", false);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }
    }

    private void FixedUpdate()
    {

        //LandingPlatform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayhit.collider != null)
            {
                if (rayhit.distance < 1f)
                {
                    anim.SetBool("isJumping", false);
                    jumpCount = 2;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Enemy")
            {
                if (rigid.velocity.y <= 0 && transform.position.y > collision.transform.position.y)
                {
                    OnAttack(collision.transform);
                }
                else
                {
                    OnDamaged(collision.transform.position);
                }
            }
            else
            {
                OnDamaged(collision.transform.position);

            }
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            OnDamaged(collision.transform.position);

        if (collision.gameObject.tag == "Item")
        {
            //점수얻기
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
                gameManager.stagePoint += 50;
            else if (isSilver)
                gameManager.stagePoint += 100;
            else if (isGold)
                gameManager.stagePoint += 300;


            //아이템 사라지기
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            //다음 스테이지
            gameManager.NextStage();
        }
    }
    void OnAttack(Transform enemy)
    {

        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        gameManager.stagePoint += 100;
        //적 죽음
        Enemy_Move enemyMove = enemy.GetComponent<Enemy_Move>();
        enemyMove.OnDamaged();
    }
    void OnDamaged(Vector2 targetPos)
    {
        //체력감소
        if (gameObject.layer == 10)
        {
            gameManager.HealthDown();


            //래이어 변경
            gameObject.layer = 11;
            //색변경
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            //넉백
            int dirc = transform.position.x - targetPos.x >= 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
            //애니메이션
            anim.SetTrigger("doDamaged");
            Invoke("OffDamaged", 2);
        }
    }
    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    public void OnDie()
    {
        //색 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //y축 변경
        spriteRenderer.flipY = true;
        //콜라이더 비활성화
        capsuleCollider.enabled = false;
        //죽는 점프 효과
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

    }
    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;

    }
    public void Respawn()  //3번 죽고 부활할때 위치 원위치. 색깔 원위치
    {
        spriteRenderer.flipY = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        capsuleCollider.enabled = true;
    }

}