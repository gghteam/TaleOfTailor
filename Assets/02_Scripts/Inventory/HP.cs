using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Slider whiteSlider;
    [SerializeField]
    Image[] danchu;

    float maxHP = 100;
    float playerHP = 100;
    int danchuIndex = 4;
    float sliderSpeed = 3f;
    float fadeTime = 1f;
    bool isDead = false;

    // ���� ���̵� ȿ��
    IEnumerator Fade(float start, int end, int i)
    {
        float currentTime = 0f;
        float percent = 0f;

        while(percent<1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            Color color = danchu[i].color;
            color.a = Mathf.Lerp(start, end, percent);
            danchu[i].color = color;
            yield return null;
        }
    }

    private void Start()
    {
        hpSlider.value = whiteSlider.value = playerHP / maxHP;
    }

    void Update()
    {
        // �׾����� Ȯ��
        if (playerHP <= 0)
        {
            Dead();
        }

        // ������ ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(20);
        }

        // ���� �ʾҴٸ� ��� �����̴� Ȯ��
        if(!isDead) UpdateSlider();
    }

    // �÷��̾ ������ �Ծ��� �� �� ���̳ʽ�
    void Damage(int minusHP)
    {
        if (playerHP > 0f)
        {
            playerHP -= minusHP;
        }
    }

    // HP ������ UI Update
    void UpdateSlider()
    {
        hpSlider.value = playerHP / maxHP;
        whiteSlider.value = Mathf.Lerp(whiteSlider.value, playerHP/maxHP, Time.deltaTime * sliderSpeed);
    }

    // �׾��� �� ����
    void Dead()
    {
        isDead = true;
        whiteSlider.value = playerHP / maxHP; // ��� �����̴� �ٽ� ä���
        StartCoroutine(Fade(1, 0, danchuIndex - 1)); // ���� �ϳ� �������
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed); //������ ��
        sliderSpeed = 3f; // �����̴� ���ǵ� �������
        playerHP = 100; // HP�� �ʱ�ȭ
        danchuIndex--; //���� ���� �� -1
        isDead = false;

        //���߰� �Ҵ�Ȱ� �� �������� ��
        if (danchuIndex < 1)
        {
            for (int i = 0; i < danchu.Length; i++)
            {
                StartCoroutine(Fade(0, 1, i));
                danchuIndex = 4;
            }
            return;
        }
    }


}
