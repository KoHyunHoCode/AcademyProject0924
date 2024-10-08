using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_ItemShop : MonoBehaviour, IPopup
{
    [SerializeField]
    private GameObject slotPrefab; // 슬롯 프리팹
    [SerializeField]
    private RectTransform sellRect; // 사용자 -> 상점에
    [SerializeField]
    private RectTransform buyRect; // 상점 -> 사용자

    private TextMeshProUGUI balanceText; // 유저의 전재산 (골드)
    private TextMeshProUGUI amountText; // 거래하기 위한 총 금액
    private DataInventory inventory; // 인벤토리 참조 변수
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
                Debug.Log("탐색성공");

                
        }
        obj = GameObject.Find("Amount");
        if (obj != null)
            if (obj.TryGetComponent<TextMeshProUGUI>(out amountText))
                Debug.Log("성공");

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

        //각 탭에 슬롯 생성
        inventory = DataManager.Inst.invenData;
        for(int  i = 0;  i < inventory.MAXITEMCOUNT; i++)
        {
            obj = Instantiate(slotPrefab, sellRect);
            shopSlot = obj.GetComponent<UIItemShopSlot>();
            shopSlot.CreateSlot(this, i);
            obj.name = "SellSlot_" + i;
            sellSlotList.Add(shopSlot);



        }
        // 상점 => 캐릭터  빨 , 파, 보라, 녹색 
        for(int i = 0; i  < 4; i++)
        {
            obj = Instantiate(slotPrefab, buyRect);
            shopSlot = obj.GetComponent <UIItemShopSlot>();
            shopSlot.CreateSlot(this, i);
            obj.name = "BuySlot_" + i;
            buySlotlist.Add(shopSlot);
        }

        
    }
    //재련소 콜바이 레퍼런스 콜바이 밸류


    // 플레이어 아이템 -> 상점에 판매 탭
    public void OnClick_SellTab()
    {
        RefreshSellData();
        sellView.SetActive(true);
        buyView.SetActive(false);
    }
    // 상점 아이템을 플레이어가 구매 탭
    public void OnClick_BuyTab()
    {
        RefreshBuyData();
        sellView.SetActive(false);
        buyView.SetActive(true);
    }

    //최종거래하는 버튼
    // 현재 열린 페이지에 대한 거래 처리
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
                inventory.DeleteItem(data); // 실제 데이터에서 제거
            }
        }
        else // 상점 -> 플레이어
        {
            int buyTotalGold = 0;
            

            // 유저가 만들어낸 목록(어떤 물약을 몇개 구매할지?) 을 검사해서 총 구매 금액 
            for(int i = 0; i < 4; i ++)
            {
                buySlotlist[i].GetBuyCount(out int id, out int count, out int gold);
                buyTotalGold += gold;
            }

            if(buyTotalGold <= DataManager.Inst.PlayerGold) // 잔고 충분
            {
                DataManager.Inst.PlayerGold -= buyTotalGold;
                for(int i = 0;  i < 4; i++)
                {
                    buySlotlist[i].GetBuyCount(out int id, out int count, out int gold);
                    if(count > 0) // 최소 1개이상 거래가 이루어진 경우
                    {
                        InventoryItemData data = new InventoryItemData();
                        data.itemID = id;
                        data.amount = count;
                        inventory.AddItem(data); // 실제 아이템 지급
                    }
                }
            }
            else // 잔고 부족
            {
                Debug.Log("잔고 부족");
            }
        }
        // 해당 팝업 갱신
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


    //팝업창 전체를 갱신
    private void RefreshPopup()
    {
        balanceText.text = DataManager.Inst.PlayerGold.ToString();

        dataList= inventory.GetItemList();

        RefreshSellData(); // 판매탭 갱신
        tradeTotalGold = 0;
        amountText.text = tradeTotalGold.ToString();

    }


    List<InventoryItemData> dataList;
    int tradeTotalGold;
    private void RefreshSellData()
    {
        // 아이템 정보
        dataList = inventory.GetItemList();
        for(int i = 0; i < inventory.MAXITEMCOUNT; i++)
        {
            if(i < inventory.CURITEMCOUNT && -1 < dataList[i].itemID)
            {
                sellSlotList[i].RefreshSlot(dataList[i]);

            }
            else // 빈슬롯
                sellSlotList[i].ClearSlot(); //화면 표시 x

            

            
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
