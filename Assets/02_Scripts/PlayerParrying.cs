using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParrying : MonoBehaviour
{
    //���ʾ� �ȳ�???? �ȳ�
    [SerializeField, Header("�и��ҽÿ� ����ϴ� ���׹̳� ��")]
    private int parriyngStemina = 1;
    [SerializeField, Header("�и� �����ÿ� ��� ���׹̳� ��")]
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
        // TODO : Parring �ൿ�ϱ�
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
    /// �и��� �����ߴ��� üũ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    private bool CheckParrying()
    {
        // �и� ������ true ��ȯ, ���н� false ��ȯ
        return false;
    }

    /// <summary>
    /// �и� ���нÿ� �ൿ �Լ�(���� �ʿ��Ѱ�?)
    /// </summary>
    private void FailedParrying()
    {
        // TODO : Failed Parring
    }

    /// <summary>
    /// �и� �����ÿ� �ൿ �Լ�
    /// </summary>
    private void SuccessParyring()
    {
        // TODO : Success Parring
        SteminaManager.Instance.PlusStemina(parringSuccessStemina); // ���׹̳� ����
    }
}