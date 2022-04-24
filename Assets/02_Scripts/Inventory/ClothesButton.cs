using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothesButton : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField]
    float clothesButtonSpeed;
    [SerializeField]
    float distance;

    //����
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

    //���� ����
    void SetDirectionButton(EventParam eventParam)
    {
        dropDirection = eventParam.vectorThreeParam;
    }

    //���� �������� �̵�
    void MoveClothesButton()
    {
        transform.position = Vector3.Slerp(transform.position, dropDirection * distance, 1f);
    }
}
