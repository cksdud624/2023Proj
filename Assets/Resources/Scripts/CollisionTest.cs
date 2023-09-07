using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollisionTest : MonoBehaviour
{
    float speedMove = 10.0f;
    float speedRotate = 100.0f;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        print("입장" + hitObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        print("퇴장" + hitObject.name);
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        print("유지" + hitObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        /*
        float rot = Input.GetAxis("Horizontal");
        float mov = Input.GetAxis("Vertical");

        rot = rot * speedRotate * Time.deltaTime;
        mov = mov * speedMove * Time.deltaTime;

        this.gameObject.transform.Rotate(Vector3.up * rot);
        this.gameObject.transform.Translate(Vector3.forward * mov);
        */
    }

    private void FixedUpdate()
    {
        float rot = Input.GetAxis("Horizontal");
        float mov = Input.GetAxis("Vertical");

        Quaternion deltaRot = Quaternion.Euler(new Vector3(0, rot, 0) * speedRotate * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRot);

        Vector3 move = transform.forward * mov;
        Vector3 newPos = rb.position + move * speedMove * Time.deltaTime;
        rb.MovePosition(newPos);
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("입장" + hitObject.name);
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("대기" + hitObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;
        print("퇴장" + hitObject.name);
    }
}
