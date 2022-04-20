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

    // 단추 페이드 효과
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
        // 죽었는지 확인
        if (playerHP <= 0)
        {
            Dead();
        }

        // 데미지 입히기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(20);
        }

        // 죽지 않았다면 계속 슬라이더 확인
        if(!isDead) UpdateSlider();
    }

    // 플레이어가 데미지 입었을 때 피 마이너스
    void Damage(int minusHP)
    {
        if (playerHP > 0f)
        {
            playerHP -= minusHP;
        }
    }

    // HP 게이지 UI Update
    void UpdateSlider()
    {
        hpSlider.value = playerHP / maxHP;
        whiteSlider.value = Mathf.Lerp(whiteSlider.value, playerHP/maxHP, Time.deltaTime * sliderSpeed);
    }

    // 죽었을 때 실행
    void Dead()
    {
        isDead = true;
        whiteSlider.value = playerHP / maxHP; // 흰색 슬라이더 다시 채우기
        StartCoroutine(Fade(1, 0, danchuIndex - 1)); // 단추 하나 사라지기
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed); //서서히 참
        sliderSpeed = 3f; // 슬라이더 스피드 원래대로
        playerHP = 100; // HP도 초기화
        danchuIndex--; //가진 단추 수 -1
        isDead = false;

        //단추가 할당된게 다 떨어졌을 떄
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
