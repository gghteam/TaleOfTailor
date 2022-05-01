using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDash : Character
{
    [Header("대쉬 스피드")]
    [SerializeField]
    private float smoothTime = 0.2f;

    [Header("대쉬 오브젝트 거리")]
    [SerializeField]
    private float DashObjectDistance;

    [Header("대쉬 오브젝트")]
    [SerializeField]
    private GameObject DashObjet;

    private Vector3 input;
    //private Vector3 lastMoveSpd;
    private bool firstbool = false;
    private bool dashbool = false;
    private bool stopbool = false;
    private Vector3 minusvec;
    private EventParam eventParam;

    private void Start()
    {
        EventManager.StartListening("INPUT", getInput);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashbool == false)
        {
            dashbool = true;
            firstbool = true;
            eventParam.boolParam = true;
            EventManager.TriggerEvent("ISDASH", eventParam);
        }

        if (dashbool)
        {
            Dash();
        }
    }
    private void Dash()
    {
        if (firstbool)
        {
            firstbool = false;
            DashObjet.transform.position = new Vector3(input.x * DashObjectDistance + transform.position.x, 0, input.y * DashObjectDistance + transform.position.z);
        }

        //Vector3 smoothPosition = Vector3.SmoothDamp(
        //    transform.position,
        //    DashObjet.transform.position,
        //    ref lastMoveSpd,
        //    smoothTime
        //    );

        Vector3 smoothPosition = Vector3.Lerp(transform.position, DashObjet.transform.position, smoothTime * Time.deltaTime);

        Vector3 dirction = transform.position - DashObjet.transform.position;

        if (Mathf.RoundToInt(dirction.magnitude) > 0 && !stopbool)
        {
            transform.position = smoothPosition;
        }
        else if (stopbool)
        {
            dashbool = false;
            eventParam.boolParam = false;
            EventManager.TriggerEvent("ISDASH", eventParam);
            stopbool = false;
            return;
        }
        else
        {
            dashbool = false;
            eventParam.boolParam = false;
            EventManager.TriggerEvent("ISDASH", eventParam);
            return;
        }
    }

    private void getInput(EventParam eventParam)
    {
        input = eventParam.vectorParam;
    }

    private void OnCollisionEnter(Collision collision)
    {
        minusvec = Mathf.Abs(transform.position.x) > Mathf.Abs(transform.position.z) ? new Vector3(1, 0, 0) : new Vector3(0, 0, 1);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag != "Floor")
        {
            stopbool = true;
            transform.position -= minusvec;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        stopbool = false;
    }
}
