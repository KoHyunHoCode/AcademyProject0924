using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public enum NPC_Type
{
    ItemShop,
    Forge,
}

[RequireComponent(typeof(SphereCollider))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    NPC_Type type;
    private bool isOn = false;
    private SphereCollider col;

    [SerializeField]
    private IPopup targetPopup;
    private GameObject obj;


    private void Awake()
    {
        if (!TryGetComponent<SphereCollider>(out col))
            Debug.Log("콜라이더 참조 실패 ");
        else
        {
            col.radius = 2f;
            col.isTrigger = true;
        }

        obj = GameObject.Find(type.ToString()); // UI캔버스 요소 탐색 
        if (obj != null)
        {
            targetPopup = obj.GetComponent<IPopup>();
            Debug.Log("정상참조");
        }
    }
    // NPC랑 충돌 발생했을시 팝업 오픈. 
    private void OnTriggerEnter(Collider other)
    {
        if(!isOn && other.CompareTag("Player"))
        {
            isOn = true;
            targetPopup.PopupOpen();
        }
    }

    // 충돌시 플레이어 창닫기
    private void OnTriggerExit(Collider other)
    {
        if (isOn && other.CompareTag("Player"))
        {
            isOn = false;
            targetPopup.PopupClose();
        }
    }
}
