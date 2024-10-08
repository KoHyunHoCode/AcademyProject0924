using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_ItemShop : MonoBehaviour, IPopup
{
    [SerializeField]
    private GameObject slotPrefab; // ���� ������
    [SerializeField]
    private RectTransform sellRect; // ����� -> ������
    [SerializeField]
    private RectTransform buyRect; // ���� -> �����

    private TextMeshProUGUI balanceText; // ������ ����� (���)
    private TextMeshProUGUI amountText; // �ŷ��ϱ� ���� �� �ݾ�
    private DataInventory inventory; // �κ��丮 ���� ����
    private UIItemShopSlot shopSlot;
    

    private List<UIItemShopSlot> sellSlotList = new List<UIItemShopSlot>();
    private List<UIItemShopSlot> buySlotlist = new List<UIItemShopSlot>();

    private GameObject sellView;
    private GameObject buyView;

    private GameObject obj;

    private Button sellTabBtn;
    private Button buyTabBtn;
    private Button applyBtn;

    private void Awake()
    {
        Invoke("TEST", 0.1f);
    }
    

    private void TEST()
    {
        obj = GameObject.Find("Balance");
        if(obj != null)
        {
            if (obj.TryGetComponent<TextMeshProUGUI>(out balanceText))
                Debug.Log("Ž������");

                
        }
        obj = GameObject.Find("Amount");
        if (obj != null)
            if (obj.TryGetComponent<TextMeshProUGUI>(out amountText))
                Debug.Log("����");

        sellView = GameObject.Find("SellTab");
        buyView = GameObject.Find("BuyTab");

        obj = GameObject.Find("SellTapBtn");
        sellTabBtn = obj.GetComponent<Button>();
        sellTabBtn.onClick.AddListener(OnClick_SellTab);
        
        obj = GameObject.Find("BuyTabBtn");
        buyTabBtn = obj.GetComponent<Button>();
        buyTabBtn.onClick.AddListener(OnClick_BuyTab);

        obj = GameObject.Find("ApplyBtn");
        applyBtn = obj.GetComponent<Button>();
        applyBtn.onClick.AddListener(OnClick_Apply);

        //�� �ǿ� ���� ����
        inventory = DataManager.Inst.invenData;
        for(int  i = 0;  i < inventory.MAXITEMCOUNT; i++)
        {
            obj = Instantiate(slotPrefab, sellRect);
            shopSlot = obj.GetComponent<UIItemShopSlot>();
            shopSlot.CreateSlot(this, i);
            obj.name = "SellSlot_" + i;
            sellSlotList.Add(shopSlot);



        }
        // ���� => ĳ����  �� , ��, ����, ��� 
        for(int i = 0; i  < 4; i++)
        {
            obj = Instantiate(slotPrefab, buyRect);
            shopSlot = obj.GetComponent <UIItemShopSlot>();
            shopSlot.CreateSlot(this, i);
            obj.name = "BuySlot_" + i;
            buySlotlist.Add(shopSlot);
        }

        
    }
    //��ü� �ݹ��� ���۷��� �ݹ��� ���


    // �÷��̾� ������ -> ������ �Ǹ� ��
    public void OnClick_SellTab()
    {
        RefreshSellData();
        sellView.SetActive(true);
        buyView.SetActive(false);
    }
    // ���� �������� �÷��̾ ���� ��
    public void OnClick_BuyTab()
    {
        RefreshBuyData();
        sellView.SetActive(false);
        buyView.SetActive(true);
    }

    //�����ŷ��ϴ� ��ư
    // ���� ���� �������� ���� �ŷ� ó��
    public void OnClick_Apply()
    {
        if(sellView.activeSelf)
        {
            for(int i = inventory.CURITEMCOUNT - 1; i >=0; i--)
            {
                sellSlotList[i].GetSellCount(out int id, out int count, out int gold);

                DataManager.Inst.PlayerGold += gold;
                InventoryItemData data = new InventoryItemData();
                data.itemID = id;
                data.amount = count;
                inventory.DeleteItem(data); // ���� �����Ϳ��� ����
            }
        }
        else // ���� -> �÷��̾�
        {
            int buyTotalGold = 0;
            

            // ������ ���� ���(� ������ � ��������?) �� �˻��ؼ� �� ���� �ݾ� 
            for(int i = 0; i < 4; i ++)
            {
                buySlotlist[i].GetBuyCount(out int id, out int count, out int gold);
                buyTotalGold += gold;
            }

            if(buyTotalGold <= DataManager.Inst.PlayerGold) // �ܰ� ���
            {
                DataManager.Inst.PlayerGold -= buyTotalGold;
                for(int i = 0;  i < 4; i++)
                {
                    buySlotlist[i].GetBuyCount(out int id, out int count, out int gold);
                    if(count > 0) // �ּ� 1���̻� �ŷ��� �̷���� ���
                    {
                        InventoryItemData data = new InventoryItemData();
                        data.itemID = id;
                        data.amount = count;
                        inventory.AddItem(data); // ���� ������ ����
                    }
                }
            }
            else // �ܰ� ����
            {
                Debug.Log("�ܰ� ����");
            }
        }
        // �ش� �˾� ����
        RefreshPopup();
    }



    
    
    

    

    public void PopupClose()
    {
        gameObject.LeanScale(Vector3.zero, 0.3f).setEase(LeanTweenType.easeInOutElastic);
    }

    public void PopupOpen()
    {
        RefreshPopup();
        gameObject.LeanScale(Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutElastic);
    }


    //�˾�â ��ü�� ����
    private void RefreshPopup()
    {
        balanceText.text = DataManager.Inst.PlayerGold.ToString();

        dataList= inventory.GetItemList();

        RefreshSellData(); // �Ǹ��� ����
        tradeTotalGold = 0;
        amountText.text = tradeTotalGold.ToString();

    }


    List<InventoryItemData> dataList;
    int tradeTotalGold;
    private void RefreshSellData()
    {
        // ������ ����
        dataList = inventory.GetItemList();
        for(int i = 0; i < inventory.MAXITEMCOUNT; i++)
        {
            if(i < inventory.CURITEMCOUNT && -1 < dataList[i].itemID)
            {
                sellSlotList[i].RefreshSlot(dataList[i]);

            }
            else // �󽽷�
                sellSlotList[i].ClearSlot(); //ȭ�� ǥ�� x

            

            
        }
    }
    private void RefreshBuyData()
    {
        for(int i = 0; i < 4; i++)
        {
            InventoryItemData item = new InventoryItemData();
            item.itemID = 2001001 + i;
            item.amount = 999;
            buySlotlist[i].RefreshSlot(item);
        }
    }

    private void CalculateGold()
    {
        tradeTotalGold = 0;
        if(sellView.activeSelf)
        {
            for(int i = 0; i< sellSlotList.Count; i++)
            {
                if (sellSlotList[i].isActiveAndEnabled) ;
                    tradeTotalGold += sellSlotList[i].TotalGold;
            }
        }
        else
        {

        }
    }

    public void RefreshGold()
    {
        CalculateGold();
        amountText.text = tradeTotalGold.ToString();
    }

    
}
