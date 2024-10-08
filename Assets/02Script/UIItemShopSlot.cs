using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemShopSlot : MonoBehaviour
{
    // ������ : ���۹��
    // �ش� ������ ���� ��ü
    private Popup_ItemShop shopPopub;

    private Image icon;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemPrice;
    private TextMeshProUGUI tradeCountText;

    private Button leftBtn;
    private Button rightBtn;
    private Button maxBtn;

    //�ش� ���� ǥ���ؾ��ϴ� ��� ������ ����

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
    private int priceGold; // �ܰ� (1���� ����)
    private int tradeMaxCount; // �ŷ��Ҽ� �ִ� �ִ� ����.
    private int curCount; // �ŷ� ����� �Ǵ� �������� ����.
    private int itemID; // ���̺� id ��

    private GameObject obj;

    private void Awake()
    {
        obj = transform.Find("Icon").gameObject;
        if(obj != null)
        {
            if (!obj.TryGetComponent<Image>(out icon))
                Debug.Log("UIItemShopSlot.cs - Awake() - icon ��������");
        }
        obj = transform.Find("ItemName").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<TextMeshProUGUI>(out itemName))
                Debug.Log("UIItemShopSlot.cs - Awake() - itemName ��������");
        }
        obj = transform.Find("PriceText").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<TextMeshProUGUI>(out itemPrice))
                Debug.Log("UIItemShopSlot.cs - Awake() - itemPrice ��������");
        }
        obj = transform.Find("TradeCount").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<TextMeshProUGUI>(out tradeCountText))
                Debug.Log("UIItemShopSlot.cs - Awake() - tradecountText ��������");
        }
        obj = transform.Find("CountDown").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<Button>(out leftBtn))
                Debug.Log("UIItemShopSlot.cs - Awake() - leftBtn ��������");
            else
                leftBtn.onClick.AddListener(OnClick_DOWNBTN);    
        }
        obj = transform.Find("CountUP").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<Button>(out rightBtn))
                Debug.Log("UIItemShopSlot.cs - Awake() - itemPrice ��������");
            else
                rightBtn.onClick.AddListener(OnClick_UPBTN);
        }
        obj = transform.Find("MaxCount").gameObject;
        if (obj != null)
        {
            if (!obj.TryGetComponent<Button>(out maxBtn))
                Debug.Log("UIItemShopSlot.cs - Awake() -maxBtn ��������");
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

        //todo ��ġ ���� ����
        shopPopub.RefreshGold();
    }
    public void OnClick_DOWNBTN()
    {
        if (curCount > 0)
            curCount--;

        // UI  ����
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
    //������ ���ʷ� ������ �ɶ�
    public void CreateSlot(Popup_ItemShop shop, int index)
    {
        shopPopub = shop;
        slotIndex = index;
        gameObject.SetActive(false);
    }

    //�Ǹ�, �����Ҷ�
    public void RefreshSlot(InventoryItemData itemInfo)
    {
        gameObject.SetActive(true);
        itemID = itemInfo.itemID;
        tradeMaxCount = itemInfo.amount;
        curCount = 0;
        tradeCountText.text = curCount.ToString();

        if(!DataManager.Inst.GetItemData(itemID,out ItemData_Entity  itemData))
        {
            Debug.Log("UI ItemShopSlot.cs - RefreshSlot() - itemData Ž������");
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
