using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleAttackSKill : BaseSkill {

    float StackTime = 0;

    eTeamType casterCharacterTeam;
    Actor casterActor;
    GiantEnemy casterAIScript;

    public override void InitSkill()
    {
        casterCharacterTeam = (eTeamType)OWNER.GetComponent<BaseObject>().GetData(ConstValue.ActorData_Team);
        casterActor = (Actor)OWNER.GetComponent<BaseObject>().GetData(ConstValue.ActorData_GetThisActor);
        casterAIScript = casterActor.GetComponentInChildren<GiantEnemy>();

        TEMP_OFF = false;
    }

    public override void UpdateSkill()
    {
        StackTime += Time.deltaTime;
        if (casterAIScript.FINAL_ATTACK == false)
        {
            if (StackTime >= 1.21f)
            {
                casterAIScript.FINAL_ATTACK = true;
                END = true;
            }
            else if (StackTime >= 1.18f)
                TEMP_OFF = false;
            else if (StackTime >= 1.03f)
                TEMP_OFF = true;
        }
        else
        {
            if (StackTime >= 0.2f)
            {
                casterAIScript.FINAL_ATTACK = false;
                END = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (END == true)
            return;

		if (other.gameObject.GetComponent<TeamObject>().TEAM_TYPE != casterCharacterTeam
			&& TEMP_OFF == false)
		{
			GameObject colObject = other.gameObject;
			BaseObject actorObject = colObject.GetComponent<BaseObject>();

			TeamObject Target = other.gameObject.GetComponent<TeamObject>();
			casterActor.ThrowEvent(ConstValue.ActorData_SetTarget, Target);

			//스킬이 생성될 때 타켓을 정해주는데, throw이벤트로 타겟을 정해주는 것은 그 후임.
			SkillManager.Instance.makeSkill.TARGET = SkillManager.Instance.makeSkill.OWNER.GetData(ConstValue.ActorData_GetTarget) as BaseObject;


			if (actorObject != TARGET)
				return;

			TARGET.ThrowEvent(ConstValue.EventKey_Hit,
						OWNER.GetData(ConstValue.ActorData_Character),
						SKILL_TEMPLATE, casterActor.CUR_BUFF, casterActor.transform.rotation);
		}
    }
}
