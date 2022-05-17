using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPos;
    private LayerMask ignoreLayers;
    private Vector3 cameraFollowVelocity = Vector3.zero;


    [Tooltip("Y��ȸ���ӵ�")]
    public float lookSpeed = 0.1f;
    [Tooltip("������� �ӵ�")]
    public float followSpeed = 0.1f;
    [Tooltip("X��ȸ���ӵ�")]
    public float pivotSpeed = 0.01f;
    [Tooltip("�ּ� X��")]
    public float minPivot = -35;
    [Tooltip("�ִ� X��")]
    public float maxPivot = 35;


    private float targetPos;
    private float defaultPos;
    private float lookAngle;
    private float pivotAngle;
    private float setDelta;

    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimumCollisionOffset = 0.2f;

    //���콺�� ��ġ
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
        //������ ���ǵ忡 ���� ��ǥ ��ġ�� �̵�
        Vector3 targetPos = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);
        myTransform.position = targetPos;

        HandleCameraCollision(delta);
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

    private void HandleCameraCollision(float delta)
    {
        targetPos = defaultPos;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPos), ignoreLayers))
        {
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPos = -(dis - cameraCollisionOffset);
        }

        if(Mathf.Abs(targetPos) < minimumCollisionOffset)
        {
            targetPos = -minimumCollisionOffset;
        }

        cameraTransformPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPos, delta / 0.2f);
        
        /*
        if(targetTransform.localPosition.z <= cameraTransformPos.z)
        {
            return;
        }
        */

        cameraTransform.localPosition = cameraTransformPos;

    }

    private void OverCollisionCamra(float delta)
    {
        Debug.Log("����");
        cameraTransformPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetTransform.localPosition.z, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPos;
    }

    /// <summary>
    /// ���콺�� ��ġ�� ����
    /// </summary>
    /// <param name="eventParam"></param>

    private void SetMousePos(EventParam eventParam)
    {
        mouseX = eventParam.vectorParam.x;
        mouseY = eventParam.vectorParam.y;
    }

    /// <summary>
    /// �������� Rotation�� �����Ű�� Method
    /// </summary>
    private void CameraMove()
    {
        if(UIManager.Instance.isSetting)
        {
            return;
        }
        FollowTarget(setDelta);
        HandleCameraRotation(setDelta, mouseX, mouseY);
    }

    private void FixedUpdate()
    {
        setDelta = Time.fixedDeltaTime;

        CameraMove();
    }
}
