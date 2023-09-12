using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
    }

    void Death()
    {
        FlappyBirdManager.Instance.CountWall();
        Destroy(this.gameObject);
    }
}
