using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject bomb_effect;
    public AudioClip clip; // 오디오 클립
    private AudioSource source; // AudioSource 컴포넌트

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0.1f;
        source.PlayOneShot(clip); // 효과음 재생
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    void FixedUpdate() {
        // 날아가는 방향 바라보기
        float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D other) // 닿으면 부착
    {
        GameObject otherObject = other.gameObject;
        if ((otherObject.CompareTag("Platform") || otherObject.CompareTag("Player")))
        {
            Destroy(gameObject);

            // scale 변경 방지용 쿠션 parent
            GameObject sharedParent = new GameObject("Father");
            sharedParent.transform.position = otherObject.transform.position;
            sharedParent.transform.rotation = otherObject.transform.rotation;
            sharedParent.transform.SetParent(otherObject.gameObject.transform);

            // 이펙트 생성
            GameObject _bomb_effect = Instantiate(bomb_effect, transform.position, transform.rotation);
            _bomb_effect.transform.SetParent(sharedParent.transform, true);
            //2초 후 소멸
            Destroy(_bomb_effect, 0.5f);
        }
    }    



}
