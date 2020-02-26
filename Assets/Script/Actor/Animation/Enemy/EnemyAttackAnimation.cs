using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAnimation : StateMachineBehaviour
{
	//Player TargetPlayer = null;
	NonPlayer TargetActor = null;
	bool bIsAttack = false;
	bool bFinalAttack = false;

    float RunSkillTiming = 0f;
    bool bLookAt = false;

	bool bGiantEnemy = false;
    bool bBossEnemy = false;


	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{

		//TargetPlayer = animator.GetComponentInParent<Player>();

		//if (TargetPlayer.CURRENT_STATE == ePlayerStateType.STATE_ATTACK)
		//{
		//	TargetPlayer.IS_ATTACK = true;
		//	bIsAttack = false;
		//}

		TargetActor = animator.GetComponentInParent<NonPlayer>();
		if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_ATTACK)
		{
			TargetActor.AI.IS_ATTACK = true;
			bIsAttack = false;
			bFinalAttack = false;
		}

        switch (TargetActor.TEMPLATE_KEY)
        {
            case "ENEMY_1":
                {
                    RunSkillTiming = 0.3f;
                }
                break;
            case "ENEMY_2":
                {
                    RunSkillTiming = 0.6f;
                    bLookAt = true;
                }
                break;
            case "ENEMY_3":
                {
					RunSkillTiming = 0.234f;
					bGiantEnemy = true;
                }
                break;
            case "ENEMY_BOSS":
                {
                    RunSkillTiming = 0.5f;
                    bBossEnemy = true;
                }
                break;
        }
    }

	// 첫 번째와 마지막 프레임을 제외하고 각 업데이트 프레임에서 호출됩니다.
	public override void OnStateUpdate(
		Animator animator, AnimatorStateInfo animatorStateInfo,int layerIndex)
	{
		if (animatorStateInfo.normalizedTime >= 1.0f && TargetActor.AI.IS_ATTACK)
		{
			//BaseObject bo =
			//	animator.GetComponentInParent<BaseObject>();

			//bo.ThrowEvent("AttackEnd",eStateType.STATE_IDLE);
			if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_ATTACK)
				TargetActor.AI.IS_ATTACK = false;
		}

        if (bLookAt)
        {
            Vector3 Dir = ((TargetActor.GetData(ConstValue.ActorData_GetTarget) as BaseObject).SelfTransform.position
                - TargetActor.SelfTransform.position).normalized;
            TargetActor.SelfTransform.forward = Dir;
        }

		if (bIsAttack == false
			&& animatorStateInfo.normalizedTime >= RunSkillTiming)
		{
            bLookAt = false;
			bIsAttack = true;
			TargetActor.RunSkill();
		}

		// GiantEnemy 일 때 3타공격 발동. (하드코딩임.. 구조상 이게 제일 편함)
		if (bGiantEnemy && bFinalAttack == false
			&& animatorStateInfo.normalizedTime >= RunSkillTiming + 0.29f)
		{
			bFinalAttack = true;
			TargetActor.ThrowEvent(ConstValue.EventKey_SelectSkill, 1);
			TargetActor.RunSkill();
		}


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
		//	&& animatorStateInfo.normalizedTime >= 0.5f)
		//{
		//	bIsAttack = true;
		//	TargetActor.RunSkill();
		//}
	}
}
