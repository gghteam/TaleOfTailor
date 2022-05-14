using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_hug : MonoBehaviour
{
	[SerializeField]
	private GameObject playerObject;

	[SerializeField]
	private Transform leftHand;
	[SerializeField]
	private Transform rightHand;

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
		if (a() < 1.5f && !ani.GetBool("IsHug"))
		{
			//�ִϸ��̼��� �÷��� �����ش�.
			ani.SetBool("IsHug", true);
			isfirst = true;
		}
		Hug();
	}


	private void Hug()
	{
		if (ani.GetBool("IsHug") && (ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f))
		{
			huging = true;
		}
		else if (ani.GetBool("IsHug") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && isfirst)
		{
			ani.SetBool("IsHug", false);
		}
		else if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		{
			isfirst = true;
			playerObject.transform.position = new Vector3(playerObject.transform.position.x, 1, playerObject.transform.position.z);
			//������ �ް��ϰ�
		}
		else if (!isfirst)
		{
			playerObject.transform.position = leftHand.transform.position;
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
		if (huging && isfirst && other.CompareTag("Player"))
		{
			isfirst = false;
		}
	}


	private float a()
	{
		return (playerObject.transform.position - transform.position).magnitude;
	}
}