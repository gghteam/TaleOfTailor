using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : Character
{
    [SerializeField, Header("�߰� �ӵ�")]
    float chaseSpeed;

    [SerializeField, Header("���� �Ÿ�")]
    float contactDistance;

    [SerializeField, Header("�Ѿư��� Ÿ��(�÷��̾�)")]
    Transform target;

    private NavMeshAgent agent;
    private Vector3 lastKnownLoc;

    void Awake()
    {
        //ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isFollow())
        {
            agent.SetDestination(target.position);
            //ani.SetBool("chasing", true);
        }
        else
        {
            //ani.SetBool("chasing", false);
        }
    }
    bool isFollow()
    {
        return Vector3.Distance(transform.position, target.position) < contactDistance;
    }

    protected Vector3 GetLastKnownPlayerLocation()
    {
        return lastKnownLoc;
    }

}
