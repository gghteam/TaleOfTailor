using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    [Header("HP")]
    [Header("�÷��̾�")]
    [SerializeField]
    float maxPlayerHP = 3000;
    [SerializeField]
    public float playerHP = 3000;
    [Header("����")]
    [SerializeField]
    float maxBossHP = 3000;
    [SerializeField]
    public float bossHP = 3000;
    [SerializeField, Header("HP �����̴� �ӵ�")]
    float sliderSpeed = 5f;

    [Header("���� ����")]
    [SerializeField]
    int danchuCount;
    [SerializeField]
    int maxDanchuCount;

    [Header("HP �����̴�")]
    [SerializeField]
    Slider playerHpSlider;
    [SerializeField]
    Slider whiteSlider;
    [SerializeField]
    Slider bossHpSlider;

    [Header("���� UI")]
    [SerializeField]
    Image[] clothesButtonImage;
    [SerializeField]
    Image halfButtonImage;

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
        playerHpSlider.value = whiteSlider.value = playerHP / maxPlayerHP;
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
        ClothesButtonOnOff(maxDanchuCount);
    }

    // �÷��̾ ������ �Ծ��� �� �� ���̳ʽ�
    public void DamageSlider(EventParam eventParam)
    {
        if(eventParam.stringParam=="PLAYER")
        {
            if (isDead) return;
            playerHP -= eventParam.intParam;

            if (playerHP <= 0) isDead = true;
            else isDead = false;
            if (isDead) Dead();
        }
        else if(eventParam.stringParam=="BOSS")
        {
            bossHP-=eventParam.intParam;
            if(bossHP <= 0)
            {
                bossHpSlider.gameObject.SetActive(false);
            }
        }
       
    }

    // HP ������ UI Update
    void UpdateSlider()
    {
        bossHpSlider.value = bossHP / maxBossHP;
        if (isDead) return;
        playerHpSlider.value = playerHP / maxPlayerHP;
        whiteSlider.value = Mathf.Lerp(whiteSlider.value, playerHP / maxPlayerHP, Time.deltaTime * sliderSpeed);
    }

    // �׾��� �� ����
    void Dead()
    {
        isDead = true;
        if (!isHalf)
        {
            eventParam.intParam = danchuCount;
            EventManager.TriggerEvent("DEAD", eventParam);
        }
        MinusClothesButton(isHalf ? 1 : 2);
        Invoke("ResetHP", 2f);
        isDead = false;
    }

    // ���� �߰��� ���̳ʽ�
    void MinusClothesButton(int minus)
    {
        danchuCount -= minus; // ���� �� ����
        if (danchuCount <= 0) Debug.Log("����");
        else ClothesButtonOnOff(danchuCount);
    }
    void PlusClothesButton(EventParam eventParam)
    {
        danchuCount++; //���� ���� �� +1
        ClothesButtonOnOff(danchuCount);
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
        whiteSlider.value = playerHP / maxPlayerHP; // ��� �����̴� �ٽ� ä���
        bossHpSlider.value = bossHP/maxBossHP;
        playerHpSlider.value = Mathf.Lerp(playerHpSlider.value, 1, Time.deltaTime * sliderSpeed + 2); //������ ��
        playerHP = maxPlayerHP; // HP�� �ʱ�ȭ
    }

}
