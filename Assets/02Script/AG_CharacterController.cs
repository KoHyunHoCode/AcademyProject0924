using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 캐릭터의 조작을 담당하면서 
// 애니메이션을 관리 해주는 역할 
// 인벤토리 

public class AG_CharacterController : MonoBehaviour
{
# region _SerializeField_
    [SerializeField] private float moveSpeed = 6f;
    #endregion

    #region _Private_
    //private Vector3 moveDelta;
    //private Animator anims;
    private CharacterController controller;
    private Vector3 camForward;
    private Vector3 camRight;

    private GameObject obj;
    private FixedJoystick joystick; // joystickpack 에셋에서 제공


    //private static int animsParam_IsMove = Animator.StringToHash("IsMove");
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
        TryGetComponent<CharacterController>(out controller);

        obj = GameObject.Find("Joystick");
        if(obj != null)
        {
            obj.TryGetComponent<FixedJoystick>(out joystick);
        }

    }

    private void Update()
    {
        //키보드 인풋처리
        moveDelta.x = Input.GetAxis("Horizontal");
        moveDelta.y = 0f;
        moveDelta.z = Input.GetAxis("Vertical");
        // UI 조이스틱을 통한 인풋 처리

        moveDelta.x += joystick.Horizontal;
        moveDelta.y += joystick.Vertical;

        // 정규화 
        moveDelta.Normalize();

        //카메라의 방향 데이터
        camForward = Camera.main.transform.forward;
        camForward.y = 0;

        camRight = Camera.main.transform.right;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // 카메라 기준으로 입력값을 적용.
        moveDelta = camForward * moveDelta.z + camRight * moveDelta.x;
        moveDelta.Normalize();

        //캐릭터 이동
        controller.Move(moveDelta * (moveSpeed * Time.deltaTime));

        // 캐릭터의 회전
        if (moveDelta != Vector3.zero)
            transform.forward = moveDelta;

        
        anims.SetBool(animsParam_IsMove, moveDelta != Vector3.zero); //  4

        
    }

}
