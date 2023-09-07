using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public GameObject Top;
    public GameObject[] Wing;
    public GameObject VecPoint;

    public float power = 0.0f;
    public float turnSpeed = 10.0f;
    public float wingSpeed = 300.0f;
    public float speed = 10.0f;
    public float amountVer = 0f;
    public float amountHor = 0f;
    public float amountback = 0.2f;
    public float UpDownPower = 2f;
    public float powerBreak = 1.5f;
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
        float powerDown = Input.GetAxis("PowerDown");

        RotateWing();
        ReturnXZAngle();
        Move(moveVer, moveHor);
        MoveUp(aiming);
        Turn(turn);
        PowerDown(powerDown);
        Rotate(moveVer, moveHor);


        if (power < 10.0f)
        {
            power += Mathf.Abs(turn * Time.deltaTime);
            power += Mathf.Abs(aiming * Time.deltaTime);
            power += Mathf.Abs(moveVer * Time.deltaTime);
            power += Mathf.Abs(moveHor * Time.deltaTime);
            if (power > 10.0f)
                power = 10.0f;
        }
    }

    void ReturnXZAngle()
    {
        if(amountVer <= amountback / 2 && amountVer >= -amountback / 2)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x - amountVer,
                this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            amountVer = 0;
        }
        else if(amountVer > 0f)
        {
            amountVer -= amountback;
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x - amountback,
               this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }
        else if(amountVer < 0f)
        {
            amountVer += amountback;
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x + amountback,
               this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }

        if(amountHor <= amountback / 2 && amountHor >= -amountback / 2)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x,
           this.transform.eulerAngles.y, this.transform.eulerAngles.z + amountHor);
            amountHor = 0;
        }
        else if(amountHor > 0f)
        {
            amountHor -= amountback;
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x,
               this.transform.eulerAngles.y, this.transform.eulerAngles.z + amountback);
        }
        else if (amountHor < 0f)
        {
            amountHor += amountback;
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x,
               this.transform.eulerAngles.y, this.transform.eulerAngles.z - amountback);
        }
    }

    void Turn(float turn)
    {
        if(power > 5.0f)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x,
                this.transform.eulerAngles.y + turn * Time.deltaTime * turnSpeed, this.transform.eulerAngles.z);
        }
    }

    void PowerDown(float powerDown)
    {
        power -= powerDown * Time.deltaTime * powerBreak;

        if (power < 0.0f)
            power = 0.0f;
    }
    void RotateWing()
    {
        if (power > 0.0f)
        {
            for(int i = 0; i < Wing.Length; i++)
            {
                Wing[i].transform.RotateAround(Top.transform.position, VecPoint.transform.position - this.transform.position, wingSpeed * power * Time.deltaTime);
            }
        }
    }
    void Rotate(float moveVer , float moveHor)
    {
        if (this.transform.position.y > 1.0f)
        {
            float temp = this.transform.eulerAngles.y;
            if (amountVer + moveVer <= 30 && amountVer + moveVer >= -30)
            {
                this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x + moveVer, temp, this.transform.eulerAngles.z);
                amountVer += moveVer;
            }

            if (amountHor + moveHor <= 30 && amountHor + moveHor >= -30)
            {
                this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, temp, this.transform.eulerAngles.z - moveHor);
                amountHor += moveHor;
            }
        }

    }
    void Move(float moveVer, float moveHor)
    {
        if (power > 5.0f)
        {
            if (this.transform.position.y > 0.65f)
            {
                float temp = this.transform.position.y;
                this.transform.Translate(Vector3.forward * Time.deltaTime * speed * moveVer);
                this.transform.Translate(Vector3.right * Time.deltaTime * speed * moveHor);
                this.transform.position = new Vector3(this.transform.position.x, temp, this.transform.position.z);
            }
        }
    }
    void MoveUp(float aiming)
    {
        if (power > 5.0f)
        {
            if (this.transform.position.y + aiming * Time.deltaTime >= 0.64f)
            {
                Vector3 temp = new Vector3(this.transform.position.x, this.transform.position.y + aiming * Time.deltaTime * UpDownPower , this.transform.position.z);

                if (temp.y < 0.64f)
                    temp.y = 0.64f;


                this.transform.position = temp;
            }
        }
    }
}
