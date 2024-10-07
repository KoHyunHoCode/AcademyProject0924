using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// itemData 에 따라서 icon 변경
// 소유하고 있는 갯수 표기
// 선택 여부 관리.
public class UIIneventorySlot : MonoBehaviour
{
    private bool isEmpty;
    private bool ISEMPTY => isEmpty;

    private int slotIndex;
    public int SLOTINDEX
    {
        get => slotIndex;
        set => slotIndex = value;
    }

    private Image icon; // 아이콘
    private GameObject focus; // 선택여부
    private TextMeshProUGUI amountText; // 보유 갯수 표기
    private Button button;

    private bool isSelect;
    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        focus = transform.GetChild(2).gameObject;
        button = GetComponent<Button>();

        button.onClick.AddListener(onClick_SelectButton);

    }

    // 슬롯이 보여질때 아이콘, 갯수등등을 갱신해주는 메소드.
    public void DrawItem(InventoryItemData newItem)
    { 
        if(DataManager.Inst.GetItemData(newItem.itemID,out ItemData_Entity itemData))
        {
            // DP : 다이나믹 프로그래밍
            icon.sprite = Resources.Load<Sprite>(itemData.iconlmg); // HDD, SSD => RAM 로딩
            ChangeAmount(newItem.amount);

            isEmpty = false;
            icon.enabled = true; //  아이콘 보이도록 활성화 처리.

        }
        else
        {
            Debug.Log("UI InventorySlot.cs - DrawItem() - 테이블 데이터 없음   " + newItem.itemID);
        }
    }
    //  슬롯을 비워주는 메소드
    public void ClearSlot()
    {
        focus.SetActive(false);
        isSelect = false;
        amountText.enabled = false;
        isEmpty = true;
        icon.enabled = false;
    }
    //보유갯수만 변경 되엇을때
    public void ChangeAmount(int newAmount)
    {
        amountText.text = newAmount.ToString();
    }

    // Focus 오브젝트만 활성화/비활성화
    // 아이템 상세정보 호출
    public void SelectSlot(bool isSelect)
    {
        focus.SetActive(!isSelect);
    }

    private void onClick_SelectButton()
    {
        if(!isEmpty)
        {
            isSelect = true;
            SelectSlot(isSelect);
            // 상세정보창 띄우는 작업 여기에서
        }
    }
}
