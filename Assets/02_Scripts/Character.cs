using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어의 공통적인 부분을 추출
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
