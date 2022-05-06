using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSetUp : MonoBehaviour
{
    private void OnEnable()
    {
        //Ŀ�� ����� �� ��ġ ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        //Ŀ�� ����� �� ��ġ ����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Cursor.visible = false;
        }
    }
}
