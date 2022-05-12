using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : FsmState
{
    [SerializeField, Header("추격 속도")]
    float chaseSpeed;

    [SerializeField, Header("근접 거리")]
    float contactDistance;


    [SerializeField]
    Transform target;

    private NavMeshAgent agent;
    private Vector3 lastKnownLoc;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = lastKnownLoc = target.position;
    }

    public override void OnStateLeave()
    {
        agent.ResetPath();
    }

    public Vector3 GetLastKnownPlayerLocation()
    {
        return lastKnownLoc;
    }

    //NavMeshAgent agent;

    //private void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //}

    //private void Update()
    //{
    //    //계속 해서 거리 체크
    //    FollowTarget();
    //}

    //void FollowTarget()
    //{
    //    float distance = agent.velocity.sqrMagnitude;

    //    // 일정 거리 안에 들어왔나 체크
    //    if (distance > contactDistance && distance>0.1f)
    //    {
    //        agent.velocity = Vector3.zero;
    //    }
    //    else
    //    {
    //        agent.SetDestination(target.position);
    //    }
    //}

}
