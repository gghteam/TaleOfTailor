using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteminaManager : MonoBehaviour
{
    // 디버깅용. 삭제 예정
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

    // 있을 필요가 있나?
    private bool recovering = true;

    private void Start()
    {
        stemina = 0;
    }

    void Update()
    {
        SteminaRecovering();

        // 디버깅 용. 삭제 예정
        text.text = string.Format("{0:F2}", stemina);
        intImage.fillAmount = (int)stemina / MAX_STEMINA;
        floatImage.fillAmount = (float)stemina / MAX_STEMINA;

        if (Input.GetKeyDown(KeyCode.D) && CheckStemina(1f))
            MinusStemina(1f);

        if (Input.GetKeyDown(KeyCode.F))
            PlusStemina(2f);
    }

    /// <summary>
    /// 스테미나를 쓸 수 있는 량인지 확인 하는 bool값 리턴 함수
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
    /// 스테미나 줄어드는 애니메니션을 넣어주는 코루틴.
    /// 삭제 예정
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
    /// 스테미나 늘어나는 애니메이션을 넣어주는 코루틴.
    /// 삭제 예정
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
    /// 스테미나 재생
    /// </summary>
    private void SteminaRecovering()
    {
        // 스테미나 재생중 && 스테미나가 최대 스테미나보다 낮을 때
        if (recovering && (stemina <= MAX_STEMINA))
        {
            stemina += Time.deltaTime / steminaRecoveringDelay;
        }

        if (stemina > MAX_STEMINA)
            stemina = MAX_STEMINA;
    }
}
