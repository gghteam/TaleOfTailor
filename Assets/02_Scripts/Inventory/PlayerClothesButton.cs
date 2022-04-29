using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : MonoBehaviour
{
    [Header("����")]
    [SerializeField]
    GameObject[] clothesButton;
    [SerializeField]
    float buttonDistance = 5f;

    int danchuIndex = 4;

    // �ֿ� ���� ����
    int clothesButtonItemIndex = 0;

    // Enemy�� ��ġ
    Vector3 enemyVector = Vector3.zero;

    EventParam eventParam = new EventParam();

    private void Start()
    {
        // �׾��� �� �̺�Ʈ �ޱ� ���� ������
        EventManager.StartListening("DEAD", DropClothesButton);
    }
    private void Update()
    {
        // ���� ������ ���
        if (Input.GetKeyDown(KeyCode.R))
        {
            //���� Ȯ��
            if (clothesButtonItemIndex > 0) // �ִٸ�
            {
                clothesButtonItemIndex--; // ���� ���̱�
                if (clothesButtonItemIndex <= 0) // �������� ���� ���ٸ� �̹��� ����
                {
                    ClothesButtonOnOff(false);
                }
                EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam); // ���� ȸ��
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //�ε����� ��
        if (collision.collider.CompareTag("ENEMY"))
        {
            eventParam.intParam = 20; //������ �ޱ�
            EventManager.TriggerEvent("DAMAGE", eventParam); // ������ �Ծ��ٴ� �̺�Ʈ ��ȣ ������
            enemyVector = collision.transform.position; // ���� ��ġ �ޱ�
        }
        if (collision.collider.CompareTag("CLOTHESBUTTON"))
        {
            clothesButtonItemIndex++; // ���� ������ �ֿ��� �� ���� �÷��ֱ�
            collision.gameObject.SetActive(false);  // �ֿ� ���� �Ⱥ��̰� �ϱ�
            ClothesButtonOnOff(true); // ���� ������ â �̹��� Ű��
        }
    }

    // ���� ������ â ���� Ű��
   void ClothesButtonOnOff(bool isOpen)
    {
        // 2�� ������ �ε��� ��ȣ
        eventParam.intParam = 2;
        eventParam.boolParam = isOpen;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }

    //���� ����߸��� ���� (���� ����Ʈ�� ���� �Լ� �߰�)
    void DropClothesButton(EventParam eventParam)
    {
        //���� �����ְ� ����
        danchuIndex = eventParam.intParam;
        SetClothesTransform();
    }

    //������ ���� ���ϰ� ����
    void SetClothesTransform()
    {
        //���� ����
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
