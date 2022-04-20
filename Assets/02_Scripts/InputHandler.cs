using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    // 움직임의 양
    private float moveAmount;

    private float moveX;
    private float moveY;

    //PlayerControls inputActions;

    Vector2 movementInput;
    Vector2 cameraInput;

    // X, Z의 움직임을 전달해주기 위한 구조체
    EventParam eventParam = new EventParam();


    private void Update()
    {
        MoveInput();
    }

    /// <summary>
    /// 움직임에 대한 입력을 담당합니다.
    /// </summary>
    private void MoveInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // X, Z 입력
        horizontal = movementInput.x;
        vertical = movementInput.y;

        // 움직임의 양 계산
        //Mathf.Clamp01 -> 강제로 0에서 1 범위로 변환
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        // TO DO 추후 카메라에 사용
        moveX = cameraInput.x;
        moveY = cameraInput.y;

        //EventManager를 위한 Setting
        eventParam.vectorParam = new Vector2(horizontal, vertical);
        eventParam.intParam = (int)moveAmount;

        //움직임을 위한 신호 전송
        EventManager.TriggerEvent("PLAYER_MOVEMENT", eventParam);
    }
}
