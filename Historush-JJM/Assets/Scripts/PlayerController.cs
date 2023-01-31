using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    public FloatingJoystick joy;
    public FixedJoystick fixjoy;
    public Sprite Bear,Human1;
    public GameManager gameManager;
    public RuntimeAnimatorController Bear_animator,Human1_animator;
    public int SookGarlic=0;
    public float maxSpeed;
    public float maxFallSpeed; //낙하 속도 조정
    public float Speed;
    public float jumpPower;
    public int jumpCount;
    public bool isSliding;
    public CustomMenu Custom_Menu;
    Rigidbody2D rigid;
    public TreasurePopUp TreasurePopup;
    public GameObject TreasurePopupObject,SettingPopup;
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
    public void settingButton()
    {
        SettingPopup.SetActive(true);
        Time.timeScale = 0;
    }
    private void Update()
    {
        //Move by control
        float h = Input.GetAxisRaw("Horizontal"); //키보드 입력
        h += fixjoy.Horizontal; //조이스틱 입력




        rigid.AddForce(Vector2.right * h*Speed, ForceMode2D.Impulse);
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
        if (rigid.velocity.y < maxFallSpeed) //낙하 최대속도를 초과해 떨어지면
        {
            
            rigid.velocity = new Vector2(rigid.velocity.x, maxFallSpeed);
        }

        //PC 슬라이드-키입력.
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
        
        Debug.DrawRay(rigid.position, Vector3.up, new Color(0, 1, 0));
        RaycastHit2D jumpPlatform = Physics2D.Raycast(rigid.position, Vector3.up,1f, LayerMask.GetMask("Platform"));
        if (rigid.velocity.y > 0 && jumpCount<2)
        {
            if (jumpPlatform.collider!=null&&jumpPlatform.collider.tag == "Platform" )
            {
                print("dds");
                capsuleCollider.isTrigger = true;
            }

        }


        //LandingPlatform
        if (rigid.velocity.y < 0)
        {
            if (jumpPlatform.collider == null)
            {
                capsuleCollider.isTrigger = false; //캐릭터가 내려오면서 머리위에 아무것도 없으면 다시 물리적용되게
            }
            //capsuleCollider.isTrigger = false; //내려올때는 다시 collider 만들기
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayhit.collider!=null&&rayhit.collider.tag == "Platform"&&jumpPlatform.collider==null)
            {
                if (rayhit.distance < 1f)
                {
                    anim.SetBool("isJumping", false);
                    jumpCount = 2;
                }
            }
        }
        Speed = 1;
        RaycastHit2D leftslope = Physics2D.Raycast(rigid.position, Vector3.left, 1f, LayerMask.GetMask("Platform"));
        RaycastHit2D rightslope = Physics2D.Raycast(rigid.position, Vector3.right, 1f, LayerMask.GetMask("Platform"));
        //if (rightslope.collider.tag == "Platform" && rigid.velocity.x > 0) //우측에 경사가 있고, 우측으로 이동한다면 
        if (rightslope.collider!=null&& rigid.velocity.x > 0)
        {
            Speed = 1.7f;
        }
        //if (leftslope.collider.tag == "Platform" && rigid.velocity.x < 0) //좌측에 경사가 있고, 좌측으로 이동한다면
        if(leftslope.collider!=null&& rigid.velocity.x < 0)
        {
            Speed = 1.7f;
        }
        
        
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Enemy")
            {
                if (rigid.velocity.y<-0.01)
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
    void HumanChange()
    {
        spriteRenderer.sprite = Human1;
        anim.runtimeAnimatorController = Human1_animator;
    }
    void BearChange()
    {
        spriteRenderer.sprite = Bear;
        anim.runtimeAnimatorController = Bear_animator;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Answer_O") BossStageManage.answer = true;
        else if (collision.gameObject.tag == "Answer_X") BossStageManage.answer = false;
        if (collision.gameObject.tag == "Enemy") OnDamaged(collision.transform.position);


        if (collision.gameObject.tag == "SookGarlic")
        {
            AudioSource CoinSound = GetComponent<AudioSource>();
            CoinSound.Play();
            SookGarlic++; 
            if(SookGarlic >= 10) //쑥마늘 먹으면 사람으로 변함
            {
                gameManager.LoadPlayerDataFromJson();
                gameManager.playerData.avatar = 1; //현재 아바타 데이터를 저장, 1 이면 사람
                gameManager.SavePlayerDataToJson();
                //사람으로 변하는 애니메이션 
                anim.SetTrigger("Change");
                Invoke("HumanChange", 1);
            }
            gameManager.SookGarlicCount.text = (gameManager.player.SookGarlic).ToString();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Item")
        {

            //점수얻기
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");
            AudioSource CoinSound = GetComponent<AudioSource>();
            CoinSound.Play();
            if (isBronze)
                gameManager.stagePoint += 50;
            else if (isSilver)
                gameManager.stagePoint += 100;
            else if (isGold)
                gameManager.stagePoint += 300;

            gameManager.UIPoint.text = (gameManager.totalPoint + gameManager.stagePoint).ToString();
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
            gameManager.UIPoint.text = (gameManager.totalPoint + gameManager.stagePoint).ToString();
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
        if (collision.gameObject.tag == "BossFinish") //깃발 먹으면
        {
            //다음 스테이지
            SceneManager.LoadScene("LoadingSceneToMain");
            gameManager.LoadPlayerDataFromJson();
            gameManager.playerData.MaxStageLevel = gameManager.playerData.MaxStageLevel + 1;
            gameManager.SavePlayerDataToJson();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Platform") 
        //    capsuleCollider.isTrigger = false; 
    }
    void OnAttack(Transform enemy)
    {
        AudioSource CoinSound = GetComponent<AudioSource>();
        CoinSound.Play();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        gameManager.stagePoint += 100;
        //적 죽음
        Enemy_Move enemyMove = enemy.GetComponent<Enemy_Move>();
        enemyMove.OnDamaged();
        gameManager.UIPoint.text = (gameManager.totalPoint + gameManager.stagePoint).ToString();
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
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
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
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }
    public void OnDie()
    {
        //색 변경
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
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
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        capsuleCollider.enabled = true;
    }
    public void avatarChange()
    {
        if (gameManager.playerData.avatar == 0) //곰
        {
            spriteRenderer.sprite = Bear;
            anim.runtimeAnimatorController = Bear_animator;
        }
        else //사람
        {
            spriteRenderer.sprite = Human1;
            anim.runtimeAnimatorController = Human1_animator;
        }
    }
    public void avatarColorChange()
    {

        if (gameManager.playerData.avatar_color == 1)
        {
            spriteRenderer.color = Custom_Menu.red;
        }
        else if (gameManager.playerData.avatar_color == 2)
        {
            spriteRenderer.color = Custom_Menu.orange;
        }
        else if (gameManager.playerData.avatar_color == 3)
        {
            spriteRenderer.color = Custom_Menu.yellow;
        }
        else if (gameManager.playerData.avatar_color == 4)
        {
            spriteRenderer.color = Custom_Menu.lightgreen;
        }
        else if (gameManager.playerData.avatar_color == 5)
        {
            spriteRenderer.color = Custom_Menu.green;
        }
        else if (gameManager.playerData.avatar_color == 6)
        {
            spriteRenderer.color = Custom_Menu.skyblue;
        }
        else if (gameManager.playerData.avatar_color == 7)
        {
            spriteRenderer.color = Custom_Menu.blue;
        }
        else if (gameManager.playerData.avatar_color == 8)
        {
            spriteRenderer.color = Custom_Menu.nam;
        }
        else if (gameManager.playerData.avatar_color == 9)
        {
            spriteRenderer.color = Custom_Menu.purple;
        }
        else if (gameManager.playerData.avatar_color == 10)
        {
            spriteRenderer.color = Custom_Menu.gray;
        }
        else if (gameManager.playerData.avatar_color == 11)
        {
            spriteRenderer.color = Custom_Menu.white;
        }
    }
}
