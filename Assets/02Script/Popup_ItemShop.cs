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
        Debug.Log("�����ۼ� Ŭ����");
    }

    public void PopupOpen()
    {
        Debug.Log("�����ۼ� ����");
    }
}
