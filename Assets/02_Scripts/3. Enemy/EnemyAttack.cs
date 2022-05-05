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
        ��� 1. �� �տ� Collider�� �ް� ���� �� Collider�� ��Ƽ�긦 Ű�� ���
         �� �ݶ��̴��� ������ Damage�ֱ�
        ��� 2. ���� �߿��� ���� �� �������� �����ɽ�Ʈ�� ���
         �� ���̿� ������ Damage�ֱ�
         */

        // ���ϸ��̼� �������� Event�ޱ�
        //  AttackChanged, ColliderEnabledChanged�� Event�� ���

        // ��� 1
        ColliderEnabledChanged(true);

        // ��� 2
        if(hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                // �÷��̾� ������ �ֱ�
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
            // �÷��̾� ������ �ֱ�
        }
    }
}
