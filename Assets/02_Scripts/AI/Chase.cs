using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField, Header("�߰� �ӵ�")]
    float chaseSpeed;

    Vector3 enemyPos;
    Vector3 playerPos;
    private void Update()
    {
        //��� �ؼ� �Ÿ� üũ
        DistanceCheck();
    }

    void DistanceCheck() 
    {
        // ���� �Ÿ� �ȿ� ���Գ� üũ
        float distance = GetDistance();

        if (distance <= 10)
        {
            // ���� ��ġ - �÷��̾� ��ġ�� ���� ���ؼ� �̵���Ű��
            Vector3 dir = GetDirection();
            // �̵� �ڵ�
            enemyPos = Vector3.MoveTowards(enemyPos, playerPos, chaseSpeed);
        }
        else
        {
            // �ν� �ȵ�
            return;
        }
    }

   // �÷��̾�� ���� ���� �Ÿ� ���ϴ� �Լ�
    float GetDistance()
    {
        return Vector3.Distance(playerPos, enemyPos);
    }

    // �÷��̾�� ���� ���� ���ϴ� �Լ�
    Vector3 GetDirection()
    {
        return playerPos - enemyPos;
    }
}
