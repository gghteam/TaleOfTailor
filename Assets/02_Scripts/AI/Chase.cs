using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField, Header("추격 속도")]
    float chaseSpeed;

    Vector3 enemyPos;
    Vector3 playerPos;
    private void Update()
    {
        //계속 해서 거리 체크
        DistanceCheck();
    }

    void DistanceCheck() 
    {
        // 일정 거리 안에 들어왔나 체크
        float distance = GetDistance();

        if (distance <= 10)
        {
            // 보스 위치 - 플레이어 위치로 방향 구해서 이동시키기
            Vector3 dir = GetDirection();
            // 이동 코드
            enemyPos = Vector3.MoveTowards(enemyPos, playerPos, chaseSpeed);
        }
        else
        {
            // 인식 안됨
            return;
        }
    }

   // 플레이어와 보스 사이 거리 구하는 함수
    float GetDistance()
    {
        return Vector3.Distance(playerPos, enemyPos);
    }

    // 플레이어와 보스 방향 구하는 함수
    Vector3 GetDirection()
    {
        return playerPos - enemyPos;
    }
}
