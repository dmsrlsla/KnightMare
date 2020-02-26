using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class BasicStatusManager : MonoSingleton<BasicStatusManager> {


	Dictionary<string, BasicStatusTemplateData> DicBasicStatus = null;

	List<Dictionary<string, BasicStatusTemplateData>> ListDicBasicStatus = new List<Dictionary<string, BasicStatusTemplateData>>();

	private void Awake()
	{
		TextAsset BStatusText =
			Resources.Load(ConstValue.BasicStatusTemplatePath) as TextAsset;

		if (BStatusText != null)
		{
			JSONObject rootNodeData =
				JSON.Parse(BStatusText.text) as JSONObject;

			if (rootNodeData != null)
			{
				JSONObject BStatusNodes =
					rootNodeData[ConstValue.BasicStatusTemplateKey]
					as JSONObject;

				foreach (KeyValuePair<string, JSONNode> node
					in BStatusNodes)
				{
					
					DicBasicStatus = new Dictionary<string, BasicStatusTemplateData>();
					for (int i = 0; i < (int)eStatusData.MAX; i++)
					{
						
						DicBasicStatus.Add(((eStatusData)i).ToString(), new BasicStatusTemplateData(node.Key, node.Value, (eStatusData)i));
						
						//ListDicBasicStatus.Add();

					}
					ListDicBasicStatus.Add(DicBasicStatus);
					
				}
			}
		}
		else
		{
			Debug.Log("Path : " + ConstValue.BuffTemplatePath);
		}
	}

	public BasicStatusTemplateData Get(string _key, int _index)
	{
		_index--; //json과 key번호를 맞추기 위함.
		BasicStatusTemplateData tempData = null;

		ListDicBasicStatus[_index].TryGetValue(_key, out tempData);
		//DicBasicStatus.TryGetValue(_key, out tempData);
		if (tempData == null)
		{
			Debug.Log(_key + " is not containsed key");
		}

		return tempData;
	}


}

