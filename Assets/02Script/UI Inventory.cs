using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private DataInventory inventory;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private GameObject slotprefab;
    [SerializeField]
    private RectTransform contentTrans;

    GameObject obj;
    List<InventoryItemData> itemDataList; // 포인터 (참조형 변수)

    // todo 슬롯관리
    private List<UIIneventorySlot> slotList = new List<UIIneventorySlot>();
    private UIIneventorySlot inventorySlot;

    private bool isinit = false;

    // 게임 시작할때 인벤토리 갯수만큼 슬롯 생성
    private void InitSlots()
    {
        
        inventory = DataManager.Inst.invenData; // 참조
        for(int i = 0; i< inventory.MAXITEMCOUNT; i++)
        {
            obj = Instantiate(slotprefab, contentTrans);
            inventorySlot = obj. GetComponent<UIIneventorySlot>();
            inventorySlot.SLOTINDEX = i;
            inventorySlot.gameObject.name = inventorySlot.name + i + 1;
            slotList.Add(inventorySlot);
        }
    }
    // 슬롯을 최신의 정보를 통해서 다시 그리는 메소드
    public void RefreshIcon()
    {
        if(!isinit)
            InitSlots();
        inventory = DataManager.Inst. invenData;// 재참조

        itemDataList = inventory.GetItemList();
        inventory.CURITEMCOUNT = itemDataList.Count;

        // 18 개
        for(int i = 0; i < inventory.MAXITEMCOUNT; i++)
        {
            if ( i  < inventory.CURITEMCOUNT && -1 < itemDataList[i].itemID )
            {
                slotList[i].DrawItem(itemDataList[i]);
            }
            else //보유 가짓수보다 큰 index가진 슬롯들은 모두 정보를 비운다.
            {
                slotList[i].ClearSlot();

            }
            slotList[i].SelectSlot(false); // 선택 프레임 비활성화.
        }
    }
}
