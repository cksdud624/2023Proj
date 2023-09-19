using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3D : MonoBehaviour
{
    public event Func<bool> IsPlaying;
    public event Action<GameObject> DestroyEnemyItem;

    public GameObject target;
    NavMeshAgent agent = null;

    CharacterController pcController;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.destination = target.transform.position;
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    private void OnDrawGizmos()
    {
        if (agent == null)
            return;
        NavMeshPath path = agent.path;

        Gizmos.color = Color.blue;
        foreach(Vector3 corner in path.corners)
        {
            Gizmos.DrawSphere(corner, 0.4f);
        }

        Gizmos.color = Color.red;
        Vector3 pos = transform.position;

        foreach(Vector3 corner in path.corners)
        {
            Gizmos.DrawLine(pos, corner);
            pos = corner;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cube")
        {
            DestroyEnemyItem(other.gameObject);
        }
    }
}
