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

    public KeyCode parryingKeyCode = KeyCode.F;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(parryingKeyCode))
        {
            if (SteminaManager.Instance.CheckStemina(parriyngStemina))
            {
                SteminaManager.Instance.MinusStemina(parriyngStemina);
                Parrying();
            }
        }
    }

    public void Parrying()
    {
        // TODO : Parring 행동하기
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
        return false;
    }

    /// <summary>
    /// 패링 실패시에 행동 함수(구지 필요한가?)
    /// </summary>
    private void FailedParrying()
    {
        // TODO : Failed Parring
    }

    /// <summary>
    /// 패링 성공시에 행동 함수
    /// </summary>
    private void SuccessParyring()
    {
        // TODO : Success Parring
        SteminaManager.Instance.PlusStemina(parringSuccessStemina); // 스테미나 증가
    }
}