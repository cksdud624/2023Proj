using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public event Func<bool> IsPlaying;

    Animation spartanKing;
    public GameObject objSword = null;
    public float runSpeed = 1f;
    CharacterController pcControl;

    bool Dead = false;
    bool Attack = false;
    bool Complete = false;

    Ray ray;
    RaycastHit rayHit;

    // Start is called before the first frame update
    void Start()
    {
        objSword.SetActive(false);
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        pcControl = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying?.Invoke() == true)
        {
            if (Dead == false)
            {
                ray.origin = this.transform.position + new Vector3(0, 0.1f, 0);
                ray.direction = new Vector3(1, 0, 0);

                if (Physics.Raycast(ray, out rayHit, 0.2f))
                {
                    if (rayHit.collider.gameObject.tag == "Cube")
                    {
                        Attack = true;
                    }
                }


                if (Attack == false)
                {
                    Vector3 direction = new Vector3(1, 0, 0);

                    if (spartanKing["run"].enabled == false)
                    {
                        spartanKing.wrapMode = WrapMode.Loop;
                        spartanKing.CrossFade("run", 0.3f);
                    }

                    pcControl.Move(direction * runSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
                }
                else if (Attack == true)
                {
                    if(Complete == true)
                    {
                        if(spartanKing["attack"].enabled == false)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                    else
                    {
                        if (spartanKing["attack"].enabled == false)
                        {
                            spartanKing.wrapMode = WrapMode.Once;
                            spartanKing.CrossFade("attack", 0.3f);
                            objSword.SetActive(true);
                            Complete = true;
                        }
                    }
                }
            }
            else
            {
                if (spartanKing["diehard"].enabled == false)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            Destroy (this.gameObject);
        }
        /*
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        objSword.SetActive(false);
        */
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(ray.origin, ray.direction * 0.2f, Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.tag == "Sword")
        {
            spartanKing.wrapMode = WrapMode.Once;
            spartanKing.CrossFade("diehard", 0.3f);
            Dead = true;
            objSword.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
