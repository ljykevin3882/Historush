using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletControl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) // 플레이어와 닿으면 삭제
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }
    // Update is called once per frame
    void Update()
    {

    }
}