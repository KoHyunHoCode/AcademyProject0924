using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Forge : MonoBehaviour, IPopup
{
    public void PopupClose()
    {
        Debug.Log("대장간 클로즈");
    }

    public void PopupOpen()
    {
        Debug.Log("대장간 오픈");
    }

}
