using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseAI
{
    BaseObject targetObject;
    //아가를 만들지 결정함.


    protected override IEnumerator Idle()
    {
        searchDis = 100f;
        // 근거리 적 탐색
        targetObject = ActorManager.Instance.GetSearchEnemy(Target, searchDis);

        if(targetObject != null)
        {
            SkillData sData =
                Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;


            float attackRange = 1f;

            if (sData != null)
                attackRange = sData.RANGE;


            float distance = Vector3.Distance(
                targetObject.SelfTransform.position,
                SelfTransform.position);

            if (IS_SKILL_COOLDOWN == false)
            {
                Stop();
                AddNextAI(eStateType.STATE_SPECIAL, targetObject);
            }
            else if (distance < attackRange
                && IS_ATTACK_COOLDOWN == false && IS_SKILL_COOLDOWN == true)
            {
                Stop();
                AddNextAI(eStateType.STATE_ATTACK, targetObject);
            }

            //else if (distance < attackRange- 2f &&
            //    (IS_ATTACK_COOLDOWN == true))
            //{
            //    Stop();
            //    AddNextAI(eStateType.STATE_IDLE);
            //}
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

        if(targetObject != null)
        {
            SkillData sData =
                Target.GetData(ConstValue.ActorData_SkillData, 0) as SkillData;
            SkillData sDataSpecial =
                Target.GetData(ConstValue.ActorData_SkillData, 1) as SkillData;
            float attackRange = 1f;
            float attackRangeSpecial = 1f;


            if (sData != null)
                attackRange = sData.RANGE;
            if (sDataSpecial != null)
                attackRangeSpecial = sDataSpecial.RANGE;

            float distance = Vector3.Distance(
                targetObject.SelfTransform.position,
                SelfTransform.position);


            if (IS_SKILL_COOLDOWN == false)
            {
                Stop();
                if (Random.Range(0, 2) == 1)
                    IS_SECOND_SKILL = true;
                AddNextAI(eStateType.STATE_SPECIAL, targetObject);
            }
            else if (distance < attackRange
                && IS_ATTACK_COOLDOWN == false && IS_SKILL_COOLDOWN == true)
            {
                Stop();
                AddNextAI(eStateType.STATE_ATTACK, targetObject);
            }

            else if(distance < attackRange - 1.2f && 
                (IS_ATTACK_COOLDOWN == true))
            {
                Stop();
                AddNextAI(eStateType.STATE_IDLE);
            }
            else
            {
                // 공격 대상까지 움직임.
                SetMove(targetObject.SelfTransform.position);
            }
        }
        // 탐색을 멈추지 않음.
        else
        {
            Stop();
            AddNextAI(eStateType.STATE_IDLE);
        }
        yield return StartCoroutine(base.Move());
    }

    protected override IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();
        while(IS_ATTACK)
        {
            if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
                break;
            yield return new WaitForEndOfFrame();
            // IDLE로 넘어가기 전에 이 애니메이션이 끝났는지 체크.
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
        //float DeadTime = 0.0f;
        //while (DeadTime < 3.0f)
        //{
        //    DeadTime += Time.deltaTime;

        //    // IDLE로 넘어가기 전에 이 애니메이션이 끝났는지 체크.
        //    // 코루틴은 따로돌기때문에 가능한 구성.
        //    transform.parent.position += Vector3.up * 4f;
        //    yield return new WaitForEndOfFrame();
        //}
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
        //Vector3 dis = (targetObject.transform.position
        //- transform.parent.position).normalized;
        IS_SKILL_COOLDOWN = true;
        Invoke("OffSkillCoolDown", 7f);

        yield return new WaitForEndOfFrame();
        while(IS_SKILL)
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
