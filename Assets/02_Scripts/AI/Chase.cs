using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField, Header("�߰� �ӵ�")]
    float chaseSpeed;
    
    [SerializeField, Header("���� �Ÿ�")]
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
        //��� �ؼ� �Ÿ� üũ
       FollowTarget();
    }

    void FollowTarget() 
    {
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
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

}
