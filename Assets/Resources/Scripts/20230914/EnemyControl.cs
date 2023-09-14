using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Animation spartanKing;
    public GameObject objSword = null;
    public float runSpeed = 1f;
    CharacterController pcControl;
    // Start is called before the first frame update
    void Start()
    {
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        pcControl = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(1, 0, 0);

        if (spartanKing["run"].enabled == false)
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("run", 0.3f);
        }

        pcControl.Move(direction * runSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
        /*
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        objSword.SetActive(false);
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if(other.tag == "Sword")
        {
            spartanKing.CrossFade("diehard", 0.3f);
        }
        */
    }
}
