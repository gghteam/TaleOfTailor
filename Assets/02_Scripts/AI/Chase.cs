using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : FsmState
{
    [SerializeField, Header("�߰� �ӵ�")]
    float chaseSpeed;
<<<<<<< HEAD

    [SerializeField, Header("���� �Ÿ�")]
=======
    
    [SerializeField, Header("���� �Ÿ�")]
>>>>>>> origin/kdh
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
<<<<<<< HEAD
        agent.destination = lastKnownLoc = target.position;
=======
        //��� �ؼ� �Ÿ� üũ
       FollowTarget();
>>>>>>> origin/kdh
    }

    public override void OnStateLeave()
    {
<<<<<<< HEAD
        agent.ResetPath();
    }

    public Vector3 GetLastKnownPlayerLocation()
=======
        float distance = GetDistance();

        // ���� �Ÿ� �ȿ� ���Գ� üũ
        if (distance <= contactDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed*Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    // �Ÿ� ���ϱ�
    protected virtual float GetDistance()
>>>>>>> origin/kdh
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
