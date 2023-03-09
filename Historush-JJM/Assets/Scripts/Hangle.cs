using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hangle : MonoBehaviour
{

    public AudioClip clip; // 오디오 클립
    private AudioSource source; // AudioSource 컴포넌트
    void OnTriggerEnter2D(Collider2D collision) // 플레이어와 닿으면 삭제
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clip); // 효과음 재생
        Destroy(gameObject, 10);
    }
    void Update() {
        transform.eulerAngles += new Vector3(0,0,0.4f);
    }

}