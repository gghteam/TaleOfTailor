using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Character
{
    // �̷��� ������ �ұ� SO�� �ұ�?
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
    /// �и������� �� �Լ�
    /// </summary>
    private void ParryingAction()
    {
        Debug.Log("�� ġ�פ�");
    }

    /// <summary>
    /// ���� �Լ�
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
    /// ���� �ڷ�ƾ
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
                    Debug.Log("�÷��̾� ����� �ֱ�");
                    PlayerDamageChange(1);
                }
            }
            else
            {
                player.FailedParrying();
                ParryingChange(0);
                Debug.Log("�÷��̾� ����� �ֱ�");
                PlayerDamageChange(1);
;            }
        }
    }

    /// <summary>
    /// �ִϸ����;ȿ� �ִ� parrying�Ķ���͸� ��ȭ��Ű�� �Լ�
    /// </summary>
    /// <param name="value"></param>
    private void ParryingChange(int value)
    {
        ani.SetBool(parrying, value == 0 ? false : true);
    }

    /// <summary>
    /// isAttack����(���� ����������?)�� �����Ű�� �Լ�
    /// </summary>
    /// <param name="value"></param>
    private void AttackChange(int value)
    {
        isAttack = value == 0 ? false : true;
    }

    /// <summary>
    /// isPlayerDamage����(�÷��̾ �������� �޴� ������?)�� �����Ű�� �Լ�. ��Ʈ���� ���� �뵵
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
