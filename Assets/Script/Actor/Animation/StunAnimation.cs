using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAnimation : StateMachineBehaviour
{
	Player TargetPlayer = null;
	NonPlayer TargetActor = null;
	bool bIsStun = false;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{

		TargetPlayer = animator.GetComponentInParent<Player>();

		if (TargetPlayer.CURRENT_STATE == ePlayerStateType.STATE_STUN)
		{
			TargetPlayer.IS_STUN = true;
			TargetPlayer.IS_ATTACK = false;
			//bIsStun = false;
		}

		//TargetActor = animator.GetComponentInParent<NonPlayer>();
		//if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_ATTACK)
		//{
		//	TargetActor.AI.IS_ATTACK = true;
		//	bIsAttack = false;
		//}


	}

	// 첫 번째와 마지막 프레임을 제외하고 각 업데이트 프레임에서 호출됩니다.
	public override void OnStateUpdate(
		Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		//애니메이션 speed와 관련이 있다.(normalized)
		if (animatorStateInfo.normalizedTime >= 1.0f && TargetPlayer.IS_STUN)
		{
			if (TargetPlayer.CURRENT_STATE == ePlayerStateType.STATE_STUN)
				TargetPlayer.IS_STUN = false;
		}


		//if (bIsStun == false && animatorStateInfo.normalizedTime >= 0.01f)
		//{
		//	bIsStun = true;
		//	TargetPlayer.IS_STUN = true;
		//}
	}


}
