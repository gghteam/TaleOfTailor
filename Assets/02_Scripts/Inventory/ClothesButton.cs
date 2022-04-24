using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothesButton : MonoBehaviour
{
    [Header("단추 정보")]
    [SerializeField]
    float clothesButtonSpeed;
    [SerializeField]
    float distance;

    //방향
    Vector3 dropDirection = Vector3.zero;
    Collider collider;
    bool isMove = true;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        EventManager.StartListening("Clothes_Direction", SetDirectionButton);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("Clothes_Direction", SetDirectionButton);
    }
    private void OnEnable()
    {
        isMove = true;
        collider.isTrigger = false;
        MoveClothesButton();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (isMove) return;
            collider.isTrigger = true;
            gameObject.SetActive(false);
        }
    }

    //단추 세팅
    void SetDirectionButton(EventParam eventParam)
    {
        dropDirection = eventParam.vectorThreeParam;
    }

    //단추 떨어지는 이동
    void MoveClothesButton()
    {
        transform.position = Vector3.Slerp(transform.position, dropDirection * distance, 1f);
    }
}
