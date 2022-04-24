using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : MonoBehaviour
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
        clothesButton.transform.localPosition = new Vector3(0, 0.5f, 0);
        //���� ����
        clothesButton.SetActive(true);
        //���� �����ֱ�
        SetClothesTransform();
    }

    //������ ���� ���ϱ�
    void SetClothesTransform()
    {
        eventParam.vectorThreeParam = (transform.position - enemyVector).normalized;
        EventManager.TriggerEvent("Clothes_Direction", eventParam);
    }
}
