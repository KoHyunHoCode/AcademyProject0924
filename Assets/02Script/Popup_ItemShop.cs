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

    public void OnClick_SellTab()
    {

    }

    public void OnClick_BuyTab()
    {

    }

    public void OnClick_Apply()
    {

    }

    public void PopupClose()
    {
        Debug.Log("아이템샵 클로즈");
    }

    public void PopupOpen()
    {
        Debug.Log("아이템샵 오픈");
    }
}
