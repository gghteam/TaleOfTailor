using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDashKDH1 : PlayerKDH
{
    [Header("대쉬에 걸리는 속도")]
    [SerializeField]
    private float DashWaitSpeed;

    [Header("대쉬 스무스 시간")]
    [SerializeField]
    private float smoothTime = 0.2f;

    [Header("대쉬 속도")]
    [SerializeField]
    private float DashSpeed;

    [Header("대쉬 오브젝트 거리")]
    [SerializeField]
    private float DashObjectDistance;

    [Header("대쉬 오브젝트")]
    [SerializeField]
    private GameObject DashObjet;


    private Vector3 lastMoveSpd;
    private bool firstbool = false;
    private bool dashbool = false;
    private void Start()
    {
        EventManager.StartListening("ISDASH", isDash);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashbool == false)
        {
            StartCoroutine("DashBool");
        }

        if (dashbool)
        {
            Dash();
        }
    }
    private IEnumerator DashBool()
    {
        dashbool = true;
        firstbool = true;
        yield return new WaitForSeconds(DashWaitSpeed);
        dashbool = false;
    }
    private void Dash()
    {
        if (firstbool)
        {
            firstbool = false;
            DashObjet.transform.position = new Vector3(Input.GetAxisRaw("Horizontal") * DashObjectDistance, 0, Input.GetAxisRaw("Vertical") * DashObjectDistance);
        }
        Vector3 smoothPosition = Vector3.SmoothDamp(
            transform.position,
            DashObjet.transform.position,
            ref lastMoveSpd,
            smoothTime
            );

        transform.position = smoothPosition;
    }

    private void isDash(EventParam eventParam)
    {
        eventParam.boolParam = dashbool;
    }
}
