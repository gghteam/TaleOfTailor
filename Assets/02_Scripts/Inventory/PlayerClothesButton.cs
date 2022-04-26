using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : MonoBehaviour
{
    [Header("����")]
    [SerializeField]
    GameObject clothesButton;
    [SerializeField]
    float buttonDistance = 5f;

    //���� ��ġ ������ ���� �̺�Ʈ
    Vector3 enemyVector = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        //�ε����� ��
        if (collision.collider.CompareTag("ENEMY"))
        {
            HP.Instance.DamageSlider(20);
            //���� ����߸���
            if (HP.Instance.playerHP <= 0)
            {
                enemyVector = collision.transform.position;
                DropClothesButton();
            }
        }
    }

    //���� ����߸��� ���� (���� ����Ʈ�� ���� �Լ� �߰�)
    void DropClothesButton()
    {
        //���� �����ְ� ����
        SetClothesTransform();
    }

    //������ ���� ���ϰ� ����
    void SetClothesTransform()
    {
        //���� ����
        Vector3 buttonPos = (transform.position - enemyVector).normalized;
        clothesButton.transform.localPosition = new Vector3(buttonPos.x*buttonDistance, 0.5f, buttonPos.z*buttonDistance);
        clothesButton.SetActive(true);
    }
}
