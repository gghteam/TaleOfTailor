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
    // 아이템 창 아이템이미지
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

    //처음 시작시 딕셔너리에 아이템 넣기
    private void StartItemSet()
    {
        itemImages.Add("NEEDLE", itemImage[0].gameObject);
        itemImages.Add("BANDAGE", itemImage[1].gameObject);
        itemImages.Add("CLOTHESBUTTON", itemImage[2].gameObject);
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

    // 아이템 창 아이템이미지 키고 끄기
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

    // 아이템 창 끄고 키기 함수 실행
    void ItemHave(EventParam eventParam)
    {
        ItemImageOn(eventParam.intParam, eventParam.boolParam);
    }
}
