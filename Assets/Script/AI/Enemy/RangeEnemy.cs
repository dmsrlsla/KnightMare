using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : BaseAI {

	BaseObject targetObject;

	float dodgeDis = 5f;

	protected override IEnumerator Idle()
	{
		searchDis = 30f;
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

			if (distance <= dodgeDis && IS_SKILL_COOLDOWN == false)
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
				AddNextAI(eStateType.STATE_IDLE);
			}
		}
		else
		{
			AddNextAI(eStateType.STATE_IDLE);
		}

		yield return StartCoroutine(base.Idle());
	}

	//protected override IEnumerator Move()
	//{
	//	targetObject =
	//		ActorManager.Instance.GetSearchEnemy(Target, searchDis);

	//	if (targetObject != null)
	//	{
	//		SkillData sData =
	//			Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;
	//		float attackRange = 1f;

	//		if (sData != null)
	//			attackRange = sData.RANGE;

	//		float distance = Vector3.Distance(
	//			targetObject.SelfTransform.position,
	//			SelfTransform.position);

	//		if (distance >= dodgeDis && IS_SKILL_COOLDOWN == false)
	//		{
	//			Stop();
	//			AddNextAI(eStateType.STATE_SPECIAL);
	//		}
	//		else if (distance < attackRange && IS_ATTACK_COOLDOWN == false)
	//		{
	//			Stop();
	//			AddNextAI(eStateType.STATE_ATTACK, targetObject);
	//		}
	//		else
	//		{
	//			SetMove(targetObject.SelfTransform.position);
	//		}
	//	}
	//	else
	//	{
	//		Stop();
	//		AddNextAI(eStateType.STATE_IDLE);
	//	}

	//	yield return StartCoroutine(base.Move());
	//}

	protected override IEnumerator Attack()
	{
		//Target.ThrowEvent(ConstValue.EventKey_SelectSkill,
		//	eSkillTemplateType.RANGE_ATTACK, targetObject);

		//if (IS_FIRE_SKILL == false)
		//{
		//	IS_ATTACK_COOLDOWN = true;
		//	Invoke("OffAttackCoolDown", 2f); // 시간부분은 나중에 JSON 공격속도 부분으로
		//}
		//if (IS_FIRE_SKILL)
		//{
		//	bFireDelay = true;
		//	Invoke("OffFireDelay", 0.5f);

		//	if (IS_FIRE_SKILL == true && IS_SKILL_COOLDOWN == false)
		//	{
		//		IS_SKILL_COOLDOWN = true;
		//		Invoke("OffSkillCoolDown", 8f);
		//	}

		//	if (FireCnt < 3)
		//	{
		//		FireCnt++;
		//		yield return new WaitForEndOfFrame();
		//		while(bFireDelay)
		//		{
		//			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
		//				break;
		//			yield return new WaitForEndOfFrame();
		//		}
		//		AddNextAI(eStateType.STATE_ATTACK, targetObject);
		//		//yield return StartCoroutine(base.Attack());
		//	}
		//	else
		//	{
		//		IS_FIRE_SKILL = false;
		//		FireCnt = 0;
		//	}
		//}

		IS_ATTACK_COOLDOWN = true;
		Invoke("OffAttackCoolDown", 2f); // 시간부분은 나중에 JSON 공격속도 부분으로

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
		int DirRandumNum = Random.Range(1, 4);
		Vector3 RollDir = (transform.parent.position - 
			targetObject.transform.position).normalized;

		IS_SKILL_COOLDOWN = true;
		Invoke("OffSkillCollTime", 4f);

		//switch (DirRandumNum)
		//{
		//	case 1:
		//		{
		//			RollDir = (transform.parent.position - targetObject.transform.position).normalized;
		//		}
		//		break;
		//	case 2:
		//		{
		//			RollDir = Vector3.back;
		//		}
		//		break;
		//	case 3:
		//		{
		//			RollDir = Vector3.right + Vector3.back;
		//		}
		//		break;
		//}

		SelfTransform.forward = RollDir;

		yield return new WaitForEndOfFrame();
		while (IS_SKILL)
		{
			SelfTransform.position += RollDir * 0.035f;
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				break;
			yield return new WaitForEndOfFrame();
		}
		AddNextAI(eStateType.STATE_IDLE);

		yield return StartCoroutine(base.Special());
	}
	void OffSkillCollTime()
	{
		IS_SKILL_COOLDOWN = false;
	}
}
