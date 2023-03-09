using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public AudioClip clip; // 오디오 클립
    private AudioSource source; // AudioSource 컴포넌트
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clip); // 효과음 재생
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
        }
    }
    public void Delete()
    {
        Destroy(gameObject);
    }

    

}
