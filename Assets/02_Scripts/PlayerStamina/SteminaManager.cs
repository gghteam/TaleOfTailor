using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteminaManager : MonoBehaviour
{
    // ������. ���� ����
    public Text text = null;
    public Image intImage = null;
    public Image floatImage = null;

    private float stemina = 5f;
    private const float MAX_STEMINA = 5f;
    public float Stemina
    {
        get { return stemina; }
    }
    public float steminaRecoveringDelay = 3f;

    // ���� �ʿ䰡 �ֳ�?
    private bool recovering = true;

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

        if (Input.GetKeyDown(KeyCode.D) && CheckStemina(1f))
            MinusStemina(1f);

        if (Input.GetKeyDown(KeyCode.F))
            PlusStemina(2f);
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
