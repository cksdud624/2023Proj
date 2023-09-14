using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    public delegate void IntCustomEvent(int score);
    public delegate void CustomEvent();
    public delegate bool BoolCustomEvent();
    public IntCustomEvent scoreCustomEvent;
    public CustomEvent customEvent;
    public BoolCustomEvent gamePlaying;

    int flip = 0;

    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying.Invoke() == true)
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
            scoreCustomEvent(100);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
