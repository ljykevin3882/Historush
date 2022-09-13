using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    public GameObject Enemy;
    public int nextMove;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 5); //5�� ������

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayhit.collider == null)
        {
            if (spriteRenderer.flipY != true) { 
                Turn();
        }
        }
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2); //������ �ִ밪�� ���� �ȵ�
        anim.SetInteger("WalkSpeed",nextMove);
        Invoke("Think", 5); //5�� ������
        if(nextMove!=0)
            spriteRenderer.flipX = nextMove==1;
    }
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }
    public void OnDamaged()
    {
        //�� ����
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //y�� ����
        spriteRenderer.flipY = true;
        //�ݶ��̴� ��Ȱ��ȭ
        capsuleCollider.enabled = false;
        //�״� ���� ȿ��
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //1�ʵ� ����
        Invoke("Disappear", 1);

    }
    public void Disappear()
    {
        Enemy.SetActive(false);
    }
}
