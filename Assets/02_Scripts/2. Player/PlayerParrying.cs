using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParrying : MonoBehaviour
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

    private float timer = 0f;

    private bool isParrying = false;
    public bool IsParrying { get => isParrying; }

    // 디버그용 코드 아닐지도...?
    private bool isAttack = false;
    public bool IsAttack
    {
        get => isAttack;
        set => isAttack = value;
    }

    // 상대 공격 콜라이더에 닿았을 떄
    // 상대가 공격 중일때
    // 내가 패링 중인가?

    private Animator animator;

    private readonly int parrying = Animator.StringToHash("isParrying");
    void Start()
    {
        animator = GetComponent<Animator>();

        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        animator.SetBool(parrying, IsParrying);

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

    // 디버그용 코든
    private IEnumerator EnemyAttack()
    {
        isAttack = true;
        yield return new WaitForSeconds(.5f);
        isAttack = false;
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
        if (CheckParrying())
        {
            SuccessParyring();
        }
        else
        {
            FailedParrying();
        }
    }

    /// <summary>
    /// 패링이 성공했는지 체크하는 함수
    /// </summary>
    /// <returns></returns>
    private bool CheckParrying()
    {
        // 패링 성공시 true 반환, 실패시 false 반환
        if (isAttack && isParrying) // isAttack을 떄리니 적에게서 가져오기
            return true;
        else
            return false;
    }

    /// <summary>
    /// 패링 실패시에 행동 함수
    /// </summary>
    private void FailedParrying()
    {
        // TODO : Failed Parring
        Debug.Log("패링 실패!");
        if(isAttack)
            Debug.Log("데미지를 받았습니다.");
    }

    /// <summary>
    /// 패링 성공시에 행동 함수
    /// </summary>
    private void SuccessParyring()
    {
        // TODO : Success Parring
        Debug.Log("패링 성공!");
        SteminaManager.Instance.PlusStemina(parringSuccessStemina); // 스테미나 증가
    }
}