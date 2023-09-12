using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud2D : MonoBehaviour
{
    float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameCenter.Instance().GetPlay() == true)
        {
            Move_01();
        }
    }

    void Move_01()
    {
        this.transform.position = new Vector2(this.transform.position.x - speed * Time.deltaTime,
            this.transform.position.y);
    }
}
