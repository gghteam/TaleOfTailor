using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HP : MonoSingleton<HP>
{
    [Header("HP �����̴�")]
    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Slider whiteSlider;
    [SerializeField]
    float sliderSpeed = 5f;

    [Header("HP")]
    [SerializeField]
    float maxHP = 100;
    [SerializeField]
    public float playerHP = 100;

    [Header("����")]
    [SerializeField]
    Image[] clothesButtonImage;
    [SerializeField]
    int danchuIndex;

    bool isDead = false;
    //�׾��� �� ȣ��
    EventParam eventParam = new EventParam();

    //float fadeTime = 1f;

    // ���� ���̵� ȿ��

    //IEnumerator Fade(float start, int end, int i)
    //{
    //    float currentTime = 0f;
    //    float percent = 0f;

    //    while (percent < 1)
    //    {
    //        currentTime += Time.deltaTime;
    //        percent = currentTime / fadeTime;
    //        Color color = clothesButton[i].color;
    //        color.a = Mathf.Lerp(start, end, percent);
    //        clothesButton[i].color = color;
    //        yield return null;
    //    }
    //}

    //��Ʈ������

    private void Awake()
    {
        EventManager.StartListening("PLUSCLOTHESBUTTON", PlusClothesButton);
    }
    private void Start()
    {
        hpSlider.value = whiteSlider.value = playerHP / maxHP;
    }
    private void OnDestroy()
    {
        EventManager.StopListening("PLUSCLOTHESBUTTON", PlusClothesButton);
    }
    void Update()
    {
        // ������ ������
        UpdateSlider();
    }

    // �÷��̾ ������ �Ծ��� �� �� ���̳ʽ�
    public void DamageSlider(int minusHP)
    {
        playerHP -= minusHP;
        if (playerHP <= 0f)
        {
            isDead = true;
            Dead();
        }
    }

    // HP ������ UI Update
    void UpdateSlider()
    {
        hpSlider.value = playerHP / maxHP;
        whiteSlider.value = Mathf.Lerp(whiteSlider.value, playerHP / maxHP, Time.deltaTime * sliderSpeed);
    }

    // �׾��� �� ����
    void Dead()
    {
        playerHP = 0;
        MinusClothesButton();
        Invoke("ResetHP", 2f);
        isDead = false;
    }
    void MinusClothesButton()
    {
        danchuIndex--; //���� ���� �� -1
        clothesButtonImage[danchuIndex].gameObject.SetActive(false);
    }
    
    void PlusClothesButton(EventParam eventParam)
    {
        danchuIndex++; //���� ���� �� +1
        clothesButtonImage[danchuIndex-1].gameObject.SetActive(true);
    }
    //reset���� �Լ�
    void ResetHP()
    {
        whiteSlider.value = playerHP / maxHP; // ��� �����̴� �ٽ� ä���
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed+2); //������ ��
        playerHP = maxHP; // HP�� �ʱ�ȭ
    }

}
