using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Attack : FsmState
{
    [SerializeField]
    private float time;
    protected float currentTime;

    [SerializeField]
    private GameObject targetPos;

    private Vector3 temp;
    private Vector3 dir;
    private Vector3 target;

    bool isStart = false;
    bool isPlay = true;

    private Animator animator;

    private FsmCore fsmCore;
    private Chase chaseState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        fsmCore = GetComponent<FsmCore>();
        chaseState = GetComponent<Chase>();
    }

    private void Update()
    {
        /*
       //Debug.Log(Vector3.Distance(transform.position, targetPos.position));
        if (Vector3.Distance(transform.position, targetPos.position) >= 10 && !isStart)
        {
            temp = transform.position;
            target = targetPos.position;
            dir = (targetPos.position - transform.position).normalized;

            isStart = true;
            isPlay = true;
            animator.SetTrigger("JumpAttack");

            Debug.Log("�� ����");
        }
        */
        if (isPlay)
        {
            currentTime += Time.deltaTime;
            transform.LookAt(targetPos.transform);

            if (currentTime <= time)
            {
                transform.position = MathParabola.Parabola(temp, targetPos.transform.position - dir * 2.5f, 3.5f, currentTime / time);
            }
            else
            {
                Vector3 pos = targetPos.transform.position - dir * 2.5f;
                //pos.y = 0;
                transform.position = pos;
                isPlay = false;
            }
        }

        if(!isPlay)
        {
            Debug.Log("�ӿ���Ӥä�");
            fsmCore.ChangeState(chaseState);
            isPlay = true;
        }
        
    }

    public override void OnStateEnter()
    {
        Debug.Log("������");
        temp = transform.position;
        target = targetPos.transform.position;
        Debug.Log(target);
        dir = (target - transform.position).normalized;

        currentTime = 0;
        isStart = true;
        isPlay = true;
        //animator.SetTrigger("JumpAttack");
    }

    public override void OnStateLeave()
    {
        isStart = false;
        isPlay = false;
        Debug.Log("������!");
    }
}