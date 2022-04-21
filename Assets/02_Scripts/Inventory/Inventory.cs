using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("�κ��丮 â")]
    // ��ü���� �κ��丮 â
    [SerializeField]
    GameObject inventoryPanel;
    // �������� �κ��丮 ���� ���� â
    [SerializeField]
    GameObject[] invenTab;
   
    //[SerializeField]
    //GameObject selectImage;

    bool isOpen = false;
    
    // �κ��丮 ���� â ���� Ű��
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

    // �κ��丮 ���� Ű��
    private void SettingTabOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                UIManager.Instance.UiClose(inventoryPanel);
            }
            else
            {
                UIManager.Instance.UiOpen(inventoryPanel);
            }
            isOpen = !isOpen;
        }
    }

    private void Update()
    {
        //��� Ȯ��
        SettingTabOnOff();
    }

    void SelectTabEffect()
    {

    }
}
