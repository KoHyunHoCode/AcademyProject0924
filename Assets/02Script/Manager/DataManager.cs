using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//�̱���
// ������ ������ �Ǹ� Resources/Table ������ �ִ� ActionGame ��ũ���ͺ� ������Ʈ�� �ε�.
// Ž���� ���� Map �ڷᱸ���� �����ؼ� ����.
// ID ������ ã�ƴ޶�� �ϸ�, �������� ����.
// ������ �������� (HP, ������ ��� , EXP) ����
[System.Serializable]

public class UserData
{
    public string userNickName;
    public int charLevel;
    public int curEXP;
    public int curHP;
    public int maxHP;
    public int curMP;
    public int maxMP;
    public int gold;
    public int uidCounter; // �������� �����Ҷ����� 1�� ����
    public DataInventory inventory; // �������� inventory ����. 
}
public class DataManager : SingletonDestroy<DataManager>
{
    private string dataPath; // ���̺� ������ 
    private ActionGame originTable;

    private UserData userData = new UserData();
    
    //STL => MAP
    // C# => DICTONARY

    private Dictionary<int, ItemData_Entity> dicitemData = new Dictionary<int, ItemData_Entity>();
    private void Start()
    {
        // 1. ���� �ε� 3 ��� Resources ������ Ȱ���ϴ� ���
        // 2. AssetBundle
        // 3. 
        originTable = Resources.Load<ActionGame>("Table/ActionGame");

        for (int i = 0; i < originTable.ItemData.Count; i++)
        {
            dicitemData.Add(originTable.ItemData[i].id, originTable.ItemData[i]);
        }

        Debug.Log("������ �̸���? " + dicitemData[1004].name);
        CreateUserData("test");
    }
    // dicItemData �����͸� ��ȣ�ϱ� ���ؼ� ĸ��ȭ ����.
    // GetItemData �޼ҵ带 ���ؼ� ������ �����ϴ���? �����Ѵٸ� ���̺����� ����

    public bool GetItemData(int itemID, out ItemData_Entity itemData)
    {
        return dicitemData.TryGetValue(itemID, out itemData);
    }

    //ĳ���� ���� �����ÿ� �⺻�� �ʱ�ȭ�� ���ؼ�
    public void CreateUserData(string newNickName)
    {
        userData.userNickName = newNickName;
        userData.charLevel = 1;
        userData.curEXP = 0;
        userData.maxHP = userData.curHP = 50;
        userData.maxMP = userData.curMP = 30;
        userData.gold = 50000; //���߰������� �׽�Ʈ
        userData.uidCounter = 1;
        userData.inventory = new DataInventory();
        

    }

    //������ ����ó���ϴ� �޼ҵ�
    // ���� �����ϸ� true����
    // ���� �����ϸ� false����
    public bool LootingItem(InventoryItemData addItemInfo)
    {
        if(!userData.inventory.IsFull ())
        {
            userData.inventory.AddItem(addItemInfo);
            Debug.Log("���� �Ϸ�");
            return true;
        }
        // else ��ø������ �������ε�, �̹� ������ �����ϰ� �ִ� ��� .���� ����
        return false;
    }

    public int PlayerGold
    {
        get => userData.gold;
        set => userData.gold = value;
    }

    public string PlayerName // => userData.userNickName;
    {
        get => userData.userNickName;
    }

    public int PlaerLevel
    {
        get => userData.charLevel;
    }

    public int PlayerCurrenEXP
    {
        get => userData.curEXP;
    }

    public int PlayerUID
    {
        get
        {
            return ++userData.uidCounter;
        }
    }

    public DataInventory invenData
    {
        get
        {
            return userData.inventory;
        }
    }

}
