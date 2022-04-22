using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveNsj : MonoBehaviour
{
    [SerializeField]
    GameObject clothesButton;

    //���� ��ġ ������ ���� �̺�Ʈ
    EventParam eventParam = new EventParam();

    Vector3 enemyVector = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        //�ε����� ��
        if (collision.collider.CompareTag("ENEMY"))
        {
            enemyVector = collision.transform.position;
            DropClothesButton();
        }
    }

    //���� ����߸��� ����
    void DropClothesButton()
    {
        clothesButton.transform.position = transform.position;
        //���� ����
        clothesButton.SetActive(true);
        //���� �����ֱ�
        SetClothesTransform();
    }

    //������ ���� ���ϱ�
    void SetClothesTransform()
    {
        eventParam.vectorParam = (transform.position - enemyVector).normalized;
        EventManager.TriggerEvent("Clothes_Direction", eventParam);
    }
}
