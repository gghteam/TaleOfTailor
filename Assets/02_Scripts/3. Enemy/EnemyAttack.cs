using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Character
{
    // �̷��� ������ �ұ� SO�� �ұ�?
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
        //        ani.SetBool(parrying, hit.collider.GetComponent<PlayerParrying>().CheckParrying()); // ���� ����
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

        // ���� ���� ���� ���ε� �÷��̾ �и� ���̸� �и� ����
        //  �и� �� �� �ƴϸ� �÷��̾� ������ �ޱ�

        if (isAttack)
        {
            // ��� 2

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
                    {
                        // �÷��̾� ������ �ֱ�
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
        attackCount++;
        AttackChanged(1);
        ani.SetTrigger(attack);
        /*
        ��� 2. ���� �߿��� ���� �� �������� �����ɽ�Ʈ�� ��� -> �ָ� ����
         �� ���̿� ������ Damage�ֱ�
         */

        // ���ϸ��̼� �������� Event�ޱ�
        //  AttackChanged, ColliderEnabledChanged�� Event�� �����

        // ��� 1
        //ColliderEnabledChanged(1);
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
