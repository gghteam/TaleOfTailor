using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Character
{
    // 이렇게 변수로 할까 SO로 할까?
    //[SerializeField]
    //private float attackDamage = 1f;
    //[SerializeField]
    //private float attackDelay = 0.5f;

    [SerializeField]
    private EnemyDataSO enemyData = null;

    private int attackLlayer = 1 << 6;

    private bool isAttack = false;
    public bool IsAttack { get => isAttack; }

    private bool isParryingSuccess = false;

    private bool isPlayerDamage = false;

    private int attackCount = 0;

    private float timer = 0f;

    RaycastHit hit;

    private readonly int attack = Animator.StringToHash("attack");
    private readonly int parrying = Animator.StringToHash("parrying");
    private readonly int attackCnt = Animator.StringToHash("AttackCount");

    void Start()
    {
        timer = enemyData.attackDelay;
    }

    void OnEnable()
    {
        timer = enemyData.attackDelay;
    }

    void Update()
    {
        timer += Time.deltaTime;

        Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), transform.forward, out hit, enemyData.attackRange, attackLlayer);
        ani.SetFloat(attackCnt, attackCount % 2);
        ani.SetBool(parrying, isParryingSuccess);

        // 만약 내가 공격 중인데 플레이어가 패링 중이면 패링 성공
        //  패링 중 이 아니명 플레이어 데미지 받기

        Debug.DrawRay(transform.position + new Vector3(0, 1.5f, 0), transform.forward, Color.green);

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (timer >= enemyData.attackDelay)
            {
                if (!isAttack)
                {
                    Attack();
                    timer = 0;
                }
            }
        }

        
    }

    private void FailedParrying()
    {
        hit.collider.GetComponent<PlayerParrying>()?.FailedParrying();
        Debug.Log("패링 실패");
        Debug.Log("Player Damage");
        isPlayerDamage = true;
    }

    private void SuccessParrying()
    {
        Debug.Log("패링 성공");
        hit.collider.GetComponent<PlayerParrying>()?.SuccessParrying();
        ParryingAction();
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
        /*
        방법 2. 공격 중에는 적에 앞 방향으로 레이케스트를 쏜다 -> 애를 쓰자
         그 레이에 닿으면 Damage주기
         */

        // 에니메이션 마지막에 Event달기
        //  AttackChanged, ColliderEnabledChanged를 Event로 등록함
        attackCount++;
        AttackChanged(1);
        ani.SetTrigger(attack);

        if (isAttack)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // 애니메이션이 일정 시간 재생되었을 때
                    //  isParryingSuccess가 true면 패링 당해버렸!
                    //      아니면 데미지 져버렸!

                    //if(ani.GetCurrentAnimatorStateInfo(0).IsName("Attack Blend Tree") &&
                    //    ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f &&
                    //    ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
                    //if (!isPlayerDamage)
                    //{
                    //    if (isParryingSuccess)
                    //    {
                    //        hit.collider.GetComponent<PlayerParrying>().SuccessParrying();
                    //        ParryingAction();
                    //    }
                    //    else
                    //    {
                    //        hit.collider.GetComponent<PlayerParrying>().FailedParrying();
                    //        Debug.Log("플레이어 대미지 주기");
                    //        isPlayerDamage = true;
                    //    }
                    //}
                    StartCoroutine(AttackCoroutine());
                }
            }
            else
            {
                isParryingSuccess = false;
            }
        }
    }

    /// <summary>
    /// 공격 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        if (!isPlayerDamage)
        {
            PlayerParrying player = hit.collider.GetComponent<PlayerParrying>();
            bool isParrying = player.IsParrying;

            if (isParrying)
            {
                if (player.IsInViewangle(this.transform))
                {
                    player.SuccessParrying();
                    ParryingAction();
                }
                else
                {
                    player.FailedParrying();
                    Debug.Log("플레이어 대미지 주기");
                    isPlayerDamage = true;
                }
            }
            else
            {
                player.FailedParrying();
                Debug.Log("플레이어 대미지 주기");
                isPlayerDamage = true;
            }
        }
    }

    [System.Obsolete]
    public void IsParrying(bool isParrying)
    {
        if(isParrying && isAttack)
        {
            Debug.Log("패링 성공");
            isParryingSuccess = true;
        }
        else
        {
            Debug.Log("패링 실패");
            isParryingSuccess = false;
        }
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
