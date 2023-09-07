using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject TopBody;
    public GameObject BottomBody;
    public GameObject Barrel;
    public GameObject Front;
    public GameObject[] Tire;

    public float turnSpeed = 50f;
    public float moveSpeed = 50f;
    public float amount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveVer = Input.GetAxis("Vertical");
        float moveHor = Input.GetAxis("Horizontal");
        float turn = Input.GetAxis("Turn");
        float aiming = Input.GetAxis("Aiming");
        MoveTank(moveVer, moveHor);
        RotateBottomBody(moveVer, moveHor);
        RotateTopBody(turn, aiming);
    }
    void MoveTank(float moveVer, float moveHor)
    {
        Vector3 temp = new Vector3(0, 0, 1);
        transform.Translate(temp * Time.deltaTime * moveSpeed * moveVer);
    }
    void RotateBottomBody(float moveVer, float moveHor)
    {
        for (int i = 0; i < Tire.Length; i++)
        {
            float rot = moveSpeed * Time.deltaTime;
            Tire[i].transform.Rotate(Vector3.down * moveVer);
        }

        //BottomBody.transform.rotation = Quaternion.Euler(BottomBody.transform.eulerAngles.x,
            //BottomBody.transform.eulerAngles.y + moveHor / 6, BottomBody.transform.eulerAngles.z);

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
            transform.eulerAngles.y + moveHor / 6, transform.eulerAngles.z);
    }
    void RotateTopBody(float turn, float aiming)
    {
        GameObject Standard = TopBody.transform.GetChild(0).gameObject;
        TopBody.transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);
        Barrel.transform.RotateAround(Standard.transform.position, Vector3.up, turn * turnSpeed * Time.deltaTime);
        if (amount + aiming * turnSpeed * Time.deltaTime >= -10 && amount + aiming * turnSpeed * Time.deltaTime <= 30)
        {
            Barrel.transform.RotateAround(Standard.transform.position, Vector3.left, aiming * turnSpeed * Time.deltaTime);
            amount += aiming * turnSpeed * Time.deltaTime;
        }
    }
}
