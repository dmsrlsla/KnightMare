using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : InterObject
{
	public float rotateSpeed = 300f;
	public float minusDegreeDrag = 4f;
	string typeOfBuff = string.Empty;
	eBuffType buffType = eBuffType.NONE;

	protected new void Awake()
	{
		int rand = Random.Range(1, 4);
		buffType = eBuffType.NONE;

		Color frameColor = Color.white;
		Color deerColor = Color.white;


		switch (rand)
		{
			case 1:
				buffType = eBuffType.ATTACK;
				frameColor = (Resources.Load("Materials/AttackStatue") as Material).color;
				deerColor = (Resources.Load("Materials/AttackStatue") as Material).color;
				break;
			case 2:
				buffType = eBuffType.DEFENCE;
				frameColor = (Resources.Load("Materials/DefenceStatue") as Material).color;
				deerColor = (Resources.Load("Materials/DefenceStatue") as Material).color;
				break;
			case 3:
				buffType = eBuffType.HEAL;
				frameColor = (Resources.Load("Materials/HealStatue") as Material).color;
				deerColor = (Resources.Load("Materials/HealStatue") as Material).color;
				//힐부분은 후에 HP Bar가 생기면 구현할 것.
				break;

			default:
				break;
		}

		

		gameObject.transform.Find("Model").Find("Frame").GetComponent<MeshRenderer>().material.color = frameColor;
		gameObject.transform.Find("Model").Find("Deer").GetComponent<MeshRenderer>().material.color = deerColor;
		//GameObject OpenChest = Resources.Load("Prefabs/Objects/chest_open") as GameObject;
		base.Awake();
	}




	protected override void Break()
	{
		if (BoardManager.Instance != null)
			BoardManager.Instance.ClearBoard(this);

		Debug.Log("Statue Break!");

		typeOfBuff = BuffManager.Instance.CreateBuff(buffType,1);
		GameManager.Instance.PlayerActor.CUR_BUFF = typeOfBuff;



		gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		StartCoroutine("StatueTurnAndDestroy");
	}

	IEnumerator StatueTurnAndDestroy()
	{
		
		while (gameObject.transform.position.y>-15f)
		{
		
			transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
			gameObject.GetComponent<Rigidbody>().drag -= Time.deltaTime* minusDegreeDrag;
			yield return null;
		}

		Destroy(gameObject);
	}
}
