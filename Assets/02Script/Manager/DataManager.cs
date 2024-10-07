using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//싱글톤
// 게임이 실행이 되면 Resources/Table 폴더에 있는 ActionGame 스크립터블 오브젝트를 로드.
// 탐색에 빠른 Map 자료구조로 변경해서 관리.
// ID 데이터 찾아달라고 하면, 데이터행 리턴.
// 유저의 게임정보 (HP, 습득한 장비 , EXP) 관리
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
    public int uidCounter; // 아이템을 습득할때마다 1씩 증가
    public DataInventory inventory; // 실질적인 inventory 역할. 
}
public class DataManager : SingletonDestroy<DataManager>
{
    private string dataPath; // 세이브 데이터 
    private ActionGame originTable;

    private UserData userData = new UserData();
    
    //STL => MAP
    // C# => DICTONARY

    private Dictionary<int, ItemData_Entity> dicitemData = new Dictionary<int, ItemData_Entity>();
    private void Start()
    {
        // 1. 동적 로딩 3 방법 Resources 폴더를 활용하는 방법
        // 2. AssetBundle
        // 3. 
        originTable = Resources.Load<ActionGame>("Table/ActionGame");

        for (int i = 0; i < originTable.ItemData.Count; i++)
        {
            dicitemData.Add(originTable.ItemData[i].id, originTable.ItemData[i]);
        }

        Debug.Log("아이템 이름은? " + dicitemData[1004].name);
        CreateUserData("test");
    }
    // dicItemData 데이터를 보호하기 위해서 캡슐화 적용.
    // GetItemData 메소드를 통해서 데이터 존재하는지? 존재한다면 테이블데이터 리턴

    public bool GetItemData(int itemID, out ItemData_Entity itemData)
    {
        return dicitemData.TryGetValue(itemID, out itemData);
    }

    //캐릭터 최초 생성시에 기본값 초기화를 위해서
    public void CreateUserData(string newNickName)
    {
        userData.userNickName = newNickName;
        userData.charLevel = 1;
        userData.curEXP = 0;
        userData.maxHP = userData.curHP = 50;
        userData.maxMP = userData.curMP = 30;
        userData.gold = 50000; //개발과정에서 테스트
        userData.uidCounter = 1;
        userData.inventory = new DataInventory();
        

    }

    //아이템 습득처리하는 메소드
    // 습득 성공하면 true리턴
    // 습득 실패하면 false리턴
    public bool LootingItem(InventoryItemData addItemInfo)
    {
        if(!userData.inventory.IsFull ())
        {
            userData.inventory.AddItem(addItemInfo);
            Debug.Log("습득 완료");
            return true;
        }
        // else 중첩가능한 아이템인데, 이미 슬롯을 차지하고 있는 경우 .추후 구현
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
