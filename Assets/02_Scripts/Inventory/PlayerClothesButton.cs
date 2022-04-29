using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : MonoBehaviour
{
    [Header("단추")]
    [SerializeField]
    GameObject[] clothesButton;
    [SerializeField]
    float buttonDistance = 5f;

    int danchuIndex = 4;

    // 주운 단추 갯수
    int clothesButtonItemIndex = 0;

    // Enemy의 위치
    Vector3 enemyVector = Vector3.zero;

    EventParam eventParam = new EventParam();

    private void Start()
    {
        // 죽었을 때 이벤트 받기 단추 떨구기
        EventManager.StartListening("DEAD", DropClothesButton);
    }
    private void Update()
    {
        // 단추 아이템 사용
        if (Input.GetKeyDown(KeyCode.R))
        {
            //갯수 확인
            if (clothesButtonItemIndex > 0) // 있다면
            {
                clothesButtonItemIndex--; // 갯수 줄이기
                if (clothesButtonItemIndex <= 0) // 아이템이 이제 없다면 이미지 끄기
                {
                    ClothesButtonOnOff(false);
                }
                EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam); // 단추 회복
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //부딪혔을 때
        if (collision.collider.CompareTag("ENEMY"))
        {
            eventParam.intParam = 20; //데미지 받기
            EventManager.TriggerEvent("DAMAGE", eventParam); // 데미지 입었다는 이벤트 신호 보내기
            enemyVector = collision.transform.position; // 적의 위치 받기
        }
        if (collision.collider.CompareTag("CLOTHESBUTTON"))
        {
            clothesButtonItemIndex++; // 단추 아이템 주웠을 때 갯수 늘려주기
            collision.gameObject.SetActive(false);  // 주운 단추 안보이게 하기
            ClothesButtonOnOff(true); // 단추 아이템 창 이미지 키기
        }
    }

    // 단추 아이템 창 끄고 키기
   void ClothesButtonOnOff(bool isOpen)
    {
        // 2가 단추의 인덱스 번호
        eventParam.intParam = 2;
        eventParam.boolParam = isOpen;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }

    //단추 떨어뜨리기 실행 (나중 이펙트를 위해 함수 추가)
    void DropClothesButton(EventParam eventParam)
    {
        //방향 구해주고 세팅
        danchuIndex = eventParam.intParam;
        SetClothesTransform();
    }

    //떨어질 방향 구하고 세팅
    void SetClothesTransform()
    {
        //단추 생성
        Vector3 buttonPos = (transform.position - enemyVector).normalized;
        if(danchuIndex%2==0)
        {
            danchuIndex = danchuIndex / 2 - 1;
        }
        else
        {
            danchuIndex = (danchuIndex - 1) / 2-1;
        }
        clothesButton[danchuIndex].transform.localPosition = new Vector3(buttonPos.x * buttonDistance, 0.5f, buttonPos.z * buttonDistance);
        clothesButton[danchuIndex].gameObject.SetActive(true);
    }
}
