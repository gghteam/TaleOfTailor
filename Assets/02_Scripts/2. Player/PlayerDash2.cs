using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash2 : Character
{
    [Header("�뽬 ���ǵ�")]
    [SerializeField]
    private float smoothTime = 0.2f;

    [Header("�뽬 �ð�")]
    [SerializeField]
    private float dashtime = 0f;

    private EventParam eventParam;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventParam.intParam = (int)smoothTime;
            eventParam.boolParam = true;
            EventManager.TriggerEvent("ISDASH", eventParam);
            StartCoroutine(StartDash());
        }
    }


    private IEnumerator StartDash()
    {
        yield return new WaitForSeconds(dashtime);
        eventParam.boolParam = false;
        eventParam.boolParam2 = false;
        EventManager.TriggerEvent("ISDASH", eventParam);
    }
}