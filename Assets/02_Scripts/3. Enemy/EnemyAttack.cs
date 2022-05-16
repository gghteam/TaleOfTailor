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

    private int attackLayer = 1 << 6;

    private bool isAttack = false;
    private bool isPlayerDamage = false;

    private int attackCount = 0;

    private float timer = 0f;

    private Collider[] hitColl;

    private readonly int attack = Animator.StringToHash("attack");
    private readonly int parrying = Animator.StringToHash("parrying");
    private readonly int attackCnt = Animator.StringToHash("AttackCount");

    void Start()
    {
        Reset();

        timer = enemyData.attackDelay;
    }

    void OnEnable()
    {
        timer = enemyData.attackDelay;
    }

    void OnDisable()
    {
        StopAllCoroutines();
        Reset();
    }

    void Update()
    {
        if(!ani.GetBool(parrying))
            timer += Time.deltaTime;

        hitColl = Physics.OverlapCapsule(transform.position, new Vector3(0, 2.2f, 0), enemyData.attackRange, attackLayer);

        ani.SetFloat(attackCnt, attackCount);

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    if (timer >= enemyData.attackDelay)
        //    {
        //        if (!isAttack)
        //        {
        //            Attack();
        //            timer = 0;
        //        }
        //    }
        //}

        if (timer >= enemyData.attackDelay)
        {
            if (!isAttack)
            {
                Attack();
                timer = 0;
            }
        }
    }

    /// <summary>
    /// 패링당했을 때 함수
    /// </summary>
    private void ParryingAction()
    {
        Debug.Log("쫌 치네ㅋ");
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    private void Attack()
    {
        attackCount = (attackCount + 1) % 2;
        AttackChange(1);
        ani.SetTrigger(attack);

        if (isAttack)
        {
            foreach(var hitObj in hitColl)
            {
                if (hitObj.CompareTag("Player"))
                {
                    StartCoroutine(AttackCoroutine(hitObj.gameObject));
                }
            }
        }
    }

    /// <summary>
    /// 공격 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCoroutine(GameObject hitObj)
    {
        yield return new WaitForSeconds(.5f);

        if (!isPlayerDamage)
        {
            PlayerParrying player = hitObj.GetComponent<PlayerParrying>();
            bool isParrying = player.IsParrying;

            if (isParrying)
            {
                if (player.IsInViewangle(this.transform))
                {
                    player.SuccessParrying();
                    ParryingChange(1);
                    ParryingAction();
                }
                else
                {
                    player.FailedParrying();
                    ParryingChange(0);
                    Debug.Log("플레이어 대미지 주기");
                    PlayerDamageChange(1);
                }
            }
            else
            {
                player.FailedParrying();
                ParryingChange(0);
                Debug.Log("플레이어 대미지 주기");
                PlayerDamageChange(1);
;            }
        }
    }

    /// <summary>
    /// 애니메이터안에 있는 parrying파라매터를 변화시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void ParryingChange(int value)
    {
        ani.SetBool(parrying, value == 0 ? false : true);
    }

    /// <summary>
    /// isAttack변수(현재 공격중인지?)를 변경시키는 함수
    /// </summary>
    /// <param name="value"></param>
    private void AttackChange(int value)
    {
        isAttack = value == 0 ? false : true;
    }

    /// <summary>
    /// isPlayerDamage변수(플레이어가 데미지를 받는 중인지?)를 변경시키는 함수. 도트뎀을 막는 용도
    /// </summary>
    /// <param name="value"></param>
    private void PlayerDamageChange(int value)
    {
        isPlayerDamage = value == 0 ? false : true;
    }

    public void Reset()
    {
        PlayerDamageChange(0);
        AttackChange(0);
        ParryingChange(0);
    }
}
