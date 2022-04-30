using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleItem : ItemManager
{
    [SerializeField, Header("�ٴ� ������ ����")]
    int needleCount;

    EventParam eventParam = new EventParam();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseItem();
        }
    }

    protected override void GetItem()
    {
        // ������ Ÿ�Կ��� ���� �ý���
    }

    protected override void ItemZero()
    {
        eventParam.boolParam = false;
        eventParam.itemParam = Item.NEEDLE;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }

    protected override void UseItem()
    {
        if (needleCount > 0)
        {
            needleCount--;
        }
        if (needleCount <= 0)
        {
            ItemZero();
        }
        eventParam.intParam = needleCount;
        eventParam.itemParam = Item.NEEDLE;
        EventManager.TriggerEvent("ITEMTEXT", eventParam);
    }
}
