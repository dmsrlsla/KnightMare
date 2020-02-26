using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InterObject {

	protected new void Awake()
	{
		//GameObject OpenChest = Resources.Load("Prefabs/Objects/chest_open") as GameObject;
		base.Awake();		
	}



	protected override void Break()
	{
		if (BoardManager.Instance != null)
			BoardManager.Instance.ClearBoard(this);

		Debug.Log("Break!");
		GameObject OpenChest = Resources.Load("Prefabs/Objects/Chest_Open") as GameObject;
		gameObject.GetComponentInChildren<MeshFilter>().mesh = OpenChest.GetComponentInChildren<MeshFilter>().sharedMesh;

		if(Random.Range(0, 2)==-1)
		InGameUI.Instance.showBasicStatusUp();

		else
		{
			ItemManager.Instance.GetItem();
			InGameUI.Instance.showInventory();
		}

	}
}
