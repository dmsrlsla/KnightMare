using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
	Player TargetPlayer = null;
	NonPlayer TargetActor = null;
	bool bIsAttack = false;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{

		TargetPlayer = animator.GetComponentInParent<Player>();

		if (TargetPlayer.CURRENT_STATE == ePlayerStateType.STATE_ATTACK)
		{
			TargetPlayer.IS_ATTACK = true;
			bIsAttack = false;
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
		Animator animator, AnimatorStateInfo animatorStateInfo,int layerIndex)
	{
		if (animatorStateInfo.normalizedTime >= 1.0f && TargetPlayer.IS_ATTACK)
		{
			//BaseObject bo =
			//	animator.GetComponentInParent<BaseObject>();

			//bo.ThrowEvent("AttackEnd",eStateType.STATE_IDLE);
			if (TargetPlayer.CURRENT_STATE == ePlayerStateType.STATE_ATTACK)
				TargetPlayer.IS_ATTACK = false;
		}

        //if (bIsAttack == false
        //	&& animatorStateInfo.normalizedTime >= 0.5f)
        //{
        //	bIsAttack = true;
        //	TargetActor.RunSkill();
        //}


        //if (animatorStateInfo.normalizedTime >= 1.0f
        //	&& TargetActor.AI.IS_ATTACK)
        //{
        //	//BaseObject bo =
        //	//	animator.GetComponentInParent<BaseObject>();

        //	//bo.ThrowEvent("AttackEnd",eStateType.STATE_IDLE);
        //	if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_ATTACK)
        //		TargetActor.AI.IS_ATTACK = false;
        //}

        //if (bIsAttack == false
        //    && animatorStateInfo.normalizedTime >= 0.1f)
        //{
        //    bIsAttack = true;
        //    TargetActor.RunSkill();
        //}

        if (bIsAttack == false
          && animatorStateInfo.normalizedTime >= 0.01f)
        {
			ParticleManager.Instance.CreateSpeedLineParticle(TargetPlayer.gameObject.transform.localPosition, TargetPlayer.gameObject.transform.localRotation);
			bIsAttack = true;
            TargetPlayer.RunSkill();
        }
    }


}
