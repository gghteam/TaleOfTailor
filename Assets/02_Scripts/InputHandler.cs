using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    // �������� ��
    private float moveAmount;

    private float mouseX;
    private float MouseY;

    //PlayerControls inputActions;

    Vector2 movementInput;
    Vector2 cameraInput;

    // X, Z�� �������� �������ֱ� ���� ����ü
    EventParam eventParam = new EventParam();
    EventParam cameraParam = new EventParam();

    private void Awake()
    {

        //Ŀ�� ����� �� ��ġ ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MoveInput();
        AttackInput();
    }

    /// <summary>
    /// �����ӿ� ���� �Է��� ����մϴ�.
    /// </summary>
    private void MoveInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // X, Z �Է�
        horizontal = movementInput.x;
        vertical = movementInput.y;

        // �������� �� ���
        //Mathf.Clamp01 -> ������ 0���� 1 ������ ��ȯ
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        // TO DO ���� ī�޶� ���
        mouseX = cameraInput.x;
        MouseY = cameraInput.y;

        //EventManager�� ���� Setting
        eventParam.vectorParam = new Vector2(horizontal, vertical);
        eventParam.intParam = (int)moveAmount;
        cameraParam.vectorParam = new Vector2(mouseX, MouseY);

        //�������� ���� ��ȣ ����
        EventManager.TriggerEvent("INPUT", eventParam);
        EventManager.TriggerEvent("PLAYER_MOVEMENT", eventParam);
        EventManager.TriggerEvent("CAMERA_MOVE", cameraParam);
    }

    private void AttackInput()
    {
        eventParam.boolParam = Input.GetMouseButtonDown(0);

        if (eventParam.boolParam)
            EventManager.TriggerEvent("InputAttack", eventParam);
    }
}
