using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Character
{
    [SerializeField]
    private float playerDamage;
    [SerializeField]
    private GameObject attackObject;

    public float PlayerDamage { get { return playerDamage; } }

    private EventParam eventParam;

    private bool attacking = false;

    public bool IsAttacking { get { return attacking; } }

    private void Start()
    {
        EventManager.StartListening("InputAttack", IsInputAttack);
    }
    private void Update()
    {
        if (eventParam.boolParam && !attacking)
        {
            attacking = true;
            eventParam.boolParam = false;
            //�ִϸ��̼� ����
            ani.SetBool("IsAttack", attacking);
        }
        else if (eventParam.boolParam && attacking)
        {
            //�ִϸ��̼� �������� �ٲ��ֱ�
        }
        AniEnd();
    }

    private void IsInputAttack(EventParam ep)
    {
        eventParam.boolParam = ep.boolParam;
    }

    private void AniEnd()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8897059f)
        {
            attackObject.GetComponent<PlayerAttackCheck>().isfirst = false;
            attacking = false;
            ani.SetBool("IsAttack", attacking);
        }
    }
}
