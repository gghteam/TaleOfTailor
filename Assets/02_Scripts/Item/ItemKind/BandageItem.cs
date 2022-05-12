using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageItem : ItemManager
{
    [SerializeField, Header("붕대 아이템 갯수")]
    int bandageCount;
    [SerializeField, Header("붕대 아이템의 스테미나 증가량")]
    int plusStemina = 1;

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
        eventParam.itemParam = Item.BANDAGE;
        eventParam.intParam = bandageCount;
        //SteminaManager.Instance.PlusStemina(plusStemina);
        EventManager.TriggerEvent("ITEMTEXT", eventParam);
        BandageUseAnim();
    }

    protected override void ItemZero()
    {
        eventParam.boolParam = false;
        eventParam.itemParam = Item.BANDAGE;
        EventManager.TriggerEvent("ITEMHAVE", eventParam);
    }
    void BandageUseAnim()
    {
        eventParam.itemParam = Item.BANDAGE;
        EventManager.TriggerEvent("ITEMANIMPLAY", eventParam);
    }

}
