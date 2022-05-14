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

    private float timer = 0f;

    private bool isParrying = false;
    public bool IsParrying { get => isParrying; }

    private readonly int parrying = Animator.StringToHash("isParrying");
    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        ani.SetBool(parrying, IsParrying);

        if (timer >= parryingDelay)
        {
            if (Input.GetKeyDown(parryingKeyCode))
            {
                if (SteminaManager.Instance.CheckStemina(parriyngStemina))
                {
                    SteminaManager.Instance.MinusStemina(parriyngStemina);
                    Parrying();
                    timer = 0f;
                }
            }
        }
    }

    /// <summary>
    /// �и� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ParryingCoroutine()
    {
        isParrying = true;
        yield return new WaitForSeconds(.5f);
        isParrying = false;
    }

    /// <summary>
    /// �и� �Լ�
    /// </summary>
    public void Parrying()
    {
        // TODO : Parring �ൿ�ϱ�
        Debug.Log("�и�");
        StartCoroutine(ParryingCoroutine());
    }

    /// <summary>
    /// tr�� ���� �þ߰��� ����ؼ� tr�� �� �þ߳��� �ִ����� ��ȯ���ִ� �Լ�
    /// </summary>
    /// <param name="tr"></param>
    /// <returns></returns>
    public bool IsInViewangle(Transform tr)
    {
        Vector3 targetDir = (tr.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, targetDir);

        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (theta <= viewAngle)
            return true;
        else
            return false;
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