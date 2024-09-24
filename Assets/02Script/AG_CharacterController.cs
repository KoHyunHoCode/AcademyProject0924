using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ĳ������ ������ ����ϸ鼭 
// �ִϸ��̼��� ���� ���ִ� ���� 
// �κ��丮 

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




    // WASD Ű�� �̿��ؼ� ĳ���͸� �̵���Ŵ. 
    // �̵� ���� ���ο� ���� 
    // Idle ��ǰ� forward ����� ������ ���� �ְ�... 
    // 7�� 10�б��� ���� 
    private void Awake()
    {
        TryGetComponent<Animator>(out anims);
    }

    private void Update()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = 0f;
        moveDelta.z = Input.GetAxisRaw("Vertical");
        // ����ȭ 
        moveDelta.Normalize();

        moveDelta *= (moveSpeed * Time.deltaTime);

        transform.Translate(moveDelta);
        //transform.LookAt(transform.position + moveDelta);
        //anims.SetBool("IsMove", moveDelta != Vector3.zero);// 12����Ʈ 
        anims.SetBool(animsParam_IsMove, moveDelta != Vector3.zero); //  4
    }

}
