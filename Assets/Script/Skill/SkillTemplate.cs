using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTemplate
{
	string StrKey = string.Empty;
	eSkillTemplateType SKillType = eSkillTemplateType.TARGET_ATTACK;
	eSkillAttackRangeType RangeType = eSkillAttackRangeType.RANGE_BOX;

	// RangeType Box     x : RangeData_1 y : RangeData_2
	// RangeType Sphere  radius : RangeData_1
	float RangeData_1 = 0;
	float RangeData_2 = 0;
	float RangeData_3 = 0;
	float RangeCenter_1 = 0;// 건희 06/27
	float RangeCenter_2 = 0;
	float RangeCenter_3 = 0;

	StatusData SkillStatus = new StatusData();

	public eSkillTemplateType SKILL_TYPE { get { return SKillType; } }
	public eSkillAttackRangeType RANGE_TYPE {  get { return RangeType; } }

	public float RANGE_DATA_1 { get { return RangeData_1; } }
	public float RANGE_DATA_2 { get { return RangeData_2; } }
	public float RANGE_DATA_3 { get { return RangeData_3; } }
	public float RANGE_CENTER_1 { get { return RangeCenter_1; } }// 건희 06/27
	public float RANGE_CENTER_2 { get { return RangeCenter_2; } }
	public float RANGE_CENTER_3 { get { return RangeCenter_3; } }
	public StatusData STATUS_DATA { get { return SkillStatus; } }

	public SkillTemplate(string _strKey, JSONNode nodeData)
	{
		StrKey = _strKey;

		SKillType = (eSkillTemplateType)nodeData["SKILL_TYPE"].AsInt;
		RangeType = (eSkillAttackRangeType)nodeData["RANGE_TYPE"].AsInt;

		RangeData_1 = nodeData["RANGE_DATA_1"].AsFloat;
		RangeData_2 = nodeData["RANGE_DATA_2"].AsFloat;
		RangeData_3 = nodeData["RANGE_DATA_3"].AsFloat;
		RangeCenter_1 = nodeData["RANGE_CENTER_1"].AsFloat;// 건희 06/27
		RangeCenter_2 = nodeData["RANGE_CENTER_2"].AsFloat;
		RangeCenter_3 = nodeData["RANGE_CENTER_3"].AsFloat;

		for (int i = 0; i < (int)eStatusData.MAX; i++)
		{
			eStatusData statusData = (eStatusData)i;
			double valueData = nodeData[statusData.ToString()].AsDouble;
			if (valueData > 0)
				SkillStatus.IncreaseData(statusData, valueData);	
		}


	}


}
