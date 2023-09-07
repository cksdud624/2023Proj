using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Car : MonoBehaviour
{
    public float moveSpeed = 15.0f;
    public float speed = 20.0f;
    public float turnSpeed = 2f;
    public float tireback = 0.3f;
    public float[] amount;
    public GameObject[] Tire;

    Vector3 StartPoint;
    // Start is called before the first frame update

    private void Start()
    {
        StartPoint = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        float moveVer = Input.GetAxis("Vertical");
        float moveHor = Input.GetAxis("Horizontal");
        MoveCar(moveVer, moveHor);
        RotateTire(moveVer, moveHor);
    }

    void RotateTire(float moveVer, float moveHor)
    {
        for (int i = 0; i < Tire.Length; i++)
        {
            float rot = speed * Time.deltaTime;
            Tire[i].transform.Rotate(rot * Vector3.down * moveVer);

        }
        for (int i = 0; i < Tire.Length - 2; i++)
        {
            if(moveHor == 0)
            {
                float tireback = 0.2f;
                if (amount[i] <= tireback / 2 && amount[i] >= -tireback /2)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
              Tire[i].transform.eulerAngles.y - amount[i], Tire[i].transform.eulerAngles.z);
                    amount[i] = 0;
                }
                else if (amount[i] > 0)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                  Tire[i].transform.eulerAngles.y - tireback, Tire[i].transform.eulerAngles.z);
                    amount[i] -= tireback;
                }
                else if (amount[i] < 0)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                  Tire[i].transform.eulerAngles.y + tireback, Tire[i].transform.eulerAngles.z);
                    amount[i] += tireback;
                }
            }
            else if (amount[i] + moveHor <= 45 && amount[i] + moveHor >= -45)
            {
                Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                Tire[i].transform.eulerAngles.y + moveHor, Tire[i].transform.eulerAngles.z);
                amount[i] += moveHor;
            }
        }
    }
    void MoveCar(float moveVer, float moveHor)
    {
        GameObject front = transform.GetChild(5).gameObject;
        Vector3 temp = new Vector3(0, 0, 1);



        transform.Translate(temp * Time.deltaTime * moveSpeed * moveVer);
        if (moveVer > 0.01f)
        {
            this.transform.RotateAround(front.transform.position, Vector3.up, Time.deltaTime * amount[0] * turnSpeed);
        }
        else if(moveVer < -0.01f)
        {
            this.transform.RotateAround(front.transform.position, Vector3.up, Time.deltaTime * -amount[0] * turnSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Length >= 9)
        {
            string outofmapcheck = other.gameObject.name.Substring(0, 9);

            if (outofmapcheck == "OutofMaps")
            {
                this.transform.position = StartPoint;
            }
        }
    }


}
