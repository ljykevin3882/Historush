using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoving : MonoBehaviour
{
    // Start is called before the first frame update
   // public float startY;
    public float distance = 0.3f;
    public float speed=0.7f;
    public bool up;
    Vector3 uptarget,downtarget,velo;

    Rigidbody2D rigid;


    void Start()
    {

        uptarget = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
        downtarget = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);
        velo = Vector3.zero;
        rigid = GetComponent<Rigidbody2D>();

        up = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (up == true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, uptarget, ref velo, speed);
            if (transform.position.y > uptarget.y-0.1f) //위 타겟 도달하면
            {
                up = false;
            }
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, downtarget, ref velo, speed);
            if (transform.position.y < downtarget.y+0.1f) //위 타겟 도달하면
            {
                up = true;
            }
        }
    }
}
