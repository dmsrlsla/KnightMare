using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackAnimation : StateMachineBehaviour
{
    float CurTime = 0f;

    NonPlayer TargetActor = null;
    BaseObject TargetObject = null;
    GameObject FireBallLoad = null;
    GameObject FireBallCreate = null;
    GameObject EggBallLoad = null;
    GameObject EggBallCreate = null;
    bool bIsAttack = false;

    Vector3 SelfPos = Vector3.zero;
    Vector3 TargetPos = Vector3.zero;

    protected new void Awake()
    {
        FireBallLoad = Resources.Load("Prefabs/Actor/FireBall") as GameObject;
        FireBallLoad.SetActive(false);
        FireBallCreate = Instantiate(FireBallLoad);

        EggBallLoad = Resources.Load("Prefabs/Map/Egg") as GameObject;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TargetActor = animator.GetComponent<NonPlayer>();
        TargetObject = (BaseObject)(TargetActor.GetData(ConstValue.ActorData_GetTarget));
        if (TargetObject == null)
            Debug.LogError("타겟널이야 홍은기 븅시나");


        if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
        {
            TargetActor.AI.IS_SKILL = true;
            bIsAttack = false;
            CurTime = 0f;
        }
        //get_target을 보내 타겟 정보를 가져옴

        //타겟(플레이어)의 위치를 구함
        Vector3 targetPos = TargetObject.SelfTransform.position;

        //보스의 위치를 구함
        SelfPos = TargetActor.SelfTransform.position;

        //FireBallCreate.transform.position = SelfPos;
        //FireBallCreate.transform.localRotation = TargetActor.SelfTransform.rotation;
        //보스와 타겟의 방향을 구함.
        Vector3 norPos = (SelfPos - targetPos).normalized * 2f;

        TargetPos = targetPos + norPos;

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CurTime += Time.deltaTime;
        //Vector3 targetPos = TargetObject.SelfTransform.position;
        SelfPos = TargetActor.SelfTransform.position;
        //Vector3 norPos = (targetPos- SelfPos).normalized;

        if (stateInfo.normalizedTime>1.0f
            && TargetActor.AI.IS_SKILL)
        {
            //1초동안 보스가 스킬을 활성화 했다면 끔.
            if (TargetActor.AI.CURRENT_AI_STATE == eStateType.STATE_SPECIAL)
            {
                TargetActor.AI.IS_SKILL = false;

            }
            FireBallCreate.SetActive(false);
        }

        if(bIsAttack == false
            && stateInfo.normalizedTime >0.5f)
        {
            bIsAttack = true;
            //보스는 0이 일반공격,1이 스킬
            if(TargetActor.AI.IS_SECOND_SKILL)
            {
                EggBallCreate = Instantiate(EggBallLoad, SelfPos, Quaternion.identity);
                TargetActor.AI.IS_SECOND_SKILL = false;
            }
            else
            {
                FireBallCreate.transform.position = SelfPos;
                FireBallCreate.transform.localRotation = TargetActor.SelfTransform.rotation;
                TargetActor.ThrowEvent(ConstValue.EventKey_SelectSkill, 1);
                TargetActor.RunSkill();
                FireBallCreate.SetActive(true);
            }
        }
        //보스는 이동하지 않으므로 주석처리
        //TargetActor.SelfTransform.position =
        //    Vector3.Lerp(SelfPos, TargetPos, CurTime / 1.84f);

        //TargetActor.SelfTransform.localRotation = Quaternion.Lerp(TargetActor.SelfTransform.rotation, TargetObject.SelfTransform.rotation, CurTime / 1.84f);
    }
}
