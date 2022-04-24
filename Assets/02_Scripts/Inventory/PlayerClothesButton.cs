using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : MonoBehaviour
{
    [SerializeField]
    GameObject clothesButton;

    //단추 위치 지정을 위한 이벤트
    EventParam eventParam = new EventParam();

    Vector3 enemyVector = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        //부딪혔을 때
        if (collision.collider.CompareTag("ENEMY"))
        {
            enemyVector = collision.transform.position;
            DropClothesButton();
        }
    }

    //단추 떨어뜨리기 실행
    void DropClothesButton()
    {
        clothesButton.transform.localPosition = new Vector3(0, 0.5f, 0);
        //단추 생성
        clothesButton.SetActive(true);
        //방향 구해주기
        SetClothesTransform();
    }

    //떨어질 방향 구하기
    void SetClothesTransform()
    {
        eventParam.vectorThreeParam = (transform.position - enemyVector).normalized;
        EventManager.TriggerEvent("Clothes_Direction", eventParam);
    }
}
