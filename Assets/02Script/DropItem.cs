using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent (typeof(Rigidbody))]
public class DropItem : MonoBehaviour
{
    private Rigidbody rb;
    private SphereCollider col;

    private bool isDrop;
    private Vector3 pos;
    private Transform moveTrans;
    private float dropPosY;
    private float timeValue;
    private void Awake()
    {
        TryGetComponent<SphereCollider>(out col);
        TryGetComponent<Rigidbody>(out rb);

        col.radius = 0.5f;
        col.isTrigger = true;

        rb.useGravity = true;
        Vector3 dropDir = Vector3.up;
        dropDir.x = Random.Range(-0.3f, 0.3f);
        dropDir.z = Random.Range(-0.3f, 0.3f);
        rb.AddForce(dropDir * 5f, ForceMode.Impulse);

        moveTrans = transform.GetChild(0);
        DropItemInit(1001, 1);
    }

    private void Update()
    {
        if(isDrop)
        {
            moveTrans.Rotate(Vector3.up * (90.0f * Time.deltaTime));
            pos = transform.position;
            timeValue += Time.deltaTime;
            pos.y = dropPosY + 0.3f * Mathf.Sin(timeValue);
            moveTrans.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            rb.useGravity = false; // �߷°��ӵ� ���߰�
            rb.velocity = Vector3.zero; // �� �ӵ� 0
            dropPosY = moveTrans.position.y;
            isDrop = true;

        }

        if(other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ������ ���� " + dropInfo.itemID + dropInfo.amount);
            DataManager.Inst.LootingItem(dropInfo);
            Destroy(gameObject);
        }
    }

    // � ������ itemID
    // �������� ���� - ��� ��������?
    InventoryItemData dropInfo = new InventoryItemData();
    
    //�ӽ÷� awake���� ȣ��
    public void DropItemInit(int dropItemID, int dropAmount)
    {
        dropInfo.itemID = dropItemID;
        dropInfo.amount = dropAmount;
        isDrop = false;
    }

}
