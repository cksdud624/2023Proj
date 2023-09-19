using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player3D : MonoBehaviour
{
    public event Func<bool> IsPlaying;
    public event Action<GameObject> DestroyItem;

    public float runSpeed = 10.0f;
    public float rotationSpeed = 0f;
    public float cameraRotationSpeed = 2f;

    CharacterController pcController;
    Animator animator;
    Vector3 direction;
    Vector3 mousePos;
    //NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying() == true)
        {
            CharacterControl_Slerp();
            RotateCamera();
        }
        //NavMesh_Control();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            float posX = Input.GetAxis("Mouse X");
            this.transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                transform.eulerAngles.y + posX * cameraRotationSpeed, transform.eulerAngles.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cube")
            DestroyItem(other.gameObject);
    }

    /*
    void NavMesh_Control()
    {
        if(Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }
    }
    */
    /*
    private void FixedUpdate()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
    */



    void CharacterControl_Slerp()
    {
        //direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", pcController.velocity.magnitude);
        pcController.Move(direction * runSpeed * Time.deltaTime + Physics.gravity * Time.deltaTime);
    }
}
