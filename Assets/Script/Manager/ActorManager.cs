using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoSingleton<ActorManager>
{
	int diedEnemyNum = 0;
	bool stageClear = false;


	//public int DIED_ENEMY_NUM
	//{
	//	get
	//	{
	//		return diedEnemyNum;
	//	}
	//}

    // 하이라키 관리용
    Transform ActorRoot = null;

    // 모든 엑터 관리
    Dictionary<eTeamType, List<Actor>> DicActor =
        new Dictionary<eTeamType, List<Actor>>();

    // 몬스터 프리팹 관리
    Dictionary<eEnemyType, GameObject> DicMonsterPrefab =
        new Dictionary<eEnemyType, GameObject>();

    private void Awake()
    {
      //  MonsterPrefabInit();
    }

    void MonsterPrefabInit()
    {
        for (int i = 0; i < (int)eEnemyType.MAX; i++)
        {
            GameObject go = Resources.Load("Prefabs/" +
                ((eEnemyType)i).ToString("F")) as GameObject;
            if (go == null)
            {
                Debug.LogError(((eEnemyType)i).ToString("F") +
                    " 로드 실패 ");
            }
            else
            {
                DicMonsterPrefab.Add((eEnemyType)i, go);
            }
        }
    }

    public GameObject GetMonsterPrefab(eEnemyType type)
    {
        if (DicMonsterPrefab.ContainsKey(type) == true)
        {
            return DicMonsterPrefab[type];
        }
        else
        {
            Debug.LogError(type.ToString() + " 타입의 몬스터 프리팹이 없습니다.");
            return null;
        }
    }

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

    public void AddActor(Actor actor)
    {
        List<Actor> listActor = null;
        eTeamType teamType = actor.TEAM_TYPE;

        // 리스트 생성 또는 로드
        if (DicActor.ContainsKey(teamType) == false)
        {
            listActor = new List<Actor>();
            DicActor.Add(teamType, listActor);
        }
        else
        {
            DicActor.TryGetValue(teamType, out listActor);
        }

        listActor.Add(actor);
    }

    public void RemoveActor(Actor actor, bool bDelete = false)
    {
        eTeamType teamType = actor.TEAM_TYPE;
		List<Actor> listActor = null;

		if (DicActor.ContainsKey(teamType) == true)
        {
			//List<Actor> listActor = null;
			DicActor.TryGetValue(teamType, out listActor);
            listActor.Remove(actor);
        }
        else
        {
            Debug.LogError("존재 하지 않는 엑터를 삭제하려고 합니다.");
        }

        if (bDelete)
            Destroy(actor.gameObject);

		//0703 교준, 죽인 적에 따라 포션의 양을 늘리기 위해
		((Player)GameManager.Instance.PlayerActor).PLAYER_POTION.PotionAmount();

		//0704 교준, 게임 클리어시를 찾기 위해
		for(int i=0;i<listActor.Count;i++)
		{
			if (listActor[i].TEAM_TYPE != eTeamType.TEAM_1)
				return;

		
		}

		//
		//GameManager.Instance.GAME_CLEAR = true;
		Debug.Log("Clear");
		GameManager.Instance.GameClear();

	}

    public BaseObject GetSearchEnemy(BaseObject actor, float radius = 100.0f)
    {
        eTeamType teamType =
            (eTeamType)actor.GetData(ConstValue.ActorData_Team);
        Vector3 myPosition = actor.SelfTransform.position;

        float nearDistance = radius;
        Actor nearActor = null;

        foreach (KeyValuePair<eTeamType, List<Actor>> pair in DicActor)
        {
            if (pair.Key == teamType)
                continue;

            for (int i = 0; i < pair.Value.Count; i++)
            {
                if (pair.Value[i].SelfObject.activeSelf == false)
                    continue;

                if (pair.Value[i].OBJECT_STATE == eBaseObjectState.STATE_DIE)
                    continue;

                float distance = Vector3.Distance(
                    myPosition,
                    pair.Value[i].SelfTransform.position);
                if (distance < nearDistance)
                {
                    nearDistance = distance;
                    nearActor = pair.Value[i];
                }
            }
        }
        return nearActor;
    }

    //-----------------------------------------------
    // TestCode
    public BaseObject GetSearchEnemy(
        BaseObject actor, out float returnDist, float radius = 100.0f)
    {
        eTeamType teamType =
            (eTeamType)actor.GetData(ConstValue.ActorData_Team);
        Vector3 myPosition = actor.SelfTransform.position;

        float nearDistance = radius;
        Actor nearActor = null;
        returnDist = 0;

        foreach (KeyValuePair<eTeamType, List<Actor>> pair in DicActor)
        {
            if (pair.Key == teamType)
                continue;

            for (int i = 0; i < pair.Value.Count; i++)
            {
                if (pair.Value[i].SelfObject.activeSelf == false)
                    continue;

                if (pair.Value[i].OBJECT_STATE == eBaseObjectState.STATE_DIE)
                    continue;

                float distance = Vector3.Distance(
                    myPosition,
                    pair.Value[i].SelfTransform.position);
                if (distance < nearDistance)
                {
                    nearDistance = distance;
                    nearActor = pair.Value[i];

                    returnDist = nearDistance;
                }
            }
        }
        return nearActor;
    }
	//-----------------------------------------------
	//교준
	public Actor PlayerLoad()
	{
		GameObject playerPrefab = Resources.Load("Prefabs/"
			+ "Actor/"
			+ "Player") as GameObject;
		//플레이어 로드시 앞으로 스킬정보를 자기가 가져감
        GameObject SkillPrefab = Instantiate(Resources.Load("Prefabs/Combo/SS")) as GameObject;

        GameObject go = Instantiate(playerPrefab,
			Vector3.zero,
			Quaternion.identity) as GameObject;

        go.GetComponent<Player>().ComboSkillSet = SkillPrefab.GetComponent<ComboSkill>();

        return go.GetComponent<Actor>();
	}
	//미니보스 구현
    public Actor MiniBossLoad(Vector3 EggPosition)
    {
        //추후 3종류 구현
        int RandBossColor = Random.Range(0, 4);
        GameObject MiniBossPrefab = null ;
        switch (RandBossColor)
        {
            case 0:
                MiniBossPrefab = Resources.Load("Prefabs/"
                    + "Actor/"
                    + "Mini_Bule_Boss_Enemy") as GameObject;
                break;
            case 1:
                MiniBossPrefab = Resources.Load("Prefabs/"
                    + "Actor/"
                    + "Mini_Gold_Boss_Enemy") as GameObject;
                break;
            case 2:
                MiniBossPrefab = Resources.Load("Prefabs/"
                    + "Actor/"
                    + "Mini_Green_Boss_Enemy") as GameObject;
                break;
            case 3:
                MiniBossPrefab = Resources.Load("Prefabs/"
                    + "Actor/"
                    + "Mini_Red_Boss_Enemy") as GameObject;
                break;
            default:
                break;
        }
        if (MiniBossPrefab == null)
            Debug.LogError("미니보스 없음");

        GameObject go = Instantiate(MiniBossPrefab,
            EggPosition,
            Quaternion.identity) as GameObject;

        return go.GetComponent<Actor>();
    }
}
