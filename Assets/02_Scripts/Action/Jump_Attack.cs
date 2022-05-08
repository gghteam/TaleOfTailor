using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Attack : MonoBehaviour
{
    [SerializeField]
    private float time;
    protected float currentTime;

    [SerializeField]
    private Transform targetPos;

    private Vector3 temp;
    private Vector3 dir;
    private Vector3 target;

    bool isStart = false;
    bool isPlay = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       //Debug.Log(Vector3.Distance(transform.position, targetPos.position));
        if (Vector3.Distance(transform.position, targetPos.position) >= 10 && !isStart)
        {
            temp = transform.position;
            target = targetPos.position;
            dir = (targetPos.position - transform.position).normalized;

            isStart = true;
            isPlay = true;
            animator.SetTrigger("JumpAttack");

            Debug.Log("Á» °¡¶ó");
        }

        if (isPlay)
        {
            currentTime += Time.deltaTime;

            if (currentTime <= time)
                transform.position = MathParabola.Parabola(temp, targetPos.position - dir, 3.5f, currentTime / time);
            else
            {
                Vector3 pos = targetPos.position - dir;
                //pos.y = 0;
                transform.position = pos;
                isPlay = false;
            }
        }
    }
}