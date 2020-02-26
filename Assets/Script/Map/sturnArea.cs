using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sturnArea : MonoBehaviour
{



	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")

		{
		
			float backDis = 1f;
			//Vector3 postMovePos = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
			//Vector3 moveDir = other.transform.position - postMovePos;
			//moveDir = moveDir.normalized;
			//moveDir.y = 0;


			//Vector3 moveDir = other.transform.position - GetPointOfContact(other);	//간 곳의 반대방향으로 튕기나 오른쪽 구석으로 이동시 밖으로 나가는 현상 발생
			Vector3 moveDir = Vector3.zero - other.transform.position; //원점으로 땡긴다.
			moveDir = moveDir.normalized;

			

			other.transform.position = new Vector3(other.transform.position.x + moveDir.x * backDis, other.transform.position.y, other.transform.position.z + moveDir.z * backDis);
			other.gameObject.GetComponent<Player>().Stun();

		}
		


	}

	private Vector3 GetPointOfContact(Collider other)
	{
		RaycastHit hit;
		if (Physics.Raycast(other.transform.position, other.transform.forward, out hit))
		{
			//Debug.DrawLine(transform.position, transform.forward, Color.blue);
			return hit.point;
		}

		return transform.position;
	}
}
