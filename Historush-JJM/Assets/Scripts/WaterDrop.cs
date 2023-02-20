using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) // 플레이어, 벽과 닿으면 파괴
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Platform")
        {
            rb.velocity = new Vector2(0,0); // 멈추기
            anim.SetBool("isPop", true); // 애니메이션 설정
            Destroy(gameObject, 0.5f);
        }
    }
}
