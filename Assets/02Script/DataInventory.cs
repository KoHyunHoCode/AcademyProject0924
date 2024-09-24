using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���� �÷��� �ϰ������� ������ > ram(�ֹ߼��޸�)
//���� ���������� ���� => �ϵ��ũ or ssd or usb

[System.Serializable] // ����ȭ

public class InventoryItemData
{
    public int uid; // �������� ���� �ڵ�
    public int itemID; // ������ ������ Ȯ���ϱ� ���� id
    public int amount; // �����ϰ� �ִ� ����
}
public class DataInventory // ���ӸŴ��� (�̱���)=> ���� & �ε尡 ��Ȱ�� �ǰ�
{
    private int maxItemCount = 18; // �κ��丮 �ִ����
    public int MAXITEMCOUNT => maxItemCount;

    private int curItemCount; // �κ��丮 ���� ������ �ִ� �������� ����

    public int CURITEMCOUNT
    {
        get => curItemCount;
        set => curItemCount = value;
    }
    [SerializeField]
    private List<InventoryItemData> items = new List<InventoryItemData>();


    // �κ��丮�� ������ ����
    //1. ������ ������ ��ø�� �����Ѱ�?
    //    n-1 ����ó��
    // y-1 �κ��丮 ���� �ߺ��� �������� �ִ���?
    // y ��������
    // n �űԵ����͸� �߰�.
    public void AddItem(InventoryItemData newitem)
    {
        if (true) // ���̺� �����Ͱ� �����ϴ°�?
        {
            int index = FindItemIndex(newitem);
            if (true) // ������ ������ ������?(��ø  �Ұ���)
            {
                // ��øó�� ���� �ʰ� ����Ʈ�� ������ ������ �߰�
                newitem.uid = 9999; // todo : �ߺ����� �ʴ� ������ uid����
                newitem.amount = 1;
                items.Add(newitem); // �κ��丮�� ������ ����ó��
                curItemCount++;
            }
            else if (-1 < index) // ��ø�� �����ϰ� �ߺ��� �������� �̹� ������ �ִ°��
            {
                items[index].amount += newitem.amount;

            }
            else // ������ �Ұ����� ������(��ø�� ����) ó������
            {
                newitem.uid = -1;
                items.Add(newitem);
                curItemCount++;
            }
        }
    }

    //��ȭ�� ���ؼ� �κ��丮 ������ ����� ������ ����
    public void UpdateItemInfo(InventoryItemData changeData)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].uid == changeData.uid)
            {
                Debug.Log(items[i].itemID + "  ��ȭ ó�� ��");
                items[i].itemID = changeData.itemID;
                items[i].itemID = changeData.amount;

            }
        }
    }

    // �κ��丮�� ã���� �ϴ� �������� �ִ°� ? �ִٸ� index ����
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
        return result; // �κ��丮�� �������� ���°�� -1����, ã�� ���� index�� ����
    }


    //�ܺο� �����۸��� ����.
    public List<InventoryItemData> GetItemList()
    {
        curItemCount = items.Count;
        return items;
    }

    // ������ ������ �Ȱų� ������ �κ��丮 �������� �ش� ������ ����.
    public void DeleteItem(InventoryItemData deleteitem)
    {
        int index = FindItemIndex(deleteitem);

        if(index > -1)
        {
            if(items[index].amount >= deleteitem.amount)
            {
                items[index].amount -= deleteitem.amount;

                if(items[index].amount <= 0) // ������ ���°��
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

    


