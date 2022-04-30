using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : MonoBehaviour
{
    [SerializeField, Header("���� ������ �� �÷��̾���� �Ÿ�")]
    float buttonDistance = 5f;

    [SerializeField, Header("����")]
    GameObject[] clothesButton;

    int danchuIndex = 4;    // ���� ��� ����
    int clothesButtonItemCount = 0;    // �ֿ� ���� ����

    Vector3 enemyVector = Vector3.zero;    // Enemy�� ��ġ

    EventParam eventParam = new EventParam();

    private void Start()
    {
        // �׾��� �� �̺�Ʈ �ޱ� ( ���� ������ )
        EventManager.StartListening("DEAD", DropClothesButton);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))   // ���� ������ ���
        {
            UseItem();
        }
    }

    // ������ ���
    void UseItem()
    {
        if (clothesButtonItemCount > 0) // ������ 0�� �ƴϸ� ���
        {
            clothesButtonItemCount--;
            ClothesButtonCount();
            // �������� ���� ���ٸ� �̹��� ����
            if (clothesButtonItemCount <= 0) ClothesButtonOnOff(false);
            EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam); // ���� ȸ��
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ENEMY"))
        {
            eventParam.intParam = 20; // ���ϴ� ������ ������ �ޱ�
            EventManager.TriggerEvent("DAMAGE", eventParam); // ������ �Ծ��ٴ� �̺�Ʈ ��ȣ ������
            enemyVector = collision.transform.position; // ���� ��ġ �ޱ�
        }
        if (collision.collider.CompareTag("CLOTHESBUTTON")) // ������ ���� �ֿ��� ��
        {
            clothesButtonItemCount++;
            ClothesButtonCount();
            collision.gameObject.SetActive(false);  // �ֿ� ���� �Ⱥ��̰� �ϱ�
            ClothesButtonOnOff(true); // ���� ������ â �̹��� Ű��
        }
    }

    void ClothesButtonCount()
    {
        eventParam.intParam = clothesButtonItemCount;
        eventParam.itemParam = Item.CLOTHES_BUTTON;
         EventManager.TriggerEvent("ITEMTEXT", eventParam); //���� ǥ��
    }

    // ���� ������ â ���� Ű��
   void ClothesButtonOnOff(bool isOpen)
    {
        eventParam.itemParam = Item.CLOTHES_BUTTON;
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
