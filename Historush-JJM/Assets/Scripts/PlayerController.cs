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
    public float maxFallSpeed; //���� �ӵ� ����
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

    public void Jump() //���� ��ư (�����)
    {
        if (jumpCount > 0)
        {
            jumpCount--;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }
    public void SlideOn() //�����̵� ��ư ������ (�����)
    {
        if (spriteRenderer.flipX == false ) //���������� �������� ��
        {
            transform.Rotate(0, 0, 90);
            isSliding = true;
        }
        if(spriteRenderer.flipX == true )
        {
            transform.Rotate(0, 0, -90);
            isSliding = true;
        }
        print("������");
    }
    public void SlideOff() //�����̵� ��ư ���� (�����)
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
        print("����");
    }
    public void settingButton()
    {
        SettingPopup.SetActive(true);
        Time.timeScale = 0;
    }
    private void Update()
    {
        //Move by control
        float h = Input.GetAxisRaw("Horizontal"); //Ű���� �Է�
        h += fixjoy.Horizontal; //���̽�ƽ �Է�




        rigid.AddForce(Vector2.right * h*Speed, ForceMode2D.Impulse);
        if (h > 0){ //������
            spriteRenderer.flipX = false;
        }
        else if(h<0) //����
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

        if (rigid.velocity.x > maxSpeed) //������ �ִ�ӵ�
        {
            anim.SetBool("IsWalking", true);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < maxSpeed * (-1)) //���� �ִ�ӵ�
        {
            
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            
        }
        if (rigid.velocity.y < maxFallSpeed) //���� �ִ�ӵ��� �ʰ��� ��������
        {
            
            rigid.velocity = new Vector2(rigid.velocity.x, maxFallSpeed);
        }

        //PC �����̵�-Ű�Է�.
        if (Input.GetButtonDown("Slide"))
        {
            if (spriteRenderer.flipX == false) //���������� �������� ��
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
        if (isSliding == true) //�����̵� ����ä�� ������ȯ�ɶ�
        {
            if (h > 0) //�����ԷµǸ� 
            {
                transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            else if (h < 0) //�����ԷµǸ�
            {
                transform.localEulerAngles = new Vector3(0, 0, -90);
            }
        }
        //�����Ҷ�
        if (Input.GetButtonDown("Jump") && jumpCount > 0)//&&!anim.GetBool("isJumping")
        {
            jumpCount--;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        //����Ű ���� ���⶧ 
        if (Input.GetButtonUp("Horizontal")||h==0)
        {

            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.2f, rigid.velocity.y);
        }
        
        //������ȯ
        //       if (Input.GetButton("Horizontal"))
        //           spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (rigid.velocity.x < 0.5 && rigid.velocity.x > -0.5) //Math.abs(rigid.velocity.x)<0.3 �� ���� �ڵ� -> ���ڵ尡 ���� ������
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
                capsuleCollider.isTrigger = false; //ĳ���Ͱ� �������鼭 �Ӹ����� �ƹ��͵� ������ �ٽ� ��������ǰ�
            }
            //capsuleCollider.isTrigger = false; //�����ö��� �ٽ� collider �����
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
        //if (rightslope.collider.tag == "Platform" && rigid.velocity.x > 0) //������ ��簡 �ְ�, �������� �̵��Ѵٸ� 
        if (rightslope.collider!=null&& rigid.velocity.x > 0)
        {
            Speed = 1.7f;
        }
        //if (leftslope.collider.tag == "Platform" && rigid.velocity.x < 0) //������ ��簡 �ְ�, �������� �̵��Ѵٸ�
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
                    print("collision�۵�");
                }
            }
            else//spike �϶�
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
            if(SookGarlic >= 10) //������ ������ ������� ����
            {
                gameManager.LoadPlayerDataFromJson();
                gameManager.playerData.avatar = 1; //���� �ƹ�Ÿ �����͸� ����, 1 �̸� ���
                gameManager.SavePlayerDataToJson();
                //������� ���ϴ� �ִϸ��̼� 
                anim.SetTrigger("Change");
                Invoke("HumanChange", 1);
            }
            gameManager.SookGarlicCount.text = (gameManager.player.SookGarlic).ToString();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Item")
        {

            //�������
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
            //������ �������
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Treasure")
        {

            gameManager.stagePoint += 500;
            gameManager.playerData.items.SetValue(true,int.Parse(collision.gameObject.name)); //DB�� ���� ���� true�� �����
            TreasurePopup.TreasurePopupNum = int.Parse(collision.gameObject.name);
            TreasurePopup.TreasurePopupBool = true;
            TreasurePopupObject.SetActive(true);
            gameManager.UIPoint.text = (gameManager.totalPoint + gameManager.stagePoint).ToString();
            collision.gameObject.SetActive(false);

        }
        if (collision.gameObject.tag == "Finish") //��� ������
        {
            //���� ��������
            gameManager.NextStage(); //�������������� �̵�, 
            if (gameManager.playerData.MaxStageLevel < gameManager.stageIndex) //DB�� ����� �ְ� ������������ ���罺�������� ũ��
            {
                gameManager.playerData.MaxStageLevel = gameManager.stageIndex; //����
            }
            
            gameManager.playerData.score = gameManager.totalPoint; //���� ����
            gameManager.SavePlayerDataToJson(); //DB�� ����
        }
        if (collision.gameObject.tag == "BossFinish") //��� ������
        {
            //���� ��������
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
        //�� ����
        Enemy_Move enemyMove = enemy.GetComponent<Enemy_Move>();
        enemyMove.OnDamaged();
        gameManager.UIPoint.text = (gameManager.totalPoint + gameManager.stagePoint).ToString();
    }
    void OnDamaged(Vector2 targetPos)
    {
        //ü�°���
        if (gameObject.layer == 10)
        {
            gameManager.HealthDown();


            //���̾� ����
            gameObject.layer = 11;
            //������
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
            //�˹�
            int dirc = transform.position.x - targetPos.x >= 0 ? 1 : -1;
            rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
            //�ִϸ��̼�
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
        //�� ����
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.4f);
        //y�� ����
        spriteRenderer.flipY = true;
        //�ݶ��̴� ��Ȱ��ȭ
        capsuleCollider.enabled = false;
        //�״� ���� ȿ��
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

    }
    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;

    }
    public void Respawn()  //3�� �װ� ��Ȱ�Ҷ� ��ġ ����ġ. ���� ����ġ
    {
        spriteRenderer.flipY = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        capsuleCollider.enabled = true;
    }
    public void avatarChange()
    {
        if (gameManager.playerData.avatar == 0) //��
        {
            spriteRenderer.sprite = Bear;
            anim.runtimeAnimatorController = Bear_animator;
        }
        else //���
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
