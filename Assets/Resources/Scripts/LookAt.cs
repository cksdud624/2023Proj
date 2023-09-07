using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //LookAt_1();
        //LookAt_2();
        LookAt_3();
    }

    void LookAt_3()
    {
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        transform.rotation = Quaternion.LookRotation(dirToTarget, Vector3.up);
    }
    void LookAt_2()
    {
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        transform.LookAt(dirToTarget.normalized);
    }
    void LookAt_1()
    {
        Vector3 dirToTarget = target.transform.position - this.transform.position;
        this.transform.forward = dirToTarget.normalized;
    }
}
