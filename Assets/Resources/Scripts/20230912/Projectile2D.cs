using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    public delegate bool BoolCustomEvent();
    public BoolCustomEvent gamePlaying;

    public float speed = 10.0f;
    void Update()
    {
        if (gamePlaying.Invoke() == true)
        {
            Move_01();
        }
    }
    void Move_01()
    {
        this.transform.position = new Vector2(this.transform.position.x + speed * Time.deltaTime,
            this.transform.position.y);
    }
}
