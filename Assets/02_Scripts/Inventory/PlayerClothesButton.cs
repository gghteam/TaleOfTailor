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

    int clothesButtonItemIndex = 0;

    Vector3 enemyVector = Vector3.zero;

    //���� ��ġ ������ ���� �̺�Ʈ
    EventParam eventParam = new EventParam();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //���� Ȯ��
            if (clothesButtonItemIndex > 0)
            {
                clothesButtonItemIndex--;
                if (clothesButtonItemIndex <= 0)
                {
                    ClothesItemHave(false);
                }
                EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam);
            }
        }
    }

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
        if (collision.collider.CompareTag("CLOTHESBUTTON"))
        {
            clothesButtonItemIndex++;
            collision.gameObject.SetActive(false);
            ClothesItemHave(true);
        }
    }

    // ���� ������ â ���� Ű��
    void ClothesItemHave(bool isOpen)
    {
        Inventory.Instance.ItemImageOn(2, isOpen);
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
        clothesButton.transform.localPosition = new Vector3(buttonPos.x * buttonDistance, 0.5f, buttonPos.z * buttonDistance);
        clothesButton.SetActive(true);
    }
}
