//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSkill : BaseSkill
{
	GameObject ModelPrefab = null;
    Vector3 TargetPos = Vector3.zero;
    bool bFire = true;
    float ArrowSpeed = 6f;

	Vector3 TargetCorrection = Vector3.zero; // 화살 도착위치 상하좌우 약간씩 보정

	public override void InitSkill()
	{
		if (ModelPrefab == null)
			return;
		GameObject model = Instantiate(ModelPrefab, Vector3.zero, Quaternion.identity);
		model.transform.SetParent(this.transform, false);
	}

	public override void UpdateSkill()
	{
		if(TARGET == null)
		{
            End();
		}

        if (TargetPos == Vector3.zero)
            TargetPos = TARGET.SelfTransform.position;

		if (TargetCorrection == Vector3.zero)
		{
			int RandomNum = Random.Range(1, 5);

			switch (RandomNum)
			{
				case 1:
					{
						TargetCorrection = Vector3.forward * 0.5f;
					}
					break;
				case 2:
					{
						TargetCorrection = Vector3.back * 0.5f;
					}
					break;
				case 3:
					{
						TargetCorrection = Vector3.left * 0.5f;
					}
					break;
				case 4:
					{
						TargetCorrection = Vector3.right * 0.5f;
					}
					break;
			}
		}

        if (bFire)
        {
			Vector3 targetPosition = SelfTransform.position +
                (TargetPos + TargetCorrection - SelfTransform.position).normalized
                * ArrowSpeed * Time.deltaTime;
            SelfTransform.position = targetPosition;
        }

        float Dis = Vector3.Distance(SelfTransform.position, TargetPos);

        if (Dis <= 0.6f)
        {
            bFire = false;
            Invoke("End", 3f);
        }
	}

	public override void ThrowEvent(string keyData, params object[] datas)
	{
		if(keyData == ConstValue.EventKey_SelectModel)
		{
			ModelPrefab = datas[0] as GameObject;
		}
		else
			base.ThrowEvent(keyData, datas);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (END == true)
			return;
        if (bFire == false)
            return;

		Actor casterActor = OWNER.GetData(ConstValue.ActorData_GetThisActor) as Actor;

		GameObject colObject = other.gameObject;
		BaseObject actorObject = colObject.GetComponent<BaseObject>();

		if (actorObject != TARGET)
			return;

		TARGET.ThrowEvent(ConstValue.EventKey_Hit,
			OWNER.GetData(ConstValue.ActorData_Character),
			SKILL_TEMPLATE, casterActor.CUR_BUFF, casterActor.transform.rotation);
		END = true;
	}

    void End()
    {
        END = true;
        return;
    }
}
