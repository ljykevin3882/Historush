using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FloatingJoystick joy;
    public FixedJoystick fixjoy;

    public GameManager gameManager;
    public int SookGarlic=0;
    public float maxSpeed;
    public float jumpPower;
    public int jumpCount;
    public bool isSliding;
    Rigidbody2D rigid;
    public TreasurePopUp TreasurePopup;
    public GameObject TreasurePopupObject;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    public float speed_Character = 5f;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Jump() //점프 버튼 (모바일)
    {
        if (jumpCount > 0)
        {
            jumpCount--;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }
    public void SlideOn() //슬라이드 버튼 누르면 (모바일)
    {
        if (spriteRenderer.flipX == false ) //오른쪽으로 가고있을 때
        {
            transform.Rotate(0, 0, 90);
            isSliding = true;
        }
        if(spriteRenderer.flipX == true )
        {
            transform.Rotate(0, 0, -90);
            isSliding = true;
        }
        print("눌렀음");
    }
    public void SlideOff() //슬라이드 버튼 떼면 (모바일)
    {
        if (spriteRenderer.flipX == false && isSliding==true)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            isSliding = false;
        }
        if (spriteRenderer.flipX == true && isSliding == true)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            isSliding = false;
        }
        print("뗐음");
    }

    private void Update()
    {
        //Move by control
        float h = Input.GetAxisRaw("Horizontal");
        h += fixjoy.Horizontal;
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (h > 0){ //오른쪽
            spriteRenderer.flipX = false;
        }
        else if(h<0) //왼쪽
        {
            spriteRenderer.flipX = true;
        }
        if (rigid.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(rigid.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (rigid.velocity.x > maxSpeed) //오른쪽 최대속도
        {
            anim.SetBool("IsWalking", true);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < maxSpeed * (-1)) //왼쪽 최대속도
        {
            
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            
        }

        //PC 슬라이드-키입력
        if (Input.GetButtonDown("Slide"))
        {
            if (spriteRenderer.flipX == false) //오른쪽으로 가고있을 때
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
                isSliding = true;
            }
            if (spriteRenderer.flipX == true)
            {
                transform.localEulerAngles = new Vector3(0, 0, -90);
                isSliding = true;
            }
        }
        if (Input.GetButtonUp("Slide"))
        {
            if (spriteRenderer.flipX == false && isSliding == true)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                isSliding = false;
            }
            if (spriteRenderer.flipX == true && isSliding == true)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                isSliding = false;
            }
        }
        if (isSliding == true) //슬라이드 누른채로 방향전환될때
        {
            if (h > 0) //우측입력되면 
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else if (h < 0) //좌측입력되면
            {
                transform.localEulerAngles = new Vector3(0, 0, -90);
            }
        }
        //점프할때
        if (Input.GetButtonDown("Jump") && jumpCount > 0)//&&!anim.GetBool("isJumping")
        {
            jumpCount--;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        //방향키 떼서 멈출때 
        if (Input.GetButtonUp("Horizontal")||h==0)
        {

            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.2f, rigid.velocity.y);
        }
        
        //방향전환
        //       if (Input.GetButton("Horizontal"))
        //           spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (rigid.velocity.x < 0.5 && rigid.velocity.x > -0.5) //Math.abs(rigid.velocity.x)<0.3 과 같은 코드 -> 내코드가 보기 쉬워서
        {
            rigid.velocity = new Vector2(rigid.velocity.x * 0.2f, rigid.velocity.y);
            anim.SetBool("IsWalking", false);
            //rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        else
        {
            anim.SetBool("IsWalking", true);
        }
        
    }

    private void FixedUpdate()
    {
        if (rigid.velocity.y > 0)
        {
            Debug.DrawRay(rigid.position, Vector3.up, new Color(0, 1, 0));
            RaycastHit2D jumpPlatform = Physics2D.Raycast(rigid.position, Vector3.up, 0.01f, LayerMask.GetMask("Platform"));

            if (jumpPlatform.transform.gameObject.tag == "Platform")
            {
                capsuleCollider.isTrigger = true;
            }
        }
        
        //LandingPlatform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayhit.collider.tag == "Platform")
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
                if (rigid.position.y>collision.gameObject.transform.position.y)
                {
                    OnAttack(collision.transform);
                    
                }
                else
                {
                    OnDamaged(collision.transform.position);
                    print("collision작동");
                }
            }
            else//spike 일때
            {
                OnDamaged(collision.transform.position);

            }
        }

    }
   
    
    void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.gameObject.tag == "SookGarlic")
        {
            SookGarlic++; 
            if(SookGarlic == 10)
            {
                spriteRenderer.color = new Color(0, 0, 0, 1f);
            }
            collision.gameObject.SetActive(false);
        }
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
        if (collision.gameObject.tag == "Treasure")
        {
            gameManager.stagePoint += 500;
            gameManager.playerData.items.SetValue(true,int.Parse(collision.gameObject.name)); //DB에 먹은 유물 true로 만들기
            TreasurePopup.TreasurePopupNum = int.Parse(collision.gameObject.name);
            TreasurePopup.TreasurePopupBool = true;
            TreasurePopupObject.SetActive(true);

            collision.gameObject.SetActive(false);

        }
        if (collision.gameObject.tag == "Finish") //깃발 먹으면
        {
            //다음 스테이지
            gameManager.NextStage(); //다음스테이지로 이동, 
            if (gameManager.playerData.MaxStageLevel < gameManager.stageIndex) //DB의 저장된 최고 스테이지보다 현재스테이지가 크면
            {
                gameManager.playerData.MaxStageLevel = gameManager.stageIndex; //갱신
            }
            
            gameManager.playerData.score = gameManager.totalPoint; //점수 갱신
            gameManager.SavePlayerDataToJson(); //DB에 저장
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform") 
            capsuleCollider.isTrigger = false; 
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
