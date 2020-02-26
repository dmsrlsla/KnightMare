using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSkill : BaseSkill {

	float StackTime = 0;
	Actor casterActor;

	public override void InitSkill()
	{
		casterActor = OWNER.GetData(ConstValue.ActorData_GetThisActor) as Actor;
	}

	public override void UpdateSkill()
	{
		StackTime += Time.deltaTime;

		if (casterActor.GetComponent<NonPlayer>().AI.IS_SKILL == false)
			END = true;

		if (StackTime >= 2.0f)
		{
			END = true;
			casterActor.GetComponent<NonPlayer>().AI.IS_SKILL = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (END == true)
			return;

		eTeamType casterCharacterTeam = (eTeamType)OWNER.GetData(ConstValue.ActorData_Team);

		if (casterActor.IS_SUPERARMOR == false)
			return;

		if (other.gameObject.GetComponent<TeamObject>().TEAM_TYPE
			!= casterCharacterTeam)
		{
			GameObject colObject = other.gameObject;
			BaseObject actorObject = colObject.GetComponent<BaseObject>();


			// 플레이어 차징어택 정지.
			
				if (other.tag == "Player")

				{
					Vector3 moveDir = other.transform.position - gameObject.transform.position;
					other.gameObject.GetComponent<Player>().Stun();
				}
				

			
		}
	}
}
