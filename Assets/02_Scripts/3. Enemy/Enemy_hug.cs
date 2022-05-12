using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_hug : MonoBehaviour
{
	[SerializeField]
	private GameObject playerObject;

	private Animator ani;
	//잡는 시기 변수
	private bool huging = false;
	//잡혔는지 안잡혔는지 변수
	private bool isfirst = true;
	//어 그정도의 바운더안에 들어오면 

	private void Start()
	{
		ani = GetComponent<Animator>();
	}

	private void Update()
	{
		Debug.Log(a());
		if (a() < 1.5f && !ani.GetBool("hug"))
		{
			//애니메이션을 플레이 시켜준다.
			ani.SetBool("hug", true);
			isfirst = true;
		}
		Hug();
	}


	private void Hug()
	{
		if (ani.GetBool("hug") && (ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f))
		{
			huging = true;
		}

		if (!isfirst)
		{
			ani.SetBool("hug", false);
		}
		//하다가 일정 시간 이상 안맞았을때 플레이를 그만 해주고
		//아니면 계속 플레이해준다.
		//맞았으면 멈춰주고 플레이어는 버둥된다.
		//그리고 플레이어를 가운데에 배치하고 따라가게한다.
		//그거 그대로 플레이어를 올려주고
		//그 후 hp를 깍는다.
	}

	private void OnTriggerStay(Collider other)
	{
		if (huging && isfirst)
		{
			isfirst = false;
			//여기서 해주고
		}
	}


	private float a()
	{
		return (playerObject.transform.position - transform.position).magnitude;
	}
}