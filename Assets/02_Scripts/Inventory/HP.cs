using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HP : MonoSingleton<HP>
{
    [Header("HP 슬라이더")]
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

    [Header("단추")]
    [SerializeField]
    Image[] clothesButtonImage;
    [SerializeField]
    int danchuIndex = 4;

    bool isDead = false;
    //죽었을 때 호출
    EventParam eventParam = new EventParam();

    //float fadeTime = 1f;

    // 단추 페이드 효과

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

    //다트윈으로

    private void Start()
    {
        hpSlider.value = whiteSlider.value = playerHP / maxHP;
    }

    void Update()
    {
        // 데미지 입히기
        UpdateSlider();
    }

    // 플레이어가 데미지 입었을 때 피 마이너스
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

    // HP 게이지 UI Update
    void UpdateSlider()
    {
        hpSlider.value = playerHP / maxHP;
        whiteSlider.value = Mathf.Lerp(whiteSlider.value, playerHP / maxHP, Time.deltaTime * sliderSpeed);
    }

    // 죽었을 때 실행
    void Dead()
    {
        playerHP = 0;
        ClothesIndexCheck();
        Invoke("ResetHP", 2f);
        isDead = false;
    }
    void ClothesIndexCheck()
    {
        danchuIndex--; //가진 단추 수 -1
        clothesButtonImage[danchuIndex].gameObject.SetActive(false);
    }
    //reset으로 함수
    void ResetHP()
    {
        sliderSpeed = 3f; // 슬라이더 스피드 원래대로
        whiteSlider.value = playerHP / maxHP; // 흰색 슬라이더 다시 채우기
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed); //서서히 참
        playerHP = 100; // HP도 초기화
    }

}
