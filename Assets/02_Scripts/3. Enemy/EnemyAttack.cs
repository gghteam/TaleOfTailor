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

        // ���� ���� ���� ���ε� �÷��̾ �и� ���̸� �и� ����
        //  �и� �� �� �ƴϸ� �÷��̾� ������ �ޱ�

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
        Debug.Log("�и� ����");
        Debug.Log("Player Damage");
        isPlayerDamage = true;
    }

    private void SuccessParrying()
    {
        Debug.Log("�и� ����");
        hit.collider.GetComponent<PlayerParrying>()?.SuccessParrying();
        ParryingAction();
    }

    /// <summary>
    /// ���ݴ����� �� �Լ�
    /// </summary>
    private void ParryingAction()
    {
        // �и� ������ �� �ൿ
        Debug.Log("�� ġ�פ�");
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    private void Attack()
    {
        /*
        ��� 2. ���� �߿��� ���� �� �������� �����ɽ�Ʈ�� ��� -> �ָ� ����
         �� ���̿� ������ Damage�ֱ�
         */

        // ���ϸ��̼� �������� Event�ޱ�
        //  AttackChanged, ColliderEnabledChanged�� Event�� �����
        attackCount++;
        AttackChanged(1);
        ani.SetTrigger(attack);

        if (isAttack)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // �ִϸ��̼��� ���� �ð� ����Ǿ��� ��
                    //  isParryingSuccess�� true�� �и� ���ع���!
                    //      �ƴϸ� ������ ������!

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
                    //        Debug.Log("�÷��̾� ����� �ֱ�");
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
    /// ���� �ڷ�ƾ
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
                    Debug.Log("�÷��̾� ����� �ֱ�");
                    isPlayerDamage = true;
                }
            }
            else
            {
                player.FailedParrying();
                Debug.Log("�÷��̾� ����� �ֱ�");
                isPlayerDamage = true;
            }
        }
    }

    [System.Obsolete]
    public void IsParrying(bool isParrying)
    {
        if(isParrying && isAttack)
        {
            Debug.Log("�и� ����");
            isParryingSuccess = true;
        }
        else
        {
            Debug.Log("�и� ����");
            isParryingSuccess = false;
        }
    }

    /// <summary>
    /// isAttack����(���� ����������?)�� �����Ű�� �Լ�
    /// </summary>
    /// <param name="value"></param>
    private void AttackChanged(int value)
    {
        isAttack = value == 0 ? false : true;
    }

    /// <summary>
    /// isPlayerDamage����(�÷��̾ �������� �޴� ������?)�� �����Ű�� �Լ�. ��Ʈ���� ���� �뵵
    /// </summary>
    /// <param name="value"></param>
    private void PlayerDamageChanged(int value)
    {
        isPlayerDamage = value == 0 ? false : true;
    }
}
