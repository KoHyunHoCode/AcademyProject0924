using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_ItemShop : MonoBehaviour, IPopup
{
    public void PopupClose()
    {
        Debug.Log("�����ۼ� Ŭ����");
    }

    public void PopupOpen()
    {
        Debug.Log("�����ۼ� ����");
    }
}
