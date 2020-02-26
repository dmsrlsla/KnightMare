using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;

public class BasicStatusTemplateData  {

	string StrKey = string.Empty;

	StatusData Status = new StatusData();

	public string KEY { get { return StrKey; } }
	public StatusData STATUS { get { return Status; } }

	

	public BasicStatusTemplateData(string _strKey, JSONNode nodeData, eStatusData statusType)
	{
		StrKey = _strKey;

		
			double valueData =
				nodeData[statusType.ToString("F")].AsDouble;
			//기존에 쓰던 방식과 다르게 저장할 것이다.
			Status.IncreaseData(statusType, valueData);
		

		
	}
}
