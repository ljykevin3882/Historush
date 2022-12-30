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

    private void Update()
    {
        //Move by control
        float h = Input.GetAxisRaw("Horizontal");
        h += fixjoy.Horizontal;
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
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

        //PC �����̵�-Ű�Է�
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
                    print("collision�۵�");
                }
            }
            else//spike �϶�
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
            //�������
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
                gameManager.stagePoint += 50;
            else if (isSilver)
                gameManager.stagePoint += 100;
            else if (isGold)
                gameManager.stagePoint += 300;


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
        //�� ����
        Enemy_Move enemyMove = enemy.GetComponent<Enemy_Move>();
        enemyMove.OnDamaged();
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
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
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
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    public void OnDie()
    {
        //�� ����
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
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
        spriteRenderer.color = new Color(1, 1, 1, 1);
        capsuleCollider.enabled = true;
    }
   

}
