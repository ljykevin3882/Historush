using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hangle : MonoBehaviour
{

    public AudioClip clip; // ����� Ŭ��
    private AudioSource source; // AudioSource ������Ʈ
    void OnTriggerEnter2D(Collider2D collision) // �÷��̾�� ������ ����
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clip); // ȿ���� ���
        Destroy(gameObject, 10);
    }
    void Update() {
        transform.eulerAngles += new Vector3(0,0,0.4f);
    }

}