using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDash : Character
{
	[Header("�뽬 �ӵ�")]
	[SerializeField]
	private float smoothTime = 0.2f;

	[Header("�뽬 �Ÿ�")]
	[SerializeField]
	private float DashDistance;

	[Header("�뽬 ������Ʈ")]
	[SerializeField]
	private GameObject DashObjet;

	[Header("���̾� ���� �ϼ���")]
	[SerializeField]
	LayerMask layer;

	[Header("ī�޶� ������Ʈ")]
	[SerializeField]
	private Transform cameraObject;

	private Vector3 dashVec = Vector3.zero;

	private Vector3 input;
	private bool firstbool = false;
	private bool dashbool = false;
	private EventParam eventParam;

	private void Start()
	{
		EventManager.StartListening("INPUT", getInput);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && dashbool == false)
		{
			dashbool = true;
			firstbool = true;
			eventParam.boolParam = true;
			EventManager.TriggerEvent("ISDASH", eventParam);
			ani.SetBool("IsDash", dashbool);
		}

		if (dashbool)
		{
			Dash();
		}

		Debug.DrawLine(transform.position, dashVec);
	}
	private void Dash()
	{
		if (firstbool)
		{
			RaycastHit ray;

			Vector3 dir = (transform.localRotation * Vector3.forward);

			dashVec = new Vector3(dir.x * DashDistance + transform.position.x, transform.position.y, dir.z * DashDistance + transform.position.z);
			if (Physics.Raycast(transform.position, dashVec, out ray, DashDistance,layer))
			{
				float vec = (transform.position - ray.point).magnitude;
				dashVec = new Vector3(input.x * vec + transform.position.x, transform.position.y, input.y * vec + transform.position.z);
			}
			firstbool = false;
		}

		//Vector3 smoothPosition = Vector3.SmoothDamp(
		//    transform.position,
		//    DashObjet.transform.position,
		//    ref lastMoveSpd,
		//    smoothTime
		//    );

		Vector3 smoothPosition = Vector3.Lerp(transform.position, dashVec, smoothTime * Time.deltaTime);

		Vector3 dirction = transform.position - dashVec;

		if (Mathf.RoundToInt(dirction.magnitude) > 0)
		{
			transform.position = smoothPosition;
		}
		else
		{
			dashbool = false;
			firstbool = true;
			eventParam.boolParam = false;
			EventManager.TriggerEvent("ISDASH", eventParam);
			ani.SetBool("IsDash", dashbool);
		}
	}

	private void getInput(EventParam eventParam)
	{
		input = eventParam.vectorParam;
	}
}
