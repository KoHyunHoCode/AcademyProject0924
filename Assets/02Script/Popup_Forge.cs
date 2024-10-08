using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_Forge : MonoBehaviour, IPopup
{

    [SerializeField]
    private GameObject forgeSlotPrefab;
    [SerializeField]
    private RectTransform contentRect;

    [SerializeField]
    private TextMeshProUGUI balanceTest; // 유저 보유 잔액
    [SerializeField]
    private TextMeshProUGUI enchantPrice; // 강화 비용
    [SerializeField]
    private TextMeshProUGUI enchantLevel; // 현재 강화레벨 
    [SerializeField]
    private TextMeshProUGUI echantAfterLevel; // 성공햇을시 레벨
    [SerializeField]
    private Button tryEnchantBTN;
    [SerializeField]
    private Image icon;

    List<UIForgeSlot> forgeSlotList = new List<UIForgeSlot> ();
    private DataInventory inventory;

    InventoryItemData selectItem;
    List<InventoryItemData> dataList; // *중요*
    ItemData_Entity data_Entity;
    UIForgeSlot slot;
    GameObject obj;


    private void Awake()
    {
        tryEnchantBTN.onClick.AddListener(OnClick_Apply);
        InitPopup();
    }
    public void OnClick_Apply()
    {
        if(TryEnchant()) // 강화 성공
        {
            selectItem.itemID++; // 공격력 정보 변경
            DataManager.Inst.invenData.UpdateItemInfo(selectItem); // 데이터 변경 갱신
            SelectItem(selectItem); // 재련소 창에서 선택된 아이템 유지
        }
    }

    private void InitPopup() // 각각 슬롯 생성.
    {

    }

    //유저가 가지고 잇는 아이템들 중에서 
    // 강화를 할 수 있는 아이템만 남김, 물약등을 표시가 되지 않도록 제외 시켜준후에 표기
    //여기에서 버그 생성
    //참조형 변수. 일반형 변수
    private void RefreshData()
    {

    }
    public void SelectItem(InventoryItemData itemData)
    {

    }
    private bool CanEnchant()
    {
        return false;
    }

    private bool TryEnchant()
    {
        return false;
    }
    
    
    public void PopupClose()
    {
        Debug.Log("대장간 클로즈");
    }

    public void PopupOpen()
    {
        Debug.Log("대장간 오픈");
    }

}
