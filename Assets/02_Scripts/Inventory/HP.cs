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
    float sliderSpeed = 3f;

    [Header("HP")]
    [SerializeField]
    float maxHP = 100;
    [SerializeField]
    public float playerHP = 100;

    [Header("����")]
    [SerializeField]
    Image[] clothesButtonImage;
    [SerializeField]
    int danchuIndex = 4;

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

    private void Start()
    {
        hpSlider.value = whiteSlider.value = playerHP / maxHP;
    }

    void Update()
    {
        // ������ ������
        UpdateSlider();
    }

    // �÷��̾ ������ �Ծ��� �� �� ���̳ʽ�
    public void DamageSlider(int minusHP)
    {
        if (playerHP > 0f)
        {
            playerHP -= minusHP;
        }
        else
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
        ClothesIndexCheck();
        Invoke("ResetHP", 2f);
        isDead = false;
    }
    void ClothesIndexCheck()
    {
        danchuIndex--; //���� ���� �� -1
        clothesButtonImage[danchuIndex].gameObject.SetActive(false);
    }
    //reset���� �Լ�
    void ResetHP()
    {
        sliderSpeed = 3f; // �����̴� ���ǵ� �������
        whiteSlider.value = playerHP / maxHP; // ��� �����̴� �ٽ� ä���
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed); //������ ��
        playerHP = 100; // HP�� �ʱ�ȭ
    }

}
