using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    public int life = 5;
    private Rigidbody2D rigidBody;
    public float maxSpeed = 10f;
    new SpriteRenderer renderer;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (GameCenter.Instance().GetPlay() == true)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Flip_2D(x);
            Move_2(x, y);
            Shoot();
        }
    }

    void Flip_2D(float x)
    {
        if(Mathf.Abs(x) > 0)
        {
            if (x >= 0)
                renderer.flipX = false;
            else
                renderer.flipX = true;
        }
    }
    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(projectile, this.transform.position, this.transform.rotation);
        }
    }



    void Move_2(float x, float y)
    {
        Vector3 position = rigidBody.transform.position;
        position = new Vector3(position.x + (x * maxSpeed * Time.deltaTime),
            position.y + (y * maxSpeed * Time.deltaTime), position.z);

        rigidBody.MovePosition(position);
    }
    // Update is called once per frame
    void Move_1(float x, float y)
    {
        rigidBody.AddForce(new Vector2(x * maxSpeed * Time.deltaTime,
            y * maxSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            life--;
            Destroy(collision.gameObject);
        }
    }
}
