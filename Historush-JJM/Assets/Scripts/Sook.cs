using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sook : MonoBehaviour
{

    public AudioClip clip; // 오디오 클립
    private AudioSource source; // AudioSource 컴포넌트
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 1f;
        source.PlayOneShot(clip); // 효과음 재생
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,-30) * Time.deltaTime); // 휘두르기

    }

}
