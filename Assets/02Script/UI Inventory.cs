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
    List<InventoryItemData> itemDataList; // ������ (������ ����)

    // todo ���԰���
    private List<UIIneventorySlot> slotList = new List<UIIneventorySlot>();
    private UIIneventorySlot inventorySlot;

    private bool isinit = false;

    // ���� �����Ҷ� �κ��丮 ������ŭ ���� ����
    private void InitSlots()
    {
        
        inventory = DataManager.Inst.invenData; // ����
        for(int i = 0; i< inventory.MAXITEMCOUNT; i++)
        {
            obj = Instantiate(slotprefab, contentTrans);
            inventorySlot = obj. GetComponent<UIIneventorySlot>();
            inventorySlot.SLOTINDEX = i;
            inventorySlot.gameObject.name = inventorySlot.name + i + 1;
            slotList.Add(inventorySlot);
        }
    }
    // ������ �ֽ��� ������ ���ؼ� �ٽ� �׸��� �޼ҵ�
    public void RefreshIcon()
    {
        if(!isinit)
            InitSlots();
        inventory = DataManager.Inst. invenData;// ������

        itemDataList = inventory.GetItemList();
        inventory.CURITEMCOUNT = itemDataList.Count;

        // 18 ��
        for(int i = 0; i < inventory.MAXITEMCOUNT; i++)
        {
            if ( i  < inventory.CURITEMCOUNT && -1 < itemDataList[i].itemID )
            {
                slotList[i].DrawItem(itemDataList[i]);
            }
            else //���� ���������� ū index���� ���Ե��� ��� ������ ����.
            {
                slotList[i].ClearSlot();

            }
            slotList[i].SelectSlot(false); // ���� ������ ��Ȱ��ȭ.
        }
    }
}
