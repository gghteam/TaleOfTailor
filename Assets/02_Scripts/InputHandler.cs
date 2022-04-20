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

    //PlayerControls inputActions;

    Vector2 movementInput;
    Vector2 cameraInput;

    // X, Z�� �������� �������ֱ� ���� ����ü
    EventParam eventParam = new EventParam();


    private void Update()
    {
        MoveInput();
    }

    /// <summary>
    /// �����ӿ� ���� �Է��� ����մϴ�.
    /// </summary>
    private void MoveInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
