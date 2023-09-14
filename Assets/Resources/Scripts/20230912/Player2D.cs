using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    public delegate void CustomEvent();
    public delegate bool BoolCustomEvent();
    public delegate void ProjCustomEvent(Vector3 position, Quaternion rotation);
    public delegate void IntCustomEvent(int life);
    public CustomEvent nextState;
    public IntCustomEvent updateLife;
    public BoolCustomEvent gamePlaying;
    public ProjCustomEvent generateProj;

    public int life = 5;
    private Rigidbody2D rigidBody;
    public float maxSpeed = 10f;
    new SpriteRenderer renderer;

    float hitFrameTime = 2.0f;
    float leftHitFrameTime = 0f;

    float timer = 2f;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (gamePlaying.Invoke() == true)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Flip_2D(x);
            Move_2(x, y);
            Shoot();
        }
    }

    IEnumerator BlinkObject()
    {
        leftHitFrameTime = hitFrameTime;
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            leftHitFrameTime -= 0.1f;

            if(renderer.enabled == true)
                renderer.enabled = false;
            else
                renderer.enabled = true;

            if (leftHitFrameTime <= 0f)
                yield break;
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
            if (timer > 1f)
            {
                generateProj(this.transform.position, this.transform.rotation);
                timer = 0f;
            }
        }
        timer += Time.deltaTime;
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
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(BlinkObject());
            life--;
            updateLife(life);
            Destroy(collision.gameObject);
        }

        if (life <= 0)
        {
            nextState();
        }
    }
}
