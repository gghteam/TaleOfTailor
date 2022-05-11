using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Character
{
    [SerializeField]
    private float playerDamage;
    [SerializeField]
    private GameObject attackObject;

    [SerializeField]
    private PlayerAttackState attackState;

    public float PlayerDamage { get { return playerDamage; } }

    private EventParam eventParam;

    private bool attacking = false;

    private bool attack = false;

    public bool IsAttacking { get { return attacking; } }

    private void Start()
    {
        EventManager.StartListening("InputAttack", IsInputAttack);
    }
    private void Update()
    {
        if (eventParam.boolParam && !attacking)
        {
            Debug.Log("1");
            attacking = true;
            eventParam.boolParam = false;
            ani.SetBool("IsAttack", attacking);
        }
        else if (eventParam.boolParam && attacking && !attack)
        {
            Debug.Log("2");
            attack = true;
            switch (attackState)
            {
                case PlayerAttackState.Attack1:
                    attackState = PlayerAttackState.Attack2;
                    ani.SetBool("NextAttack", attack);
                    break;
                case PlayerAttackState.Attack2:
                    attackState = PlayerAttackState.Attack1;
                    ani.SetBool("NextAttack", attack);
                    break;
            }
            eventParam.boolParam = false;
        }
        else
        {
            AniEnd();
            AniStart();
        }
    }

    private void IsInputAttack(EventParam ep)
    {
        eventParam.boolParam = ep.boolParam;
    }

    private void AniEnd()
    {
        if (attack)
        {
            if (EndAnimationDone("Attack1", 0.8897059f))
            {
                attack = false;
                ani.SetBool("NextAttack", attack);
                ani.SetBool("IsAttack", true);
            }
            else if (EndAnimationDone("Attack2", 0.8897059f))
            {
                attack = false;
                ani.SetBool("NextAttack", attack);
                ani.SetBool("IsAttack", true);
            }
        }

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Idle") && attacking)
        {
            Debug.Log("?");
            attacking = false;
            ani.SetBool("IsAttack", attacking);
        }
    }

    private void AniStart()
    {
        if(StartAnimationDone("Attack1", 0.8897059f))
        {
            attacking = true;
            ani.SetBool("IsAttack", attacking);
        }
    }

    bool EndAnimationDone(string animationname, float exittime)
    {
        return ani.GetCurrentAnimatorStateInfo(0).IsName(animationname) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= exittime;
    }
    bool StartAnimationDone(string animationname, float exittime)
    {
        return ani.GetCurrentAnimatorStateInfo(0).IsName(animationname) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0 && exittime > ani.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
