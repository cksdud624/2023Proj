using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using static UnityEngine.GraphicsBuffer;

public class AICar : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float speed = 360.0f;
    public float[] amount;
    public GameObject[] Tire;
    public float turnSpeed = 0.2f;

    float vecRange = 0f;

    [Range(0, 50)]
    public float distance = 10.0f;
    public float tireback = 0.2f;
    private Ray[] ray;
    private Ray[] balancer;
    private RaycastHit[] rayHits;
    private RaycastHit rayHit;

    private float balanceAngle = 0.1f;

    Vector3 temp = new Vector3(0, 0, 0.3f);

    int end = 0;

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray[2];
        balancer = new Ray[2];
        tireback = 0.2f;
        distance = Random.Range(10f, 10f);
        vecRange = Random.Range(0.3f, 0.4f);
        turnSpeed = 1f;
        balanceAngle = tireback + 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (end == 0)
        {
            moveSpeed = Random.Range(10f, 25f);
        }

        for (int i = 0; i < ray.Length; i++)
        {
            ray[i].origin = this.transform.position + ray[i].direction * 2.5f;
        }
        ray[0].direction = this.transform.forward - this.transform.right * vecRange;
        ray[1].direction = this.transform.forward + this.transform.right * vecRange;
        for(int i = 0; i < balancer.Length; i++)
        {
            balancer[i].origin = this.transform.position;
        }
        balancer[0].direction = this.transform.right + this.transform.forward;
        balancer[1].direction = -this.transform.right + this.transform.forward;
        float moveVer = Input.GetAxis("Vertical");
        float moveHor = Input.GetAxis("Horizontal");
        
        MoveCar();
        RotateCar();
        CorrectRotation();
    }

    private void CorrectRotation()
    {
        //ray 0 ÁÂÃø ray 1 ¿ìÃø

        float[] mags = new float[2];

        for (int i = 0; i < 2; i++)
        {
            if (Physics.Raycast(balancer[i], out rayHit, 60f))
            {
                mags[i] = (balancer[i].origin - rayHit.point).magnitude;
            }
        }
        if (mags[0] < mags[1])
        {
            if (amount[0] + balanceAngle <= 15)
            {
                amount[0] += balanceAngle;
                for (int i = 0; i < 2; i++)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                        Tire[i].transform.eulerAngles.y - balanceAngle, Tire[i].transform.eulerAngles.z);
                }
            }
        }
        else if (mags[0] > mags[1])
        {
            if (amount[0] - balanceAngle >= -15)
            {
                amount[0] -= balanceAngle;
                for (int i = 0; i < 2; i++)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                        Tire[i].transform.eulerAngles.y + balanceAngle, Tire[i].transform.eulerAngles.z);
                }
            }
        }


    }

    private void OnDrawGizmos()
    {
        if (ray != null)
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.DrawRay(ray[i].origin, ray[i].direction * distance, Color.red);
                Debug.DrawRay(balancer[i].origin, balancer[i].direction * 60f, Color.blue);//0 ¿À¸¥ÂÊ 1 ¿ÞÂÊ
            }
        }
    }



    void RotateCar()
    {
        Vector3[] vecs = new Vector3[2];
        for (int i = 0; i < Tire.Length; i++)
        {
            float rot = speed * Time.deltaTime;
            Tire[i].transform.Rotate(rot * Vector3.down);
        }
        bool[] raycheck = new bool[ray.Length];
        for (int j = 0; j < ray.Length; j++)
        {
            if (Physics.Raycast(ray[j], out rayHit, distance))
            {
                raycheck[j] = true;
                vecs[j] = ray[j].origin - rayHit.point;
            }
        }


        if (raycheck[1] == true && raycheck[0] == true)
        {
            if (vecs[1].magnitude > vecs[0].magnitude)
            {
                if (amount[0] - turnSpeed >= -90)
                {
                    amount[0] -= turnSpeed;
                    for (int i = 0; i < 2; i++)
                    {
                        Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                            Tire[i].transform.eulerAngles.y + turnSpeed, Tire[i].transform.eulerAngles.z);
                    }
                }
            }
            else
            {
                if(amount[0] + turnSpeed <= 90)
                {
                    amount[0] += turnSpeed;
                    for (int i = 0; i < 2; i++)
                    {
                        Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                            Tire[i].transform.eulerAngles.y - turnSpeed, Tire[i].transform.eulerAngles.z);
                    }
                }
            }
            Debug.Log("break");
        }


        else if (raycheck[0] == false && raycheck[1] == false)
        {

            if (amount[0] <= tireback / 2 && amount[0] >= -tireback / 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                        Tire[i].transform.eulerAngles.y + amount[0], Tire[i].transform.eulerAngles.z);
                }
                amount[0] = 0;
            }
            else if (amount[0] > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                        Tire[i].transform.eulerAngles.y + tireback, Tire[i].transform.eulerAngles.z);
                }
                amount[0] -= tireback;
            }
            else if (amount[0] < 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                        Tire[i].transform.eulerAngles.y - tireback, Tire[i].transform.eulerAngles.z);
                }
                amount[0] += tireback;
            }
        }

        else if (raycheck[1] == true)
        {
            if (amount[0] + turnSpeed <= 40)
            {
                amount[0] += turnSpeed;
                for (int i = 0; i < 2; i++)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                        Tire[i].transform.eulerAngles.y - turnSpeed, Tire[i].transform.eulerAngles.z);
                }
            }
        }
        else if (raycheck[0] == true)
        {
            if (amount[0] + turnSpeed >= -40)
            {
                amount[0] -= turnSpeed;
                for (int i = 0; i < 2; i++)
                {
                    Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                        Tire[i].transform.eulerAngles.y + turnSpeed, Tire[i].transform.eulerAngles.z);
                }
            }
        }
        
        /*
        for (int i = 0; i < amount.Length; i++)
        {
            if (amount[i] + turnSpeed <= 45 && amount[i] + turnSpeed >= -45)
            {
                Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                    Tire[i].transform.eulerAngles.y + turnSpeed, Tire[i].transform.eulerAngles.z);
                amount[i] += turnSpeed;
            }
        }
        */
        /*
        else
        {
            for (int i = 0; i < Tire.Length - 2; i++)
            {
                float tireback = 0.2f;
                if (amount[i] <= 0.1 && amount[i] >= -0.1)
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
        }
        */

        /*
        for (int i = 0; i < Tire.Length - 2; i++)
        {
            if(moveHor == 0)
            {
                float tireback = 0.2f;
                if (amount[i] <= 0.1 && amount[i] >= -0.1)
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
        */
    }

    int memorize = 0;
    private void OnTriggerEnter(Collider other)
    {
        string temp = other.gameObject.name.Substring(0, 3);

        /*
        if(temp == "Car")
        {
            amount[0] = -amount[0] * 0.5f;

            for (int i = 0; i < 2; i++)
            {
                Tire[i].transform.rotation = Quaternion.Euler(Tire[i].transform.eulerAngles.x,
                    Tire[i].transform.eulerAngles.y - amount[0] * 1.5f, Tire[i].transform.eulerAngles.z);
            }
        }
        */



        /*
        if (other.gameObject.name == "StartLine")
        {
            GameManager.Instance.ArriveStartLine(this.gameObject.name);
            string temp = GameManager.Instance.ArriveCheck(this.gameObject.name);

            if (temp == this.gameObject.name)
            {
                moveSpeed = 0;
                GameManager.Instance.ArriveEnd(this.gameObject.name);
                //Debug.Log(temp);
                end = 1;
            }
        }
        */
    }

    void MoveCar()
    {
        GameObject front = transform.GetChild(5).gameObject;
        Vector3 temp = new Vector3(0, 0, 1);
        transform.Translate(temp * Time.deltaTime * moveSpeed);
        this.transform.RotateAround(front.transform.position, Vector3.down, Time.deltaTime * moveSpeed * amount[0] / 6.5f);
        /*
        if (moveVer > 0)
        {
            this.transform.RotateAround(front.transform.position, Vector3.up, Time.deltaTime * moveSpeed * amount[0] / 2.5f);
        }
        else if(moveVer < 0)
        {
            this.transform.RotateAround(front.transform.position, Vector3.up, Time.deltaTime * moveSpeed * -amount[0] / 2.5f);
        }
        */
    }
}
