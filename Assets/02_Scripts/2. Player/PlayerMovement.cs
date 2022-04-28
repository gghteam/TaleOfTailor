using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character
{

    // EventParam���� ���� ����
    private int inputX;
    private int inputZ;
    private int inputAmount;


    Transform cameraObject;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;


    [Header("Stats")]
    [SerializeField]
    private float movementSpeed = 5;
    [SerializeField]
    private float runMovementSpeed = 7;
    [SerializeField]
    private float rotationSpeed = 10;

    private bool isDash = false;

    private void Start()
    {
        //Player �������� ���� ����
        EventManager.StartListening("PLAYER_MOVEMENT", SetMovement);
        EventManager.StartListening("ISDASH", IsDash);
        //��� ȣ�� �ϴ� ���� ����(����ȭ)
        cameraObject = Camera.main.transform;
        myTransform = transform;
    }

    public void Update()
    {
        if (isDash)
            return;
        //ĳ���� ��(inputZ = 1) �Ǵ� ��(inputZ = -1)�� vector�� ����
        moveDirection = cameraObject.forward * inputZ;
        //ĳ���� ������(inputZ = 1) �Ǵ� ����(inputZ = -1)�� vector�� ����
        moveDirection += cameraObject.right * inputX;
        //vector�� ����ȭ��(���̸� 1�� ����� ���⸸ ����)
        moveDirection.Normalize();
        if(moveDirection.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, cameraObject.eulerAngles.y, transform.rotation.z);
            moveDirection.y = 0;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                //���⿡ Run_Speed�� ����
                moveDirection *= runMovementSpeed;
                ani.SetBool("IsMove", false);
                ani.SetBool("IsRun", true);
            }
            else
            {
                //���⿡ Speed�� ����
                moveDirection *= movementSpeed;
                ani.SetBool("IsMove", true);
                ani.SetBool("IsRun", false);
            }

        }
        else
<<<<<<< HEAD
        {
            ani.SetBool("IsMove", false);
            ani.SetBool("IsRun", false);
        }

=======
            //���⿡ Speed�� ����
            moveDirection *= movementSpeed;
>>>>>>> kdh

        //normalVector�� ���� ������κ��� �÷��̾ �����̷��� ���⺤�ͷ� ����
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        //�̵�
        rigidbody.velocity = projectedVelocity;

        transform.LookAt(transform.position + moveDirection);
    }

    /// <summary>
    /// �ı��Ǹ� �� �̻� �������� ���� �ʴ´�.
    /// </summary>
    private void OnDestroy()
    {
        EventManager.StopListening("PLAYER_MOVEMENT", SetMovement);
    }

    #region Movement
    Vector3 normalVector;

    /// <summary>
    /// Listening�� ���� Setting
    /// </summary>
    /// <param name="eventParam"></param>
    private void SetMovement(EventParam eventParam)
    {
        inputX = (int)eventParam.vectorParam.x;
        inputZ = (int)eventParam.vectorParam.y;
        inputAmount = eventParam.intParam;
    }
    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputAmount;

        targetDir = cameraObject.forward * inputZ;
        targetDir += cameraObject.right * inputX;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = myTransform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }
    #endregion

    private void IsDash(EventParam eventParam)
    {
        isDash = eventParam.boolParam;
    }
}
