using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �÷��̾��� �������� �κ��� ����
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    protected Rigidbody rigidbody = null;
    protected Collider col = null;
    protected Animator ani = null;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        ani = GetComponent<Animator>();
    }
}
