using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // 전체적인 인벤토리 창
    [SerializeField]
    GameObject inventoryPanel;
    // 세부적인 인벤토리 내에 나뉜 창
    [SerializeField]
    GameObject[] invenTab;

    bool isOpen = false;
    
    // 인벤토리 내에 창 끄고 키기
   public void InvenOpen(int tab)
    {
        for(int i=0;i < 4; i++)
        {
            if(i == tab)
                UIManager.Instance.UiOpen(invenTab[i]);
            else
                UIManager.Instance.UiClose(invenTab[i]);
        }
    }

    // 인벤토리 끄고 키기
    private void SettingTabOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                UIManager.Instance.UiClose(inventoryPanel);
                isOpen = false;
            }
            else
            {
                UIManager.Instance.UiOpen(inventoryPanel);
                isOpen = true;
            }
        }
    }

    private void Update()
    {
        //계속 확인
        SettingTabOnOff();
    }
}
