using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageItem : ItemManager
{
    [SerializeField, Header("붕대 아이템 갯수")]
    int bandageCount;

    EventParam eventParam = new EventParam();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem();
        }
    }

    protected override void GetItem()
    {
        // 프로토타입에는 없음
    }

    protected override void UseItem()
    {
        if (bandageCount > 0)
        {
            bandageCount--;
        }
        if (bandageCount <= 0)
        {
            ItemZero();
        }
        eventParam.intParam = bandageCount;
        eventParam.itemParam = Item.BANDAGE;
        EventManager.TriggerEvent("ITEMTEXT", eventParam);
    }

    protected override void ItemZero()
    {
        eventParam.boolParam = false;
        eventParam.itemParam = Item.BANDAGE;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }
}
