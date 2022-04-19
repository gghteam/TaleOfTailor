using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �÷��̾��� �������� �κ��� ����
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    protected Rigidbody rigidbody = null;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}
