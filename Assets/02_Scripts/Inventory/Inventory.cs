using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("인벤토리 창")]
    // 전체적인 인벤토리 창
    [SerializeField]
    GameObject inventoryPanel;
    // 세부적인 인벤토리 내에 나뉜 창
    [SerializeField]
    GameObject[] invenTab;
    // 선택 창 버튼 위에 표시
    [SerializeField]
    Image selectImage;

    bool isOpen = false;

    private void Update()
    {
        SettingTabOnOff();
    }

    // 인벤토리 내에 창 끄고 키기
    public void InvenOpen(int tab)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == tab)
            {
                //선택창 표시
                selectImage.rectTransform.anchoredPosition = new Vector3(355 * (i+1), -59f, 0f);
                UIManager.Instance.UiOpen(invenTab[i]);

            }
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
            }
            else
            {
                UIManager.Instance.UiOpen(inventoryPanel);
            }
            isOpen = !isOpen;
        }
    }

}
