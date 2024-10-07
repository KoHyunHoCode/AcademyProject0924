using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ui �����޴� ��ư�� �������ִ� �Ŵ���
// �κ��丮 ����/ Ŭ����
// �޴� �˾�����/ Ŭ����
// ���ʻ�� ����â ����.
public class MenuManaer : MonoBehaviour
{
    private GameObject inventoryObj;
    private UIInventory uiInventory;
    private bool isInventoryOpen;

    // ������� �޴� ��ư��
    private Button menuBtn;
    private Button inventoryBtn;

    private GameObject obj;

    private void Awake()
    {
        inventoryObj = GameObject.Find("Inventory");
        if(inventoryObj != null )
        {
            uiInventory = inventoryObj.GetComponent<UIInventory>();
        }

        //��ư event bind
        obj = GameObject.Find("InventoryBTN");
        inventoryBtn = obj.GetComponent<Button>();
        inventoryBtn.onClick.AddListener(onClick_ShowInventory);
        isInventoryOpen = false; // �ʱ�� inventory Scale 0,0,0 ����
    }

    public void onClick_ShowInventory()
    {
        isInventoryOpen = !isInventoryOpen; // �������� ����ó��.
        if(isInventoryOpen)
        {
            // �˾� open - �ֽ������� �����Ŀ� �˾� ũ�� 1,1,1
            //uiInventory.RefreshIcon();
            inventoryObj.LeanScale(Vector3.one, 1.5f).setEase(LeanTweenType.easeInOutElastic);
        }
        else
        {
            // �˾� close
            inventoryObj.LeanScale(Vector3.zero, 1.5f);
        }
    }

}
