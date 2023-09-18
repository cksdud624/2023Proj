using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float runSpeed = 10.0f;
    public float rotationSpeed = 360.0f;

    public GameObject Sword;
    public GameObject Arrow;
    public GameObject Bow;

    float subRunSpeed;

    CharacterController pcController;
    Animator animator;
    Vector3 direction;
    void Start()
    {
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        subRunSpeed = runSpeed;
    }
    void Update()
    {
        CharacterControl_Slerp();
    }

    void CharacterControl_Slerp()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            subRunSpeed = runSpeed / 2;
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            subRunSpeed = runSpeed;
        }

        if(direction.sqrMagnitude > 0.01f)
        {
            Vector3 forward = Vector3.Slerp(transform.forward, direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward,direction));

            transform.LookAt(transform.position + forward);
        }


        animator.SetFloat("Speed", pcController.velocity.magnitude);
        pcController.Move(direction * subRunSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Sword");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("Bow");
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Sword"))
        {
            Sword.SetActive(true);
        }
        else
        {
            Sword.SetActive(false);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Bow"))
        {
            Bow.SetActive(true);

            if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.7f)
            {
                Arrow.SetActive(false);
            }
            else
            {
                Arrow.SetActive(true);
            }
        }
        else
        {
            Bow.SetActive(false);
            Arrow.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Dead");
        }
    }
    void LateUpdate()
    {
        
    }
    void FixedUpdate()
    {
        
    }
}
