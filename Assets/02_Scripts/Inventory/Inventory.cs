using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum ItemContirion
{
    CLOTHES_BUTTON,
    NEEDLE,
    BANDAGE
}

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
    // ������ â �������̹���
    [SerializeField]
    Image[] itemImage;

    Image beforeItem;

    EventParam eventParam = new EventParam();

    Dictionary<string, GameObject> itemImages = new Dictionary<string, GameObject>();
    bool isOpen = false;


    private void Start()
    {
        EventManager.StartListening("ITEMHAVE", ItemHave);
    }
    
    private void Update()
    {
        SettingTabOnOff();
    }

    //ó�� ���۽� ��ųʸ��� ������ �ֱ�
    private void StartItemSet()
    {
        itemImages.Add("NEEDLE", itemImage[0].gameObject);
        itemImages.Add("BANDAGE", itemImage[1].gameObject);
        itemImages.Add("CLOTHESBUTTON", itemImage[2].gameObject);
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

    // ������ â �������̹��� Ű�� ����
    private void ItemImageOn(int item, bool isOpen)
    {
        Image obj = itemImage[item];
        if (isOpen)
        {
            obj.gameObject.SetActive(true);
            beforeItem = obj;
        }
        else
        {
            beforeItem.gameObject.SetActive(false);
        }
    }

    // ������ â ���� Ű�� �Լ� ����
    void ItemHave(EventParam eventParam)
    {
        ItemImageOn(eventParam.intParam, eventParam.boolParam);
    }
}
