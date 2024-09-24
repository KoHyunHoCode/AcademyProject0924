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
            Debug.Log("�ݶ��̴� ���� ���� ");
        else
        {
            col.radius = 2f;
            col.isTrigger = true;
        }

        obj = GameObject.Find(type.ToString()); // UIĵ���� ��� Ž�� 
        if (obj != null)
        {
            targetPopup = obj.GetComponent<IPopup>();
            Debug.Log("��������");
        }
    }
    // NPC�� �浹 �߻������� �˾� ����. 
    private void OnTriggerEnter(Collider other)
    {
        if(!isOn && other.CompareTag("Player"))
        {
            isOn = true;
            targetPopup.PopupOpen();
        }
    }

    // �浹�� �÷��̾� â�ݱ�
    private void OnTriggerExit(Collider other)
    {
        if (isOn && other.CompareTag("Player"))
        {
            isOn = false;
            targetPopup.PopupClose();
        }
    }
}
