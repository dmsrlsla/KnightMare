using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class BuffTemplateData {

	string StrKey = string.Empty;
	StatusData StatusData = new StatusData();

	float Heal = 0;
	float Time = 0;

	//string Image	= string.Empty;
	//	string Category = string.Empty;

	public string KEY { get { return StrKey; } }
	public float HEAL { get { return Heal; } }
	public float TIME { get { return Time; } }

	//	public string IMAGE { get { return Image; } }
	//	public string CATEGORY { get { return Category; } }

	public StatusData STATUS_DATA { get { return StatusData; } }

	public BuffTemplateData(string strKey, JSONNode nodeData)
	{
		StrKey = strKey;
		Heal = nodeData["HEAL"].AsFloat;
		Time = nodeData["TIME"].AsFloat;

		//Image = nodeData["IMAGE"];
		//	Category = nodeData["CATEGORY"];

		for (int i = 0; i < (int)eStatusData.MAX; i++)
		{
			eStatusData eStatus = (eStatusData)i;
			double valueData =
				nodeData[eStatus.ToString()].AsDouble;
			StatusData.IncreaseData(eStatus, valueData);
		}
	}

}
