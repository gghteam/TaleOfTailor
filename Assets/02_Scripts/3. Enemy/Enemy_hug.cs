using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_hug : MonoBehaviour
{
	[SerializeField]
	private GameObject playerObject;

	private Animator ani;
	//��� �ñ� ����
	private bool huging = false;
	//�������� ���������� ����
	private bool isfirst = true;
	//�� �������� �ٿ���ȿ� ������ 

	private void Start()
	{
		ani = GetComponent<Animator>();
	}

	private void Update()
	{
		Debug.Log(a());
		if (a() < 1.5f && !ani.GetBool("hug"))
		{
			//�ִϸ��̼��� �÷��� �����ش�.
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
		//�ϴٰ� ���� �ð� �̻� �ȸ¾����� �÷��̸� �׸� ���ְ�
		//�ƴϸ� ��� �÷������ش�.
		//�¾����� �����ְ� �÷��̾�� ���յȴ�.
		//�׸��� �÷��̾ ����� ��ġ�ϰ� ���󰡰��Ѵ�.
		//�װ� �״�� �÷��̾ �÷��ְ�
		//�� �� hp�� ��´�.
	}

	private void OnTriggerStay(Collider other)
	{
		if (huging && isfirst)
		{
			isfirst = false;
			//���⼭ ���ְ�
		}
	}


	private float a()
	{
		return (playerObject.transform.position - transform.position).magnitude;
	}
}