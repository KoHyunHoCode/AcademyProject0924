using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//게임 플레이 하고있을때 데이터 > ram(휘발성메모리)
//게임 종료했을때 저장 => 하드디스크 or ssd or usb

[System.Serializable] // 직렬화

public class InventoryItemData
{
    public int uid; // 아이템의 고유 코드
    public int itemID; // 아이템 정보를 확인하기 위한 id
    public int amount; // 보유하고 있는 갯수
}
public class DataInventory // 게임매니저 (싱글톤)=> 저장 & 로드가 원활이 되게
{
    private int maxItemCount = 18; // 인벤토리 최대공간
    public int MAXITEMCOUNT => maxItemCount;

    private int curItemCount; // 인벤토리 내에 가지고 있는 아이템의 갯수

    public int CURITEMCOUNT
    {
        get => curItemCount;
        set => curItemCount = value;
    }
    [SerializeField]
    private List<InventoryItemData> items = new List<InventoryItemData>();


    // 인벤토리에 아이템 습득
    //1. 습득한 아이템 중첩이 가능한가?
    //    n-1 습득처리
    // y-1 인벤토리 내에 중복된 아이템이 있는지?
    // y 갯수증가
    // n 신규데이터를 추가.
    public void AddItem(InventoryItemData newitem)
    {
        if (true) // 테이블 데이터가 존재하는가?
        {
            int index = FindItemIndex(newitem);
            if (true) // 장착이 가능한 아이템?(중첩  불가능)
            {
                // 중첩처리 하지 않고 리스트에 아이템 정보를 추가
                newitem.uid = 9999; // todo : 중복되지 않는 고유한 uid생성
                newitem.amount = 1;
                items.Add(newitem); // 인벤토리에 아이템 습득처리
                curItemCount++;
            }
            else if (-1 < index) // 중첩이 가능하고 중복된 아이템을 이미 가지고 있는경우
            {
                items[index].amount += newitem.amount;

            }
            else // 장착이 불가능한 아이템(중첩이 가능) 처음습득
            {
                newitem.uid = -1;
                items.Add(newitem);
                curItemCount++;
            }
        }
    }

    //강화를 통해서 인벤토리 정보가 변경된 정보를 갱신
    public void UpdateItemInfo(InventoryItemData changeData)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].uid == changeData.uid)
            {
                Debug.Log(items[i].itemID + "  강화 처리 됨");
                items[i].itemID = changeData.itemID;
                items[i].itemID = changeData.amount;

            }
        }
    }

    // 인벤토리에 찾고자 하는 아이템이 있는가 ? 있다면 index 리턴
    private int FindItemIndex(InventoryItemData findItem)
    {
        int result = -1;

        for (int i = items.Count-1; i >= 0; i--)
        {
            if (items[i].itemID == findItem.itemID)
            {
                result = i;
                break;
            }
        }
        return result; // 인벤토리에 아이템이 없는경우 -1리턴, 찾은 경우는 index가 리턴
    }


    //외부에 아이템리슽 전달.
    public List<InventoryItemData> GetItemList()
    {
        curItemCount = items.Count;
        return items;
    }

    // 상점에 아이템 팔거나 버릴때 인벤토리 정보에서 해당 아이템 삭제.
    public void DeleteItem(InventoryItemData deleteitem)
    {
        int index = FindItemIndex(deleteitem);

        if(index > -1)
        {
            if(items[index].amount >= deleteitem.amount)
            {
                items[index].amount -= deleteitem.amount;

                if(items[index].amount <= 0) // 남은게 없는경우
                {
                    items.RemoveAt(index);
                    curItemCount--;
                }
            }
        }
    }

    public bool IsFull()
    {
        return curItemCount >= maxItemCount;
    }
}

    


