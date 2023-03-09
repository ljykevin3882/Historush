using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlic : MonoBehaviour
{
    private bool isSound = false;
    public AudioClip clip; // 오디오 클립
    private AudioSource source; // AudioSource 컴포넌트
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Platform") {
            if (isSound == false) {
                source = GetComponent<AudioSource>();
                source.PlayOneShot(clip); // 효과음 재생
                isSound = true;
            }
        }
    }
}
