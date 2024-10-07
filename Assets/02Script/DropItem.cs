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
            rb.useGravity = false; // 중력가속도 멈추고
            rb.velocity = Vector3.zero; // 현 속도 0
            dropPosY = moveTrans.position.y;
            isDrop = true;

        }

        if(other.CompareTag("Player"))
        {
            Debug.Log("플레이어 아이템 습득 " + dropInfo.itemID + dropInfo.amount);
            DataManager.Inst.LootingItem(dropInfo);
            Destroy(gameObject);
        }
    }

    // 어떤 아이템 itemID
    // 아이템의 갯수 - 몇개를 떨궛는지?
    InventoryItemData dropInfo = new InventoryItemData();
    
    //임시로 awake에서 호출
    public void DropItemInit(int dropItemID, int dropAmount)
    {
        dropInfo.itemID = dropItemID;
        dropInfo.amount = dropAmount;
        isDrop = false;
    }

}
