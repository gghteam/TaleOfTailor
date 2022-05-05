using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float attackDamage = 1f;
    [SerializeField]
    private float attackDelay = 0.5f;

    [SerializeField]
    private Collider[] hitColloders;
    public int attackLlayer = 1 << 6;

    private bool isAttack = false;

    private float timer = 0f;

    RaycastHit hit;

    void Start()
    {
        timer = attackDelay;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isAttack)
            Physics.Raycast(transform.position, transform.forward * 10, out hit, attackLlayer);

        if(timer >= attackDelay)
        {
            if (!isAttack)
            {
                Attack();
                timer = 0;
            }
        }
    }

    private void Attack()
    {
        AttackChanged(true);
        /*
        방법 1. 적 손에 Collider를 달고 공격 시 Collider의 엑티브를 키는 방식
         그 콜라이더에 닿으면 Damage주기
        방법 2. 공격 중에는 적에 앞 방향으로 레이케스트를 쏜다
         그 레이에 닿으면 Damage주기
         */

        // 에니메이션 마지막에 Event달기
        //  AttackChanged, ColliderEnabledChanged를 Event로 등록

        // 방법 1
        ColliderEnabledChanged(true);

        // 방법 2
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                // 플레이어 데미지 주기
            }
        }
    }

    public void AttackChanged(bool value)
    {
        isAttack = value;
    }

    public void ColliderEnabledChanged(bool value)
    {
        foreach (Collider collider in hitColloders)
        {
            collider.enabled = value;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // 플레이어 데미지 주기
        }
    }
}
