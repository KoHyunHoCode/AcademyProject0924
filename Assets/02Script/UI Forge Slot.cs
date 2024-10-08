using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIForgeSlot : MonoBehaviour
{
    private Popup_Forge forgePopup;

    private Image icon;
    private Image focus;
    private bool isSelect;
    private InventoryItemData data;
    private Button selectBtn;

    private GameObject obj;


    private void Awake()
    {
        selectBtn = GetComponent<Button>();
        selectBtn.onClick.AddListener(OnClick_SelectBtn); // �̺�Ʈ ���ε�

        obj = transform.Find("Icon").gameObject;

        if(obj != null )
            obj.TryGetComponent<Image>(out icon);

        obj = transform.Find("Focus").gameObject;

        if(obj != null )
            obj.TryGetComponent<Image>(out focus);


    }

    public void CreateSlot(Popup_Forge forge)
    {
        forgePopup = forge;
        gameObject.SetActive(false);
    }

    public void OnClick_SelectBtn()
    {
        if(!isSelect) // ������ �������� �ƴҶ�
        {
            // �����ֿ� ������ ������ �Ǿ��� �˸�.
            //forgePopup.gameObject
            SelectFocus(true);
        }
    }

    public void SelectFocus(bool select)
    {
        isSelect = select;
        focus.enabled = select;
    }
    public void RefreshSlot(InventoryItemData itemData)
    {
        gameObject.SetActive (true);
        data = itemData;

        if(DataManager.Inst.GetItemData(data.itemID, out ItemData_Entity tableData))
        {
            icon.sprite = Resources.Load<Sprite>(tableData.iconlmg);
            icon.enabled = true;
            SelectFocus(false);
        }
    }

    public void ClearSlot()
    {
        gameObject.SetActive(false);
    }
        




}
