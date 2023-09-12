using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public float speed = 10.0f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GameCenter.Instance().updateScore(100);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
