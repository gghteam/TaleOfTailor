using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : FsmState
{
    //[SerializeField, Header("�߰� �ӵ�")]
    //float chaseSpeed;

    [SerializeField, Header("���� �Ÿ�")]
    float contactDistance;

    [SerializeField, Header("�Ѿư��� Ÿ��(�÷��̾�)")]
    Transform target;

    private Vector3 lastKnownLoc;
    private NavMeshAgent agent;

    void Awake()
    {
        //ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isFollow())
        {
        agent.destination = lastKnownLoc = target.position;
            //ani.SetBool("chasing", true);
        }
        //else
        {
            //ani.SetBool("chasing", false);
        }
    }
    bool isFollow()
    {
        return Vector3.Distance(transform.position, target.position) < contactDistance;
    }

    public override void OnStateEnter()
    {
        Debug.Log("���Ӥ�����");
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
