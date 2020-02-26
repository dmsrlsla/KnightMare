using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAnmation : StateMachineBehaviour {

	NonPlayer TargetActor = null;
	RangeEnemy RangeEnemyScript = null;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		TargetActor = animator.GetComponentInParent<NonPlayer>();

		if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
			TargetActor.AI.IS_SKILL = true;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		//if (animatorStateInfo.normalizedTime >= 0.95f)
		//{
		//	if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
		//		RangeEnemyScript.IS_ROLLING = false;
		//}

		if (animatorStateInfo.normalizedTime >= 1.0f && TargetActor.AI.IS_SKILL)
		{
			if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
				TargetActor.AI.IS_SKILL = false;
		}
	}
}
