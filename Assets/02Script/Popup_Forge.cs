using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Forge : MonoBehaviour, IPopup
{
    public void PopupClose()
    {
        Debug.Log("���尣 Ŭ����");
    }

    public void PopupOpen()
    {
        Debug.Log("���尣 ����");
    }

}
