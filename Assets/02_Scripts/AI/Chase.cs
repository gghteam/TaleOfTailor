using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : FsmState
{
    [SerializeField, Header("�߰� �ӵ�")]
    float chaseSpeed;

    [SerializeField, Header("���� �Ÿ�")]
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
    //    //��� �ؼ� �Ÿ� üũ
    //    FollowTarget();
    //}

    //void FollowTarget()
    //{
    //    float distance = agent.velocity.sqrMagnitude;

    //    // ���� �Ÿ� �ȿ� ���Գ� üũ
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
