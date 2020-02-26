using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttackAnimation : StateMachineBehaviour {

	float CurTime = 0f;

	NonPlayer TargetActor = null;
	BaseObject TargetObject = null;
	bool bIsAttack = false;

	Vector3 SelfPos = Vector3.zero;
	Vector3 TargetPos = Vector3.zero;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		TargetActor = animator.GetComponentInParent<NonPlayer>();
		if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
		{
			TargetActor.AI.IS_SKILL = true;
			bIsAttack = false;
			CurTime = 0f;
		}

		TargetObject = (BaseObject)(TargetActor.GetData(ConstValue.ActorData_GetTarget));

		Vector3 targetPos = TargetObject.SelfTransform.position;
		SelfPos = TargetActor.SelfTransform.position;

		Vector3 norPos = (SelfPos - targetPos).normalized * 2f;

		TargetPos = targetPos + norPos;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		CurTime += Time.deltaTime;

        if (animatorStateInfo.normalizedTime >= 1.0f
            && TargetActor.AI.IS_SKILL)
        {
            if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
                TargetActor.AI.IS_SKILL = false;
        }

        if (bIsAttack == false
			&& animatorStateInfo.normalizedTime >= 0.5f)
		{
			bIsAttack = true;
			TargetActor.ThrowEvent(ConstValue.EventKey_SelectSkill, 2);
			TargetActor.RunSkill();
		}
		
		TargetActor.SelfTransform.position =
			Vector3.Lerp(SelfPos, TargetPos, CurTime / 1.84f);
	}

    //public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    //{
    //    if (TargetActor.AI.IS_SKILL)
    //    {
    //        if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
    //            TargetActor.AI.IS_SKILL = false;
    //    }
    //}


}
