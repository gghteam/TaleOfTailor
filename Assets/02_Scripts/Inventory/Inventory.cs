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
    // ���� â ��ư ���� ǥ��
    [SerializeField]
    Image selectImage;

    bool isOpen = false;

    private void Update()
    {
        SettingTabOnOff();
    }

    // �κ��丮 ���� â ���� Ű��
    public void InvenOpen(int tab)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == tab)
            {
                //����â ǥ��
                selectImage.rectTransform.anchoredPosition = new Vector3(355 * (i+1), -59f, 0f);
                UIManager.Instance.UiOpen(invenTab[i]);

            }
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

}
