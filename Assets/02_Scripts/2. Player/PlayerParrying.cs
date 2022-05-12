using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParrying : Character
{
    //���ʾ� �ȳ�???? �ȳ�
    [SerializeField, Header("�и��ҽÿ� ����ϴ� ���׹̳� ��")]
    private int parriyngStemina = 1;
    [SerializeField, Header("�и� �����ÿ� ��� ���׹̳� ��")]
    private int parringSuccessStemina = 2;

    [SerializeField, Header("�и� Ű")]
    private KeyCode parryingKeyCode = KeyCode.Return;
    [SerializeField, Header("�и� ������")]
    private float parryingDelay = .5f;

    [SerializeField, Tooltip("�þ߰� ����")]
    private float viewAngle = 60f;

    private int parryingLayer = 1 << 7;

    private float timer = 0f;

    private bool isParrying = false;
    public bool IsParrying { get => isParrying; }

    private Collider[] hitColl;

    // ����׿� �ڵ� ���� ����...? // ���� ������ ���� ��츦 ��������. ����� �Ѹ� ���� ���� �����Ͽ� ����Ǵ� ����.

    // ��� ���� �ݶ��̴��� ����� ��
    // ��밡 ���� ���϶�
    // ���� �и� ���ΰ�?

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
        // TODO : Parring �ൿ�ϱ�
        Debug.Log("�и�");

        // Action ����� ����, ����
        // Enemy�ʿ��� Action�� �����Ű��

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
    ///// �и��� �����ߴ��� üũ�ϴ� �Լ�
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

    //        //if (theta <= 60)// �� �þ߰� �ȿ� ���� ��
    //        //{
    //        //    bool isAttack = hit.collider.GetComponent<EnemyAttack>().IsAttack;

    //        //    // �и� ������ true ��ȯ, ���н� false ��ȯ
    //        //    if (isAttack && isParrying) // isAttack�� ������ �����Լ� ��������
    //        //        return true;
    //        //    else
    //        //        return false;
    //        //}
    //        //else
    //        //    return false;
    //    }
    //}

    /// <summary>
    /// ������ �ݶ��̾� �������� �ڽ��� �и� ������ �Ѱ��ִ� �Լ�
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
    /// �и� ���нÿ� �ൿ �Լ�
    /// </summary>
    public void FailedParrying()
    {
        // TODO : Failed Parring
        Debug.Log("�и� ����!");
    }

    /// <summary>
    /// �и� �����ÿ� �ൿ �Լ�
    /// </summary>
    public void SuccessParrying()
    {
        // TODO : Success Parring
        Debug.Log("�и� ����!");
        SteminaManager.Instance.PlusStemina(parringSuccessStemina); // ���׹̳� ����
    }
}