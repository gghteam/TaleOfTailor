using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HP : MonoBehaviour
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
    Image halfButtonImage;
    [SerializeField]
    int danchuIndex;
    [SerializeField]
    int maxDanchuIndex;

    bool isDead = false;
    bool isHalf = false;

    EventParam eventParam = new EventParam();

    private void Awake()
    {
        EventManager.StartListening("PLUSCLOTHESBUTTON", PlusClothesButton);
        EventManager.StartListening("DAMAGE", DamageSlider);
    }
    private void Start()
    {
        ResetClothesButton();
        hpSlider.value = whiteSlider.value = playerHP / maxHP;
    }
    private void OnDestroy()
    {
        EventManager.StopListening("PLUSCLOTHESBUTTON", PlusClothesButton);
        EventManager.StopListening("DAMAGE", DamageSlider);
    }
    void Update()
    {
        // ������ ������
        UpdateSlider();
    }

    // ���� ����
    void ResetClothesButton()
    {
        ClothesButtonOnOff(maxDanchuIndex);
    }

    // �÷��̾ ������ �Ծ��� �� �� ���̳ʽ�
    public void DamageSlider(EventParam eventParam)
    {
        if (isDead) return;
        playerHP -= eventParam.intParam;
        if (playerHP <= 0) isDead = true;
        else isDead = false;
        if (isDead)
        {
            Dead();
        }
    }

    // HP ������ UI Update
    void UpdateSlider()
    {
        if (isDead) return;
        hpSlider.value = playerHP / maxHP;
        whiteSlider.value = Mathf.Lerp(whiteSlider.value, playerHP / maxHP, Time.deltaTime * sliderSpeed);
    }

    // �׾��� �� ����
    void Dead()
    {
        isDead = true;
        if (!isHalf)
        {
            eventParam.intParam = danchuIndex;
            EventManager.TriggerEvent("DEAD", eventParam);
        }
        MinusClothesButton(isHalf ? 1 : 2);
        Invoke("ResetHP", 2f);
        isDead = false;
    }

    // ���� �߰��� ���̳ʽ�
    void MinusClothesButton(int minus)
    {
        danchuIndex -= minus; // ���� �� ����
        ClothesButtonOnOff(danchuIndex);
    }
    void PlusClothesButton(EventParam eventParam)
    {
        danchuIndex++; //���� ���� �� +1
        ClothesButtonOnOff(danchuIndex);
    }

    //UI ���� ���� Ű��
    void ClothesButtonOnOff(int index)
    {
        int cIndex = 0;
        isHalf = index % 2 == 0 ? false : true;
        if (index % 2 != 0) cIndex = index - 1;
        cIndex = index / 2 - 1;

        //���� ����
        for (int i = 0; i < 4; i++)
            clothesButtonImage[i].gameObject.SetActive(false);
        //�ε��������� Ű��
        for (int i = 0; i < cIndex + 1; i++)
            clothesButtonImage[i].gameObject.SetActive(true);

        Vector3 pos = clothesButtonImage[cIndex].rectTransform.anchoredPosition;

        if (isHalf) pos.x += 21f;
        else pos.x -= 21f;
        halfButtonImage.gameObject.SetActive(isHalf);
        halfButtonImage.rectTransform.anchoredPosition = pos;

    }

    // HP ���� �Լ�
    void ResetHP()
    {
        whiteSlider.value = playerHP / maxHP; // ��� �����̴� �ٽ� ä���
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed + 2); //������ ��
        playerHP = maxHP; // HP�� �ʱ�ȭ
    }

}
