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
    private TextMeshProUGUI balanceTest; // ���� ���� �ܾ�
    [SerializeField]
    private TextMeshProUGUI enchantPrice; // ��ȭ ���
    [SerializeField]
    private TextMeshProUGUI enchantLevel; // ���� ��ȭ���� 
    [SerializeField]
    private TextMeshProUGUI echantAfterLevel; // ���������� ����
    [SerializeField]
    private Button tryEnchantBTN;
    [SerializeField]
    private Image icon;

    List<UIForgeSlot> forgeSlotList = new List<UIForgeSlot> ();
    private DataInventory inventory;

    InventoryItemData selectItem;
    List<InventoryItemData> dataList; // *�߿�*
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
        if(TryEnchant()) // ��ȭ ����
        {
            selectItem.itemID++; // ���ݷ� ���� ����
            DataManager.Inst.invenData.UpdateItemInfo(selectItem); // ������ ���� ����
            SelectItem(selectItem); // ��ü� â���� ���õ� ������ ����
        }
    }

    private void InitPopup() // ���� ���� ����.
    {

    }

    //������ ������ �մ� �����۵� �߿��� 
    // ��ȭ�� �� �� �ִ� �����۸� ����, ������� ǥ�ð� ���� �ʵ��� ���� �������Ŀ� ǥ��
    //���⿡�� ���� ����
    //������ ����. �Ϲ��� ����
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
        Debug.Log("���尣 Ŭ����");
    }

    public void PopupOpen()
    {
        Debug.Log("���尣 ����");
    }

}
