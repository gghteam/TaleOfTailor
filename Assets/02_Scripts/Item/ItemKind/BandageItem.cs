using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageItem : ItemManager
{
    [SerializeField, Header("�ش� ������ ����")]
    int bandageCount;
    [SerializeField, Header("�ش� �������� ���׹̳� ������")]
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
        // ������Ÿ�Կ��� ����
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
