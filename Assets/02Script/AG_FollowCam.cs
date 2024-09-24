using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AG_FollowCam : MonoBehaviour
{
    private Transform targetPlayer;

    [SerializeField]
    private Vector3 offset; // 거리두고 따라가도록 

    private void Start()
    {
        targetPlayer = GameObject.Find("Player")?.GetComponent<Transform>();
    }

    // 하나의 씬에 있는 모든 오브젝트들은 
    // 누가 먼저 update가 처리되고, 누가 나중에되는지 알수없음. 
    // 모든 오브젝트의 update가 호출된 후에,
    // lateUpdate 가 진행. 
    private void LateUpdate()
    {
        transform.position = targetPlayer.position + offset;

        
    }
}
