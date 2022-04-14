using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveKDH : PlayerKDH
{
    [Header("플레이어 움직임 속도")]
    [SerializeField]
    private float PlayerSpeed;

    private float Horizontal;
    private float Vertical;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector3(Horizontal * PlayerSpeed, rigid.velocity.y, Vertical * PlayerSpeed);

    }
}
