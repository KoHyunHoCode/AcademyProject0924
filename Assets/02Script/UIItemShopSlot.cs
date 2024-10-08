using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemShopSlot : MonoBehaviour
{
    // 역참조 : 나쁜방식
    // 해당 슬롯을 만들어낸 주체
    private Popup_ItemShop shopPopub;

    private Image icon;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemPrice;
    private TextMeshProUGUI tradeCountText;

    private Button leftBtn;
    private Button rightBtn;
    private Button maxBtn;

    //해당 슬롯 표현해야하는 대상 아이템 정보

    private InventoryItemData data;
    private int slotIndex;

    public int SlotIndex
    {
        get => slotIndex;
    }
    private int totalGold;

    public int TotalGold
    {
        get => totalGold;   
    }
    private int priceGold; // 단가 (1개의 가격)
    private int tradeMaxCount; // 거래할수 있는 최대 갯수.
    private int curCount; // 거래 대상이 되는 아이템의 갯수.
    private int itemID; // 테이블 id 값

    private GameObject obj;

    private void Awake()
    {
        obj = transform.Find("Icon").gameObject;
        if(obj != null)
        {
            if (!obj.TryGetComponent<Image>(out icon))
                Debug.Log("UIItemShopSlot.cs - Awake() - icon 참조실패");
        }
        obj = transform.Find("ItemName").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<TextMeshProUGUI>(out itemName))
                Debug.Log("UIItemShopSlot.cs - Awake() - itemName 참조실패");
        }
        obj = transform.Find("PriceText").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<TextMeshProUGUI>(out itemPrice))
                Debug.Log("UIItemShopSlot.cs - Awake() - itemPrice 참조실패");
        }
        obj = transform.Find("TradeCount").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<TextMeshProUGUI>(out tradeCountText))
                Debug.Log("UIItemShopSlot.cs - Awake() - tradecountText 참조실패");
        }
        obj = transform.Find("CountDown").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<Button>(out leftBtn))
                Debug.Log("UIItemShopSlot.cs - Awake() - leftBtn 참조실패");
            else
                leftBtn.onClick.AddListener(OnClick_DOWNBTN);    
        }
        obj = transform.Find("CountUP").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<Button>(out rightBtn))
                Debug.Log("UIItemShopSlot.cs - Awake() - itemPrice 참조실패");
            else
                rightBtn.onClick.AddListener(OnClick_UPBTN);
        }
        obj = transform.Find("MaxCount").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<Button>(out maxBtn))
                Debug.Log("UIItemShopSlot.cs - Awake() -maxBtn 참조실패");
            else 
                maxBtn.onClick.AddListener(OnClick_MaxBTN);
        }
    }
    public void OnClick_UPBTN()
    {
        if (curCount < tradeMaxCount)
            curCount++;
        tradeCountText.text = curCount.ToString();
        totalGold = curCount * priceGold;

        //todo 수치 변경 전달
        shopPopub.RefreshGold();
    }
    public void OnClick_DOWNBTN()
    {
        if (curCount > 0)
            curCount--;

        // UI  갱신
        tradeCountText.text = curCount.ToString();
        totalGold = curCount * priceGold;
        shopPopub.RefreshGold();
    }
    public void OnClick_MaxBTN()
    {
        curCount = tradeMaxCount;
        tradeCountText.text = curCount.ToString();
        totalGold = curCount * priceGold;
        // todo
        shopPopub.RefreshGold();
    }
    //슬롯이 최초로 생성이 될때
    public void CreateSlot(Popup_ItemShop shop, int index)
    {
        shopPopub = shop;
        slotIndex = index;
        gameObject.SetActive(false);
    }

    //판매, 구매할때
    public void RefreshSlot(InventoryItemData itemInfo)
    {
        gameObject.SetActive(true);
        itemID = itemInfo.itemID;
        tradeMaxCount = itemInfo.amount;
        curCount = 0;
        tradeCountText.text = curCount.ToString();

        if(!DataManager.Inst.GetItemData(itemID,out ItemData_Entity  itemData))
        {
            Debug.Log("UI ItemShopSlot.cs - RefreshSlot() - itemData 탐색실패");
            return;
        }

        icon.sprite = Resources.Load<Sprite>(itemData.iconlmg);
        icon.enabled = true;
        itemPrice.text = itemData.sellGold.ToString();
        priceGold = itemData.sellGold;
    }
    public void ClearSlot()
    {
        gameObject.SetActive(!false);
    }
    public bool GetSellCount(out int _sellltemID, out int _sellCount, out int sellGold)
    {
        _sellltemID = itemID;
        _sellCount = curCount;
        sellGold = totalGold;
        
        return true;
    }

    public bool GetBuyCount(out int _buyitemID, out int _buyCount, out int _buyGold)
    {
        _buyitemID = itemID;
        _buyCount = curCount;
        _buyGold = totalGold;
        return true;
    }
}
