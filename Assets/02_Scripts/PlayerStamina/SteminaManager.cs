using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SteminaManager : MonoBehaviour
{
    // ���� �̱���
    public static SteminaManager Instance = null;

    // ������. ���� ����
    public Text text = null;
    public Image intImage = null;
    public Image floatImage = null;

    public GameObject steminaBar = null;
    [Header("Shake���� ���� : �ð�, ����, ������ ������")]
    [SerializeField] private float duration = 1f, strength = 1f,randomness = 90f;
    [SerializeField, Header("���� ����")] int vibrato = 10;

    private float stemina = 5f;
    private const float MAX_STEMINA = 5f;
    public float Stemina
    {
        get { return stemina; }
    }

    [Header("���׹̳� ��� �ӵ� ���� ��, ���� ���� ������")]
    public float steminaRecoveringDelay = 3f;

    [Header("���׹̳� ���ϴ� ��")]
    public float plusSteminaValue = 2f;
    [Header("���׹̳� ���� ��")]
    public float minusSteminaValue = 1f;

    [Header("���ϴ� Ű")]
    public KeyCode plusKeyCode = KeyCode.F;
    [Header("���� Ű")]
    public KeyCode minusKeyCode = KeyCode.D;

    // ���� �ʿ䰡 �ֳ�?
    private bool recovering = true;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple SteminaManager is running!");
        }
        Instance = this;
    }

    private void Start()
    {
        stemina = 0;
    }

    void Update()
    {
        SteminaRecovering();

        // ����� ��. ���� ����
        text.text = string.Format("{0:F2}", stemina);
        intImage.fillAmount = (int)stemina / MAX_STEMINA;
        floatImage.fillAmount = (float)stemina / MAX_STEMINA;

        if (Input.GetKeyDown(minusKeyCode))
        {
            if (CheckStemina(minusSteminaValue))
            {
                MinusStemina(1f);
            }
            else
            {
                MinusStemianFailedEffect();
            }
        }

        if (Input.GetKeyDown(plusKeyCode))
            PlusStemina(plusSteminaValue);
    }

    private void MinusStemianFailedEffect()
    {
        // TODO : ���׹̳��� ����
        steminaBar.transform.DOShakePosition(duration, strength, vibrato, randomness);
    }

    /// <summary>
    /// ���׹̳��� �� �� �ִ� ������ Ȯ�� �ϴ� bool�� ���� �Լ�
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool CheckStemina(float value)
    {
        if (stemina > value)
            return true;
        else
            return false;
    }

    public void MinusStemina(float value)
    {
        stemina -= value;
        //StartCoroutine(MinusSteminaCoroutine(value));
    }

    /// <summary>
    /// ���׹̳� �پ��� �ִϸ޴ϼ��� �־��ִ� �ڷ�ƾ.
    /// ���� ����
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IEnumerator MinusSteminaCoroutine(float input)
    {
        float value = input;
        recovering = false;
        while(input > 0)
        {
            input -= Time.deltaTime;
            stemina -= Time.deltaTime;
            //yield return new WaitForSeconds(.0001f);
            yield return null;
        }
        recovering = true;
    }
    public void PlusStemina(float input)
    {
        float value = Mathf.Min(input, MAX_STEMINA - stemina);
        stemina += value;
        //StartCoroutine(PlusSteminaCoroutine(input));
    }

    /// <summary>
    /// ���׹̳� �þ�� �ִϸ��̼��� �־��ִ� �ڷ�ƾ.
    /// ���� ����
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IEnumerator PlusSteminaCoroutine(float input)
    {
        float value = input;
        recovering = false;
        while (input > 0)
        {
            input -= Time.deltaTime;
            stemina += Time.deltaTime;
            //yield return new WaitForSeconds(.0001f);
            yield return null;
        }
        recovering = true;
    }

    /// <summary>
    /// ���׹̳� ���
    /// </summary>
    private void SteminaRecovering()
    {
        // ���׹̳� ����� && ���׹̳��� �ִ� ���׹̳����� ���� ��
        if (recovering && (stemina <= MAX_STEMINA))
        {
            stemina += Time.deltaTime / steminaRecoveringDelay;
        }

        if (stemina > MAX_STEMINA)
            stemina = MAX_STEMINA;
    }
}
