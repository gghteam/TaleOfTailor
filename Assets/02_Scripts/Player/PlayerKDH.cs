using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerKDH : MonoBehaviour
{
    protected Rigidbody rigid = null;
    protected Collider col = null;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
}
