using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = new Vector3(0.0f, 0.5f, 0.0f);벡터 월드좌표기준
        //this.transform.Translate(new Vector3(0.0f, 5.5f, 0.0f));// 로컬기준이동 더 이동하는 느낌이듬

        /*
        if(oldTime - newTime < 30) return;

        gap = newTime - oldTime;
        oldTime = newTime - gap;
         */
    }

    // Update is called once per frame
    void Update()
    {
        //Move_1();
        Move_2();
    }

    void Move_Control()
    {
        float move = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * move);
    }
    void Move_2()
    {
        float moveDelta = this.moveSpeed * Time.deltaTime;
        this.transform.Translate(Vector3.forward * moveDelta);
    }

    void Move_1()
    {
        float moveDelta = this.moveSpeed * Time.deltaTime;
        Vector3 pos = this.transform.position;
        pos.z += moveDelta;
        this.transform.position = pos;
    }
}
