using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // �̷��� ������ �ұ� SO�� �ұ�?
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
        //    // ��� 2
        //    Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), transform.forward, out hit, 1, attackLlayer);

        //    if (hit.collider != null)
        //    {
        //        if (hit.collider.CompareTag("Player"))
        //        {
        //            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        //            {
        //                // �÷��̾� ������ �ֱ�
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
        AttackChanged(1);
        animator.SetTrigger(attack);
        /*
        ��� 1. �� �տ� Collider�� �ް� ���� �� Collider�� ��Ƽ�긦 Ű�� ���
         �� �ݶ��̴��� ������ Damage�ֱ�
        ��� 2. ���� �߿��� ���� �� �������� �����ɽ�Ʈ�� ���
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

    /// <summary>
    /// ���� �ݶ��̴��� Enabled�� �����Ű�� �Լ�
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
                // �÷��̾� ������ �ֱ�
                Debug.Log("Player Damage");
                PlayerDamageChanged(1);
            }
        }
    }
}
