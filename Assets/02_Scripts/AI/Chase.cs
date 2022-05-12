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

}
