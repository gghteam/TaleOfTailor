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
        Debug.Log(eventParam.boolParam);
        Debug.Log(attacking);
        if (eventParam.boolParam && !attacking)
        {
            Debug.Log("&");
            attacking = true;
            eventParam.boolParam = false;
            //�ִϸ��̼� ����
            StartCoroutine(testPlayerAnimation());
            //�߸� ���
            //attackObject.SetActive(true);
            //eventParam.boolParam = false;
        }
        else if(eventParam.boolParam && attacking)
        {
            //�ִϸ��̼� �������� �ٲ��ֱ�
        }
    }

    private void IsInputAttack(EventParam ep)
    {
        eventParam.boolParam = ep.boolParam;
    }

    private IEnumerator testPlayerAnimation()
    {
        //���࿡ �ִϸ��̼��� �����ٸ�
        yield return new WaitForSeconds(1f);
        attackObject.GetComponent<PlayerAttackCheck>().isfirst = false;
        attacking = false;
    }
}
