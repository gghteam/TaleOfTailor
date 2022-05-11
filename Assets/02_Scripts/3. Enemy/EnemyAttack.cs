using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Character
{
    // 이렇게 변수로 할까 SO로 할까?
    [SerializeField]
    private float attackDamage = 1f;
    [SerializeField]
    private float attackDelay = 0.5f;

    private int attackLlayer = 1 << 6;

    private bool isAttack = false;
    public bool IsAttack { get => isAttack; }

    private bool isPlayerParrying = false;
    public bool IsPlayerParrying { get => isPlayerParrying; set => isPlayerParrying = value; }
    private bool isPlayerDamage = false;

    private int attackCount = 0;

    private float timer = 0f;

    RaycastHit hit;

    private readonly int attack = Animator.StringToHash("attack");
    private readonly int parrying = Animator.StringToHash("parrying");
    private readonly int attackCnt = Animator.StringToHash("AttackCount");

    void Start()
    {
        timer = attackDelay;
    }

    void OnEnable()
    {
        timer = attackDelay;
    }

    void Update()
    {
        timer += Time.deltaTime;

        Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), transform.forward, out hit, 1, attackLlayer);
        ani.SetInteger(attackCnt, attackCount % 2);

        //if (hit.collider != null)
        //{
        //    if (hit.collider.GetComponent<PlayerParrying>() != null)
        //    {
        //        ani.SetBool(parrying, hit.collider.GetComponent<PlayerParrying>().CheckParrying()); // 수정 예정
        //        //hit.collider.GetComponent<PlayerParrying>().IsAttack = isAttack;
        //        //hit.collider.GetComponent<PlayerParrying>().Enemy = this.gameObject;
        //    }
        //    else
        //    {
        //        ani.SetBool(parrying, false);
        //    }
        //}
        //else
        //{
        //    ani.SetBool(parrying, false);
        //}

        // 만약 내가 공격 중인데 플레이어가 패링 중이면 패링 성공
        //  패링 중 이 아니명 플레이어 데미지 받기

        if (isAttack)
        {
            // 방법 2

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
                    {
                        // 플레이어 데미지 주기
                        if (!isPlayerDamage)
                        {
                            Debug.Log("Player Damage");
                            isPlayerDamage = true;
                        }
                    }
                }
            }
        }

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
        attackCount++;
        AttackChanged(1);
        ani.SetTrigger(attack);
        /*
        방법 2. 공격 중에는 적에 앞 방향으로 레이케스트를 쏜다 -> 애를 쓰자
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
}
