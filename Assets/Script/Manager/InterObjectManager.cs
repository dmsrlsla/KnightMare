using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObjectManager : MonoSingleton<InterObjectManager>
{
	// 하이라키 관리용
	Transform ActorRoot = null;

	// 모든 엑터 관리
	Dictionary<eTeamType, List<InterObject>> DicInterObject =
		new Dictionary<eTeamType, List<InterObject>>();

	// 몬스터 프리팹 관리
	Dictionary<eEnemyType, GameObject> DicMonsterPrefab =
		new Dictionary<eEnemyType, GameObject>();

	private void Awake()
	{
		//  MonsterPrefabInit();
	}

	//void MonsterPrefabInit()
	//{
	//	for (int i = 0; i < (int)eEnemyType.MAX; i++)
	//	{
	//		GameObject go = Resources.Load("Prefabs/" +
	//			((eEnemyType)i).ToString("F")) as GameObject;
	//		if (go == null)
	//		{
	//			Debug.LogError(((eEnemyType)i).ToString("F") +
	//				" 로드 실패 ");
	//		}
	//		else
	//		{
	//			DicMonsterPrefab.Add((eEnemyType)i, go);
	//		}
	//	}
	//}

	//public GameObject GetMonsterPrefab(eEnemyType type)
	//{
	//	if (DicMonsterPrefab.ContainsKey(type) == true)
	//	{
	//		return DicMonsterPrefab[type];
	//	}
	//	else
	//	{
	//		Debug.LogError(type.ToString() + " 타입의 몬스터 프리팹이 없습니다.");
	//		return null;
	//	}
	//}

	public Actor InstantiateOnce(GameObject prefab, Vector3 pos)
	{
		if (prefab == null)
		{
			Debug.LogError(
				"프리팹이 null 입니다. [ActorManager.InstantiateOnce()]");
			return null;
		}

		GameObject go =
			Instantiate(prefab, pos, Quaternion.identity) as GameObject;

		if (ActorRoot == null)
		{
			GameObject temp = new GameObject();
			temp.name = "ActorRoot";
			ActorRoot = temp.transform;
		}

		go.transform.SetParent(ActorRoot);
		return go.GetComponent<Actor>();
	}

	public void AddObject(InterObject interObject)
	{
		List<InterObject> listInterObject = null;
		eTeamType teamType = interObject.TEAM_TYPE;

		// 리스트 생성 또는 로드
		if (DicInterObject.ContainsKey(teamType) == false)
		{
			listInterObject = new List<InterObject>();
			DicInterObject.Add(teamType, listInterObject);
		}
		else
		{
			DicInterObject.TryGetValue(teamType, out listInterObject);
		}

		listInterObject.Add(interObject);
	}

	public void RemoveObject(InterObject interObject, bool bDelete = false)
	{
		eTeamType teamType = interObject.TEAM_TYPE;

		if (DicInterObject.ContainsKey(teamType) == true)
		{
			List<InterObject> listInterObject = null;
			DicInterObject.TryGetValue(teamType, out listInterObject);
			listInterObject.Remove(interObject);
		}
		else
		{
			Debug.LogError("존재 하지 않는 엑터를 삭제하려고 합니다.");
		}

		if (bDelete)
			Destroy(interObject.gameObject);
	}


}
