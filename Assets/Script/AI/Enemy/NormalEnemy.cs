using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 건희 6/26 22:03 수정

public class NormalEnemy : BaseAI {
	
	BaseObject targetObject;

	protected override IEnumerator Idle()
	{
		// 근거리 적 탐색
		targetObject = ActorManager.Instance.GetSearchEnemy(Target, searchDis);

        if (targetObject != null)
		{
			SkillData sData =
				Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;
			float attackRange = 1f;

			if (sData != null)
				attackRange = sData.RANGE;

			float distance = Vector2.Distance(
				targetObject.SelfTransform.position,
				SelfTransform.position);

			if (distance > attackRange + 1f && IS_SKILL_COOLDOWN == false)
			{
				Stop();
				AddNextAI(eStateType.STATE_SPECIAL);
			}
			else if (distance < attackRange && IS_ATTACK_COOLDOWN == false)
			{
				Stop();
				AddNextAI(eStateType.STATE_ATTACK, targetObject);
			}
			else
			{
				AddNextAI(eStateType.STATE_WALK);
			}
		}
		else
		{
			AddNextAI(eStateType.STATE_IDLE);
        }

        yield return StartCoroutine(base.Idle());
	}

	protected override IEnumerator Move()
	{
		targetObject =
			ActorManager.Instance.GetSearchEnemy(Target, searchDis);

		if (targetObject != null)
		{
			SkillData sData =
				Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;
			float attackrange = 1f;

			if (sData != null)
				attackrange = sData.RANGE;

			float distance = Vector3.Distance(
				targetObject.SelfTransform.position,
				SelfTransform.position);

            if (distance > attackrange + 1f && IS_SKILL_COOLDOWN == false)
            {
				Stop();
				AddNextAI(eStateType.STATE_SPECIAL);
            }
			else if (distance < attackrange && IS_ATTACK_COOLDOWN == false)
			{
				Stop();
				AddNextAI(eStateType.STATE_ATTACK, targetObject);
			}
			else
			{
				SetMove(targetObject.SelfTransform.position);
			}
		}
		else
		{
			Stop();
			AddNextAI(eStateType.STATE_IDLE);
		}

		yield return StartCoroutine(base.Move());
	}

	protected override IEnumerator Attack()
	{
		Vector3 dis = (targetObject.transform.position - transform.parent.position).normalized;
		IS_ATTACK_COOLDOWN = true;
		Invoke("OffAttackCoolDown", 3f); // 시간부분은 나중에 JSON 공격속도 부분으로

		yield return new WaitForEndOfFrame();
		while(IS_ATTACK)
		{
			transform.parent.position += dis * 0.03f;
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				break;
			yield return new WaitForEndOfFrame(); // IDLE로 넘어가기 전에 이 애니메이션이 끝났는지 체크.
			// 코루틴은 따로돌기때문에 가능한 구성.
		}
		AddNextAI(eStateType.STATE_IDLE);

		yield return StartCoroutine(base.Attack());
	}

	protected override IEnumerator Die()
	{
		AddNextAI(eStateType.STATE_DEAD);
		Invoke("End", 4f);
		yield return StartCoroutine(base.Die());
	}

	void End()
	{
		END = true;
	}

	protected override IEnumerator Special()
	{
		NonPlayer casterActor
			= Target.GetComponent<NonPlayer>();

		casterActor.ThrowEvent(ConstValue.EventKey_SelectSkill, 
			eSkillTemplateType.TARGET_BLOCK);
		IS_SKILL = true;
		casterActor.RunSkill();
		IS_SKILL_COOLDOWN = true;
        Invoke("OffSkillCoolDown", 7f);

        yield return new WaitForEndOfFrame();
        while (IS_SKILL)
        {
            if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
                break;
            yield return new WaitForEndOfFrame();
        }
        AddNextAI(eStateType.STATE_IDLE);

		yield return StartCoroutine(base.Special());
	}

	void OffSkillCoolDown()
	{
		IS_SKILL_COOLDOWN = false;
	}

	void OffAttackCoolDown()
	{
		IS_ATTACK_COOLDOWN = false;
	}
}
