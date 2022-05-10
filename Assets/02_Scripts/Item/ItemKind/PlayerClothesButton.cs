using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothesButton : ItemManager
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
    private void OnDestroy()
    {
        EventManager.StopListening("DEAD", DropClothesButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))   // ���� ������ ���
        {
            UseItem();
        }
    }

    // ������ ���
    protected override void UseItem()
    {
        if (clothesButtonItemCount > 0) // ������ 0�� �ƴϸ� ���
        {
            clothesButtonItemCount--;
            ClothesButtonCount();
            eventParam.itemParam = Item.CLOTHES_BUTTON;
            EventManager.TriggerEvent("ITEMANIMPLAY", eventParam);
            // �������� ���� ���ٸ� �̹��� ����
            if (clothesButtonItemCount >= 0)
            {
                ItemZero();
                EventManager.TriggerEvent("PLUSCLOTHESBUTTON", eventParam); // ���� ȸ��
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ENEMY"))
        {
            eventParam.intParam = 200; // ���ϴ� ������ ������ �ޱ�
            eventParam.stringParam = "PLAYER";
            EventManager.TriggerEvent("DAMAGE", eventParam); // ������ �Ծ��ٴ� �̺�Ʈ ��ȣ ������
            enemyVector = collision.transform.position; // ���� ��ġ �ޱ�
        }
        if (collision.collider.CompareTag("CLOTHESBUTTON")) // ������ ���� �ֿ��� ��
        {
            collision.gameObject.SetActive(false);  // �ֿ� ���� �Ⱥ��̰� �ϱ�
            GetItem();
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
        if (danchuIndex % 2 == 0) // ���߰� ���ɰ����� ����
        {
            danchuIndex = danchuIndex / 2 - 1;
        }
        else // ���߰� ���ɰ����� �ִ�
        {
            danchuIndex = (danchuIndex - 1) / 2 - 1; 
        }
        clothesButton[danchuIndex].transform.localPosition = new Vector3(buttonPos.x * buttonDistance, 0.5f, buttonPos.z * buttonDistance);
        clothesButton[danchuIndex].gameObject.SetActive(true);
    }

    protected override void GetItem()
    {
        clothesButtonItemCount++;
        ClothesButtonCount();
        ClothesButtonOnOff(true); // ���� ������ â �̹��� Ű��
    }

    protected override void ItemZero()
    {
        ClothesButtonOnOff(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BOSS"))
        {
            GetItem();
        }
    }
}
