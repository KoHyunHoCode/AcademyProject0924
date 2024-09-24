using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 캐릭터의 조작을 담당하면서 
// 애니메이션을 관리 해주는 역할 
// 인벤토리 

public class AG_CharacterController : MonoBehaviour
{
    #region _SerializeField_
    [SerializeField] private float moveSpeed = 2f;
    #endregion

    #region _Private_
    private Vector3 moveDelta;
    private Animator anims;

    private static int animsParam_IsMove = Animator.StringToHash("IsMove");
    #endregion

    #region _Public_
    #endregion




    // WASD 키를 이용해서 캐릭터를 이동시킴. 
    // 이동 조작 여부에 따라서 
    // Idle 모션과 forward 모션을 번갈아 갈수 있게... 
    // 7시 10분까지 구현 
    private void Awake()
    {
        TryGetComponent<Animator>(out anims);
    }

    private void Update()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = 0f;
        moveDelta.z = Input.GetAxisRaw("Vertical");
        // 정규화 
        moveDelta.Normalize();

        moveDelta *= (moveSpeed * Time.deltaTime);

        transform.Translate(moveDelta);
        //transform.LookAt(transform.position + moveDelta);
        //anims.SetBool("IsMove", moveDelta != Vector3.zero);// 12바이트 
        anims.SetBool(animsParam_IsMove, moveDelta != Vector3.zero); //  4
    }

}
