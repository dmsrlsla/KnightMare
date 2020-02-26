using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemy : BaseAI {

	BaseObject targetObject;

    bool bFinalAttack = false;
    public bool FINAL_ATTACK
    {// 3타중 마지막 공격 중인지.
        get { return bFinalAttack; }
        set { bFinalAttack = value; }
    }

	protected override IEnumerator Idle()
	{
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

			if (distance < attackRange + 4f && IS_SKILL_COOLDOWN == false)
			{
				Stop();
				AddNextAI(eStateType.STATE_SPECIAL, targetObject);
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

			if (distance < attackrange + 4f && IS_SKILL_COOLDOWN == false)
			{
				Stop();
				AddNextAI(eStateType.STATE_SPECIAL, targetObject);
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
		IS_ATTACK_COOLDOWN = true;
		Invoke("OffAttackCoolDown", 1f);

		yield return new WaitForEndOfFrame();
		while (IS_ATTACK)
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				break;
			yield return new WaitForEndOfFrame(); // IDLE로 넘어가기 전에 이 애니메이션이 끝났는지 체크.
												  // 코루틴은 따로돌기때문에 가능한 구성.
		}
		AddNextAI(eStateType.STATE_IDLE);

		yield return StartCoroutine(base.Attack());
	}
	void OffAttackCoolDown()
	{
		IS_ATTACK_COOLDOWN = false;
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
}
