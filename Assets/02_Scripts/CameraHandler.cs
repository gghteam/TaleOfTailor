using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraTransformPos;
    private LayerMask ignoreLayers;


    [Tooltip("Y축회전속도")]
    public float lookSpeed = 0.1f;
    [Tooltip("따라오는 속도")]
    public float followSpeed = 0.1f;
    [Tooltip("X축회전속도")]
    public float pivotSpeed = 0.01f;
    [Tooltip("최소 X축")]
    public float minPivot = -35;
    [Tooltip("최대 X축")]
    public float maxPivot = 35;


    private float targetPos;
    private float defaultPos;
    private float lookAngle;
    private float pivotAngle;
    private float setDelta;

    //마우스의 위치
    private float mouseX;
    private float mouseY;

    private void Awake()
    {
        myTransform = transform;
        defaultPos = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        EventManager.StartListening("CAMERA_MOVE", SetMousePos);
    }


    public void FollowTarget(float delta)
    {
        //설정할 스피드에 따라 목표 위치로 이동
        Vector3 targetPos = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
        myTransform.position = targetPos;
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYinput)
    {
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYinput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    /// <summary>
    /// 마우스의 위치를 저장
    /// </summary>
    /// <param name="eventParam"></param>

    private void SetMousePos(EventParam eventParam)
    {
        mouseX = eventParam.vectorParam.x;
        Debug.Log(mouseX);
        mouseY = eventParam.vectorParam.y;
    }

    /// <summary>
    /// 따라오기와 Rotation을 실행시키는 Method
    /// </summary>
    private void CameraMove()
    {
        FollowTarget(setDelta);
        HandleCameraRotation(setDelta, mouseX, mouseY);
    }

    private void FixedUpdate()
    {
        setDelta = Time.fixedDeltaTime;

        CameraMove();
    }
}
