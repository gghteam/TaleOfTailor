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
    float sliderSpeed = 3f;

    [Header("HP")]
    [SerializeField]
    float maxHP = 100;
    [SerializeField]
    float playerHP = 100;

    [Header("����")]
    [SerializeField]
    Image[] clothesButtonImage;
    [SerializeField]
    int danchuIndex = 4;

    bool isDead = false;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamageSlider(20);
        }
        UpdateSlider();
    }

    // �÷��̾ ������ �Ծ��� �� �� ���̳ʽ�
    void DamageSlider(int minusHP)
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
        danchuIndex--; //���� ���� �� -1
        Invoke("ResetHP", 2f);
        isDead = false;
    }

    //reset���� �Լ�
    void ResetHP()
    {
        sliderSpeed = 3f; // �����̴� ���ǵ� �������
        whiteSlider.value = playerHP / maxHP; // ��� �����̴� �ٽ� ä���
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed); //������ ��
        playerHP = 100; // HP�� �ʱ�ȭ
    }

    void ClothesButtonImageReset()
    {
        //���߰� �Ҵ�Ȱ� �� �������� ��
        if (danchuIndex < 1)
        {
            for (int i = 0; i < clothesButtonImage.Length; i++)
            {
                danchuIndex = 4;
            }
            return;
        }
    }
}
