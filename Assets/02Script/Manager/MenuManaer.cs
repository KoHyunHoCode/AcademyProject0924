using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ui 여러메뉴 버튼들 관리해주는 매니저
// 인벤토리 오픈/ 클로즈
// 메뉴 팝업오픈/ 클로즈
// 왼쪽상단 정보창 관리.
public class MenuManaer : MonoBehaviour
{
    private GameObject inventoryObj;
    private UIInventory uiInventory;
    private bool isInventoryOpen;

    // 우측상단 메뉴 버튼들
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

        //버튼 event bind
        obj = GameObject.Find("InventoryBTN");
        inventoryBtn = obj.GetComponent<Button>();
        inventoryBtn.onClick.AddListener(onClick_ShowInventory);
        isInventoryOpen = false; // 초기는 inventory Scale 0,0,0 시작
    }

    public void onClick_ShowInventory()
    {
        isInventoryOpen = !isInventoryOpen; // 열림상태 반전처리.
        if(isInventoryOpen)
        {
            // 팝업 open - 최신정보로 갱신후에 팝업 크기 1,1,1
            //uiInventory.RefreshIcon();
            inventoryObj.LeanScale(Vector3.one, 1.5f).setEase(LeanTweenType.easeInOutElastic);
        }
        else
        {
            // 팝업 close
            inventoryObj.LeanScale(Vector3.zero, 1.5f);
        }
    }

}
