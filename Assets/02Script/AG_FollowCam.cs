using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AG_FollowCam : MonoBehaviour
{
    private Transform targetPlayer;

    [SerializeField]
    private Vector3 offset; // �Ÿ��ΰ� ���󰡵��� 

    private void Start()
    {
        targetPlayer = GameObject.Find("Player")?.GetComponent<Transform>();
    }

    // �ϳ��� ���� �ִ� ��� ������Ʈ���� 
    // ���� ���� update�� ó���ǰ�, ���� ���߿��Ǵ��� �˼�����. 
    // ��� ������Ʈ�� update�� ȣ��� �Ŀ�,
    // lateUpdate �� ����. 
    private void LateUpdate()
    {
        transform.position = targetPlayer.position + offset;

        
    }
}
