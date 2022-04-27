using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : MonoBehaviour
{
    [Header("단추")]
    [SerializeField]
    GameObject clothesButton;
    [SerializeField]
    float buttonDistance = 5f;

    //단추 위치 지정을 위한 이벤트
    Vector3 enemyVector = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        //부딪혔을 때
        if (collision.collider.CompareTag("ENEMY"))
        {
            HP.Instance.DamageSlider(20);
            //단추 떨어뜨리기
            if (HP.Instance.playerHP <= 0)
            {
                enemyVector = collision.transform.position;
                DropClothesButton();
            }
        }
    }

    //단추 떨어뜨리기 실행 (나중 이펙트를 위해 함수 추가)
    void DropClothesButton()
    {
        //방향 구해주고 세팅
        SetClothesTransform();
    }

    //떨어질 방향 구하고 세팅
    void SetClothesTransform()
    {
        //단추 생성
        Vector3 buttonPos = (transform.position - enemyVector).normalized;
        clothesButton.transform.localPosition = new Vector3(buttonPos.x*buttonDistance, 0.5f, buttonPos.z*buttonDistance);
        clothesButton.SetActive(true);
    }
}
