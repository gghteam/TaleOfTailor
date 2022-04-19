using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    // �������� ��
    private float moveAmount;

    private float moveX;
    private float moveY;

    PlayerControls inputActions;

    Vector2 movementInput;
    Vector2 cameraInput;

    // X, Z�� �������� �������ֱ� ���� ����ü
    EventParam eventParam = new EventParam();

    // ĳ���Ͱ� Ŀ��������(ó�� Setting)
    public void OnEnable()
    {
        if(inputActions == null)
        {
            // ���ο� PlayerControls ���� �� �ֱ�
            inputActions = new PlayerControls();

            // �� �Է��� Ű�� ���� ���� �� �ֵ�����
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();

            //inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        // inputActions ���
        inputActions.Enable();
    }

    /// <summary>
    /// ���� ������Ʈ�� ������� inputActions�� �� �̻� ������� �ʵ��� ����
    /// </summary>
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        MoveInput();
    }

    /// <summary>
    /// �����ӿ� ���� �Է��� ����մϴ�.
    /// </summary>
    private void MoveInput()
    {
        // X, Z �Է�
        horizontal = movementInput.x;
        vertical = movementInput.y;

        // �������� �� ���
        //Mathf.Clamp01 -> ������ 0���� 1 ������ ��ȯ
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        // TO DO ���� ī�޶� ���
        moveX = cameraInput.x;
        moveY = cameraInput.y;

        //EventManager�� ���� Setting
        eventParam.vectorParam = new Vector2(horizontal, vertical);
        eventParam.intParam = (int)moveAmount;

        //�������� ���� ��ȣ ����
        EventManager.TriggerEvent("PLAYER_MOVEMENT", eventParam);
    }
}
