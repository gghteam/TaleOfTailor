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

    [SerializeField, Header("�и� Ű")]
    private KeyCode parryingKeyCode = KeyCode.Return;
    [SerializeField, Header("�и� ������")]
    private float parryingDelay = .5f;

    private float timer = 0f;

    private bool isParrying = false;
    public bool IsParrying { get => isParrying; }

    // ����׿� �ڵ� �ƴ�����...?
    private bool isAttack = false;
    public bool IsAttack
    {
        get => isAttack;
        set => isAttack = value;
    }
    private GameObject enemy = null;
    public GameObject Enemy
    {
        get => enemy;
        set
        {
            if (!IsAttack)
            {
                enemy = null;
            }
            else
            {
                enemy = value;
            }
        }
    }

    // ��� ���� �ݶ��̴��� ����� ��
    // ��밡 ���� ���϶�
    // ���� �и� ���ΰ�?

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
    public bool CheckParrying()
    {
        if (enemy == null) return false;
        Vector3 targetDir = (enemy.transform.position - transform.position);
        float dot = Vector3.Dot(transform.forward, targetDir);

        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        if (theta <= 60)// �� �þ߰� �ȿ� ���� ��
        {
            // �и� ������ true ��ȯ, ���н� false ��ȯ
            if (isAttack && isParrying) // isAttack�� ������ �����Լ� ��������
                return true;
            else
                return false;
        }
        else
            return false;
    }

    /// <summary>
    /// �и� ���нÿ� �ൿ �Լ�
    /// </summary>
    private void FailedParrying()
    {
        // TODO : Failed Parring
        Debug.Log("�и� ����!");
        if(isAttack)
            Debug.Log("�������� �޾ҽ��ϴ�.");
    }

    /// <summary>
    /// �и� �����ÿ� �ൿ �Լ�
    /// </summary>
    private void SuccessParyring()
    {
        // TODO : Success Parring
        Debug.Log("�и� ����!");
        SteminaManager.Instance.PlusStemina(parringSuccessStemina); // ���׹̳� ����
    }
}