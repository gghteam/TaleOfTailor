using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : Character
{
    [SerializeField, Header("추격 속도")]
    float chaseSpeed;

    [SerializeField, Header("근접 거리")]
    float contactDistance;

    [SerializeField, Header("쫓아가는 타겟(플레이어)")]
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
