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
            //애니메이션 실행
            StartCoroutine(testPlayerAnimation());
            //야메 방법
            //attackObject.SetActive(true);
            //eventParam.boolParam = false;
        }
        else if(eventParam.boolParam && attacking)
        {
            //애니메이션 여러개로 바꿔주기
        }
    }

    private void IsInputAttack(EventParam ep)
    {
        eventParam.boolParam = ep.boolParam;
    }

    private IEnumerator testPlayerAnimation()
    {
        //만약에 애니메이션이 끝났다면
        yield return new WaitForSeconds(1f);
        attackObject.GetComponent<PlayerAttackCheck>().isfirst = false;
        attacking = false;
    }
}
