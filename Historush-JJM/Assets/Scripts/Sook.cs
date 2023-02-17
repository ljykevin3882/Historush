using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,-40) * Time.deltaTime); // 휘두르기

    }

}
