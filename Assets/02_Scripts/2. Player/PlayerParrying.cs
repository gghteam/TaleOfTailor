using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParrying : Character
{
    //재필아 안녕???? 안녕
    [SerializeField, Header("패링할시에 사용하는 스테미나 양")]
    private int parriyngStemina = 1;
    [SerializeField, Header("패링 성공시에 얻는 스테미나 양")]
    private int parringSuccessStemina = 2;

    [SerializeField, Header("패링 키")]
    private KeyCode parryingKeyCode = KeyCode.Return;
    [SerializeField, Header("패링 딜레이")]
    private float parryingDelay = .5f;

    [SerializeField, Tooltip("시야각 범위")]
    private float viewAngle = 60f;

    private int parryingLayer = 1 << 7;

    private float timer = 0f;

    private bool isParrying = false;
    public bool IsParrying { get => isParrying; }

    private Collider[] hitColl;

    // 디버그용 코드 삭제 예정...? // 적이 여러명 있을 경우를 생각안함. 현재는 한명만 있을 경우로 가장하여 실행되는 거임.

    // 상대 공격 콜라이더에 닿았을 떄
    // 상대가 공격 중일때
    // 내가 패링 중인가?

    private readonly int parrying = Animator.StringToHash("isParrying");
    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Physics.Raycast(transform.position, transform.forward, out hit, 1, parryingLayer);

        hitColl = Physics.OverlapCapsule(transform.position, transform.position + new Vector3(0, 2.2f, 0), 1, parryingLayer);
        ani.SetBool(parrying, IsParrying);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(EnemyAttack());
        //}

        if (timer >= parryingDelay)
        {
            if (Input.GetKeyDown(parryingKeyCode))
            {
                if (SteminaManager.Instance.CheckStemina(parriyngStemina))
                {
                    StartCoroutine(ParryingCoroutine());
                    SteminaManager.Instance.MinusStemina(parriyngStemina);
                    Parrying();
                    timer = 0f;
                }
            }
        }
    }

    private IEnumerator ParryingCoroutine()
    {
        isParrying = true;
        yield return new WaitForSeconds(.5f);
        isParrying = false;
    }

    public void Parrying()
    {
        // TODO : Parring 행동하기
        Debug.Log("패링");

        // Action 만들기 성공, 실패
        // Enemy쪽에서 Action을 실행시키기

        if (hitColl == null)
        {
            FailedParrying();
        }
        else
        {
            ReturnParryingData();
        }
        //if (CheckParrying())
        //{
        //    SuccessParyring();
        //}
        //else
        //{
        //    FailedParrying();
        //}
    }

    ///// <summary>
    ///// 패링이 성공했는지 체크하는 함수
    ///// </summary>
    ///// <returns></returns>
    //public bool CheckParrying()
    //{
    //    if (hit.collider == null) return false;
    //    else
    //    {
    //        //Vector3 targetDir = (hit.collider.transform.position - transform.position);
    //        //float dot = Vector3.Dot(transform.forward, targetDir);

    //        //float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

    //        //if (theta <= 60)// 내 시야각 안에 있을 때
    //        //{
    //        //    bool isAttack = hit.collider.GetComponent<EnemyAttack>().IsAttack;

    //        //    // 패링 성공시 true 반환, 실패시 false 반환
    //        //    if (isAttack && isParrying) // isAttack을 떄리는 적에게서 가져오기
    //        //        return true;
    //        //    else
    //        //        return false;
    //        //}
    //        //else
    //        //    return false;
    //    }
    //}

    /// <summary>
    /// 가져온 콜라이어 각각에게 자신의 패링 정보를 넘겨주는 함수
    /// </summary>
    private void ReturnParryingData()
    {
        foreach(var item in hitColl)
        {
            Vector3 targetDir = (item.transform.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.position, targetDir);

            float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (theta <= viewAngle)
            {
                //item.GetComponent<EnemyAttack>().IsPlayerParrying = true;
                item.GetComponent<EnemyAttack>().IsParrying(IsParrying);
            }
            else
            {
                //item.GetComponent<EnemyAttack>().IsPlayerParrying = false;
            }
        }
    }

    /// <summary>
    /// 패링 실패시에 행동 함수
    /// </summary>
    public void FailedParrying()
    {
        // TODO : Failed Parring
        Debug.Log("패링 실패!");
    }

    /// <summary>
    /// 패링 성공시에 행동 함수
    /// </summary>
    public void SuccessParrying()
    {
        // TODO : Success Parring
        Debug.Log("패링 성공!");
        SteminaManager.Instance.PlusStemina(parringSuccessStemina); // 스테미나 증가
    }
}