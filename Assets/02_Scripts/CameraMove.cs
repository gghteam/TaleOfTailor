using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform targetTransform;
    private Transform cameraTransform;

    [Range(2.0f, 20.0f)]
    public float distance = 10f;

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    public float moveDamping = 15f;
    public float rotateDamping = 10f;

    public float targetOffset = 2f;

    public Vector3 offset = new Vector3(0, 1.0f, -1.0f);
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Rotate();
    }


    private void LateUpdate()
    {
        //Vector3 vector3 = targetTransform.position + offset * distance;
        transform.position = targetTransform.position + offset * distance; 
        transform.LookAt(targetTransform);

        //구면 보간: 현재 -> 타켓의 회전
        //cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetTransform.rotation, rotateDamping * Time.deltaTime);

        //cameraTransform.LookAt(targetTransform.position + (targetTransform.up * targetOffset));

        //ector3 pos = targetTransform.position + (-targetTransform.forward * distance) + (targetTransform.up * height);

        //구면 보간 : 현재 -> 타켓의 위치
        //cameraTransform.position = Vector3.Slerp(cameraTransform.position, pos, moveDamping * Time.deltaTime);

        //transform.LookAt(targetTransform);
    }




    Vector2 m_Input;

    void Rotate()
    {
        m_Input.x = Input.GetAxis("Mouse X");
        m_Input.y = Input.GetAxis("Mouse Y");

        if (m_Input.magnitude != 0)
        {
            cameraTransform.RotateAround(targetTransform.position, Vector3.up, m_Input.x * 20);

            offset = transform.position - targetTransform.position;
            offset.Normalize();
        }
    }
}
