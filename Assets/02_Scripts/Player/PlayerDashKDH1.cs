using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDashKDH1 : PlayerKDH
{
    [Header("�뽬�� �ɸ��� �ӵ�")]
    [SerializeField]
    private float DashWaitSpeed;

    [Header("�뽬 �ӵ�")]
    [SerializeField]
    private float DashSpeed;

    [Header("�뽬 ������Ʈ �Ÿ�")]
    [SerializeField]
    private float DashObjectDistance;

    [Header("�뽬 ������Ʈ")]
    [SerializeField]
    private GameObject DashObjet;

    private bool firstbool = false;
    private bool dashbool = false;

    private float time = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && dashbool == false)
        {
            StartCoroutine("DashBool");
        }

        if(dashbool)
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
        if(firstbool)
        {
            firstbool = false;
            DashObjet.transform.position = new Vector3(Input.GetAxisRaw("Horizontal") * DashObjectDistance, 0, Input.GetAxisRaw("Vertical") * DashObjectDistance);
        }
        transform.position = Vector3.Lerp(transform.position, DashObjet.transform.position, Time.deltaTime*DashSpeed);
    }
}
