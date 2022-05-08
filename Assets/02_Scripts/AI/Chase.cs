using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField, Header("추격 속도")]
    float chaseSpeed;
    
    [SerializeField, Header("근접 거리")]
    float contactDistance;

    Rigidbody rb;

    [SerializeField]
    GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //계속 해서 거리 체크
       FollowTarget();
    }

    void FollowTarget() 
    {
        float distance = GetDistance();

        // 일정 거리 안에 들어왔나 체크
        if (distance <= contactDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed*Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    // 거리 구하기
    float GetDistance()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

}
