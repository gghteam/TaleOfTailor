using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : FsmState
{
    [SerializeField, Header("ï¿½ß°ï¿½ ï¿½Óµï¿½")]
    float chaseSpeed;
<<<<<<< HEAD

    [SerializeField, Header("±ÙÁ¢ °Å¸®")]
=======
    
    [SerializeField, Header("ï¿½ï¿½ï¿½ï¿½ ï¿½Å¸ï¿½")]
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
        //ï¿½ï¿½ï¿½ ï¿½Ø¼ï¿½ ï¿½Å¸ï¿½ Ã¼Å©
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

        // ï¿½ï¿½ï¿½ï¿½ ï¿½Å¸ï¿½ ï¿½È¿ï¿½ ï¿½ï¿½ï¿½Ô³ï¿½ Ã¼Å©
        if (distance <= contactDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed*Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    // ï¿½Å¸ï¿½ ï¿½ï¿½ï¿½Ï±ï¿½
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
    //    //°è¼Ó ÇØ¼­ °Å¸® Ã¼Å©
    //    FollowTarget();
    //}

    //void FollowTarget()
    //{
    //    float distance = agent.velocity.sqrMagnitude;

    //    // ÀÏÁ¤ °Å¸® ¾È¿¡ µé¾î¿Ô³ª Ã¼Å©
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
