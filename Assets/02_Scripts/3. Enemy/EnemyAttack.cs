using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // 이렇게 변수로 할까 SO로 할까?
    [SerializeField]
    private float attackDamage = 1f;
    [SerializeField]
    private float attackDelay = 0.5f;

    [SerializeField]
    private Collider[] hitColloders;
    public int attackLlayer = 1 << 2;

    private bool isAttack = false;
    private bool isPlayerDamage = false;

    private float timer = 0f;

    RaycastHit hit;

    private Animator animator;
    private readonly int attack = Animator.StringToHash("attack");
    private readonly int parrying = Animator.StringToHash("parrying");

    void Start()
    {
        animator = GetComponent<Animator>();

        timer = attackDelay;
        ColliderEnabledChanged(0);
    }

    void OnEnable()
    {
        timer = attackDelay;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //if (isAttack)
        //{
        //    // 방법 2
        //    Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), transform.forward, out hit, 1, attackLlayer);

        //    if (hit.collider != null)
        //    {
        //        if (hit.collider.CompareTag("Player"))
        //        {
        //            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        //            {
        //                // 플레이어 데미지 주기
        //                if (!isPlayerDamage)
        //                {
        //                    Debug.Log("Player Damage");
        //                    isPlayerDamage = true;
        //                }
        //            }
        //        }
        //    }
        //}

        Debug.DrawRay(transform.position + new Vector3(0, 1.5f, 0), transform.forward, Color.green);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (timer >= attackDelay)
            {
                if (!isAttack)
                {
                    Attack();
                    timer = 0;
                }
            }
        }
    }

    /// <summary>
    /// 공격당했을 때 함수
    /// </summary>
    private void ParryingAction()
    {
        // 패링 당했을 때 행동
        Debug.Log("쫌 치네ㅋ");
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    private void Attack()
    {
        AttackChanged(1);
        animator.SetTrigger(attack);
        /*
        방법 1. 적 손에 Collider를 달고 공격 시 Collider의 엑티브를 키는 방식
         그 콜라이더에 닿으면 Damage주기
        방법 2. 공격 중에는 적에 앞 방향으로 레이케스트를 쏜다
         그 레이에 닿으면 Damage주기
         */

        // 에니메이션 마지막에 Event달기
        //  AttackChanged, ColliderEnabledChanged를 Event로 등록함

        // 방법 1
        //ColliderEnabledChanged(1);
    }

    /// <summary>
    /// isAttack변수(현재 공격중인지?)를 변경시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void AttackChanged(int value)
    {
        isAttack = value == 0 ? false : true;
    }

    /// <summary>
    /// isPlayerDamage변수(플레이어가 데미지를 받는 중인지?)를 변경시키는 함수. 도트뎀을 막는 용도
    /// </summary>
    /// <param name="value"></param>
    private void PlayerDamageChanged(int value)
    {
        isPlayerDamage = value == 0 ? false : true;
    }

    /// <summary>
    /// 공격 콜라이더의 Enabled를 변경시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void ColliderEnabledChanged(int value)
    {
        foreach (Collider collider in hitColloders)
        {
            collider.enabled = value == 0 ? false : true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!isPlayerDamage)
            {
                // 플레이어 데미지 주기
                Debug.Log("Player Damage");
                PlayerDamageChanged(1);
            }
        }
    }
}
