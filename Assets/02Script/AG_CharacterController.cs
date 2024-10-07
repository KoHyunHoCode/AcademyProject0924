using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ĳ������ ������ ����ϸ鼭 
// �ִϸ��̼��� ���� ���ִ� ���� 
// �κ��丮 

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
    private FixedJoystick joystick; // joystickpack ���¿��� ����


    //private static int animsParam_IsMove = Animator.StringToHash("IsMove");
    #endregion


   
    
    

    #region _Private_
    private Vector3 moveDelta;
    private Animator anims;

    private static int animsParam_IsMove = Animator.StringToHash("IsMove");
    #endregion

    #region _Public_
    #endregion




    // WASD Ű�� �̿��ؼ� ĳ���͸� �̵���Ŵ. 
    // �̵� ���� ���ο� ���� 
    // Idle ��ǰ� forward ����� ������ ���� �ְ�... 
    // 7�� 10�б��� ���� 
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
        //Ű���� ��ǲó��
        moveDelta.x = Input.GetAxis("Horizontal");
        moveDelta.y = 0f;
        moveDelta.z = Input.GetAxis("Vertical");
        // UI ���̽�ƽ�� ���� ��ǲ ó��

        moveDelta.x += joystick.Horizontal;
        moveDelta.y += joystick.Vertical;

        // ����ȭ 
        moveDelta.Normalize();

        //ī�޶��� ���� ������
        camForward = Camera.main.transform.forward;
        camForward.y = 0;

        camRight = Camera.main.transform.right;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // ī�޶� �������� �Է°��� ����.
        moveDelta = camForward * moveDelta.z + camRight * moveDelta.x;
        moveDelta.Normalize();

        //ĳ���� �̵�
        controller.Move(moveDelta * (moveSpeed * Time.deltaTime));

        // ĳ������ ȸ��
        if (moveDelta != Vector3.zero)
            transform.forward = moveDelta;

        
        anims.SetBool(animsParam_IsMove, moveDelta != Vector3.zero); //  4

        
    }

}
