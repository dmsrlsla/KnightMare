using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ObjectTemplateData {

	string StrKey = string.Empty;

	StatusData Status = new StatusData();
	
	public string KEY { get { return StrKey; } }
	public StatusData STATUS { get { return Status; } }

	
	public ObjectTemplateData(string _strKey, JSONNode nodeData)
	{
		StrKey = _strKey;

		for (int i = 0; i < (int)eStatusData.MAX; i++)
		{
			eStatusData statusData = (eStatusData)i;
			double valueData =
				nodeData[statusData.ToString("F")].AsDouble;
			Status.IncreaseData(statusData, valueData);
		}

		
	}
}
