using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 건희 6/26 22:15 수정

public class MeleeSkill : BaseSkill
{
    float StackTime = 0;
	float EndTime = 0;
	//Actor player;
	eTeamType casterCharacterTeam = eTeamType.TEAM_1;
	Actor casterActor = null;

	bool bGiantEnemy = false;
	bool bNormalEnemy = false;

	public override void InitSkill()
    {
		//Debug.LogError("");
		//player = GameManager.Instance.PlayerActor;
		casterCharacterTeam = (eTeamType)OWNER.GetComponent<BaseObject>().GetData(ConstValue.ActorData_Team);
		casterActor = OWNER.GetComponent<BaseObject>().GetData(ConstValue.ActorData_GetThisActor) as Actor;

		switch (casterActor.TEMPLATE_KEY)
		{
			case "CHARACTER_1":
				{
					EndTime = 0.5f;
				}
				break;	
			case "ENEMY_1":
				{
					EndTime = 0.5f;
					bNormalEnemy = true;
				}
				break;
			case "ENEMY_3":
				{
					EndTime = 0.05f;
					bGiantEnemy = true;
				}
				break;
		}
	}
    //시간에 따라 움직임(거리 받을 필요 없음)
    public override void UpdateSkill()
    {
        StackTime += Time.deltaTime;
        if (StackTime >= EndTime)
            END = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (END == true)
            return;

		if (other.gameObject.tag != "Obstacle")
		{
			if (other.gameObject.GetComponent<Actor>().TEMPLATE_KEY == "ENEMY_1")
			{
				other.gameObject.GetComponent<NonPlayer>().AI.IS_SKILL = false;
			}
		}

		if (other.gameObject.GetComponent<TeamObject>().TEAM_TYPE != casterCharacterTeam
			|| (bGiantEnemy &&
			other.gameObject.GetComponent<Actor>() != casterActor))
		{
			GameObject colObject = other.gameObject;
			BaseObject actorObject = colObject.GetComponent<BaseObject>();

			TeamObject Target = other.gameObject.GetComponent<TeamObject>();
			casterActor.ThrowEvent(ConstValue.ActorData_SetTarget, Target);

			//스킬이 생성될 때 타켓을 정해주는데, throw이벤트로 타겟을 정해주는 것은 그 후임.
			SkillManager.Instance.makeSkill.TARGET = SkillManager.Instance.makeSkill.OWNER.GetData(ConstValue.ActorData_GetTarget) as BaseObject;


			if (actorObject != TARGET)
				return;


			//Destroy(other.gameObject);

				TARGET.ThrowEvent(ConstValue.EventKey_Hit,
					OWNER.GetData(ConstValue.ActorData_Character),
					SKILL_TEMPLATE, casterActor.CUR_BUFF, casterActor.transform.rotation);
		}
	}
}
