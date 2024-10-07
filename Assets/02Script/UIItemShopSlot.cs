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
    private int pricegold;
    private int tradeMaxCount;
    private int curCount;
    private int itemID;

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

    }
    public void OnClick_DOWNBTN()
    {

    }
    public void OnClick_MaxBTN()
    {

    }

    public void CreateSlot(Popup_ItemShop shop, int index)
    {

    }
    public void RefreshSlot(InventoryItemData itemInfo)
    {

    }
    public void ClearSlot()
    {

    }
    //public bool GetSellCount(out int _sellltemID, out int _sellCount, out int sellGold)
    //{
    //    return true;
    //}

    //public bool GetBuyCount(out int _buyitemID, out int _buyCount, out int _buyGold)
    //{
    //    return true;
    //}
}
