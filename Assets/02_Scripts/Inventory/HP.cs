using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    [Header("HP")]
    [SerializeField]
    float maxHP = 3000;
    [SerializeField]
    public float playerHP = 3000;
    [SerializeField, Header("HP 슬라이더 속도")]
    float sliderSpeed = 5f;

    [Header("단추 갯수")]
    [SerializeField]
    int danchuCount;
    [SerializeField]
    int maxDanchuCount;

    [Header("HP 슬라이더")]
    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Slider whiteSlider;

    [Header("단추 UI")]
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
        hpSlider.value = whiteSlider.value = playerHP / maxHP;
    }
    private void OnDestroy()
    {
        EventManager.StopListening("PLUSCLOTHESBUTTON", PlusClothesButton);
        EventManager.StopListening("DAMAGE", DamageSlider);
    }

    void Update()
    {
        // 데미지 입히기
        UpdateSlider();
    }

    // 단추 리셋
    void ResetClothesButton()
    {
        ClothesButtonOnOff(maxDanchuCount);
    }

    // 플레이어가 데미지 입었을 때 피 마이너스
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

    // HP 게이지 UI Update
    void UpdateSlider()
    {
        if (isDead) return;
        hpSlider.value = playerHP / maxHP;
        whiteSlider.value = Mathf.Lerp(whiteSlider.value, playerHP / maxHP, Time.deltaTime * sliderSpeed);
    }

    // 죽었을 때 실행
    void Dead()
    {
        isDead = true;
        if (!isHalf)
        {
            eventParam.intParam = danchuCount;
            EventManager.TriggerEvent("DEAD", eventParam);
        }
        MinusClothesButton(isHalf ? 1 : 2);
        Invoke("ResetPlayerHP", 2f);
        isDead = false;
    }

    // 단추 추가와 마이너슨
    void MinusClothesButton(int minus)
    {
        danchuCount -= minus; // 단추 수 빼기
        if (danchuCount <= 0) Debug.Log("죽음");
        else ClothesButtonOnOff(danchuCount);
    }
    void PlusClothesButton(EventParam eventParam)
    {
        danchuCount++; //가진 단추 수 +1
        ClothesButtonOnOff(danchuCount);
    }

    //UI 단추 끄고 키기
    void ClothesButtonOnOff(int index)
    {
        int cIndex = 0;
        isHalf = index % 2 == 0 ? false : true;
        if (index % 2 != 0) cIndex = index - 1;
        cIndex = index / 2 - 1;

        //전부 끄기
        for (int i = 0; i < 4; i++)
            clothesButtonImage[i].gameObject.SetActive(false);
        //인덱스까지만 키기
        for (int i = 0; i < cIndex + 1; i++)
            clothesButtonImage[i].gameObject.SetActive(true);

        Vector3 pos = clothesButtonImage[cIndex].rectTransform.anchoredPosition;

        if (isHalf) pos.x += 21f;
        else pos.x -= 21f;
        halfButtonImage.gameObject.SetActive(isHalf);
        halfButtonImage.rectTransform.anchoredPosition = pos;

    }

    // HP 리셋 함수
    void ResetPlayerHP()
    {
        whiteSlider.value = playerHP / maxHP; // 흰색 슬라이더 다시 채우기
        hpSlider.value = Mathf.Lerp(hpSlider.value, 1, Time.deltaTime * sliderSpeed + 2); //서서히 참
        playerHP = maxHP; // HP도 초기화
    }

}
