using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleItem : ItemManager
{
    [SerializeField, Header("�ٴ� ������ ����")]
    int needleCount;
    [SerializeField, Header("�ٴ� ������ ���ݷ� ������")]
    int attackPower = 30;

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
        eventParam.itemParam = Item.NEEDLE;
        eventParam.intParam = needleCount;
        EventManager.TriggerEvent("ITEMTEXT", eventParam);
        eventParam.intParam = attackPower;
        //EventManager.TriggerEvent("PLUS_ATTACKPOWER", eventParam);
        NeedleUseAnim();
    }

    void NeedleUseAnim()
    {
        eventParam.itemParam = Item.NEEDLE;
        EventManager.TriggerEvent("ITEMANIMPLAY", eventParam);
    }


}
