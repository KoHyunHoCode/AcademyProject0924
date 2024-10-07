using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// itemData �� ���� icon ����
// �����ϰ� �ִ� ���� ǥ��
// ���� ���� ����.
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

    private Image icon; // ������
    private GameObject focus; // ���ÿ���
    private TextMeshProUGUI amountText; // ���� ���� ǥ��
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

    // ������ �������� ������, ��������� �������ִ� �޼ҵ�.
    public void DrawItem(InventoryItemData newItem)
    { 
        if(DataManager.Inst.GetItemData(newItem.itemID,out ItemData_Entity itemData))
        {
            // DP : ���̳��� ���α׷���
            icon.sprite = Resources.Load<Sprite>(itemData.iconlmg); // HDD, SSD => RAM �ε�
            ChangeAmount(newItem.amount);

            isEmpty = false;
            icon.enabled = true; //  ������ ���̵��� Ȱ��ȭ ó��.

        }
        else
        {
            Debug.Log("UI InventorySlot.cs - DrawItem() - ���̺� ������ ����   " + newItem.itemID);
        }
    }
    //  ������ ����ִ� �޼ҵ�
    public void ClearSlot()
    {
        focus.SetActive(false);
        isSelect = false;
        amountText.enabled = false;
        isEmpty = true;
        icon.enabled = false;
    }
    //���������� ���� �Ǿ�����
    public void ChangeAmount(int newAmount)
    {
        amountText.text = newAmount.ToString();
    }

    // Focus ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ
    // ������ ������ ȣ��
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
            // ������â ���� �۾� ���⿡��
        }
    }
}
