using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class BuffManager : MonoSingleton<BuffManager>
{
	Dictionary<string, BuffTemplateData> DicBuff =
	new Dictionary<string, BuffTemplateData>();

	private void Awake()
	{
		TextAsset BuffText =
			Resources.Load(ConstValue.BuffTemplatePath) as TextAsset;

		if (BuffText != null)
		{
			JSONObject rootNodeData =
				JSON.Parse(BuffText.text) as JSONObject;

			if (rootNodeData != null)
			{
				JSONObject BuffNodes =
					rootNodeData[ConstValue.BuffTemplateKey]
					as JSONObject;

				foreach (KeyValuePair<string, JSONNode> node
					in BuffNodes)
				{
					DicBuff.Add(node.Key,
						new BuffTemplateData(node.Key, node.Value));
				}
			}
		}
		else
		{
			Debug.Log("Path : " + ConstValue.BuffTemplatePath);
		}
	}

	public BuffTemplateData Get(string _key)
	{
		BuffTemplateData tempData = null;
		DicBuff.TryGetValue(_key, out tempData);
		if (tempData == null)
		{
			Debug.Log(_key + " is not containsed key");
		}

		return tempData;
	}

	public string CreateBuff(eBuffType typeOfBuff, int level)
	{
		
		string typeOfBuffPlusLv = typeOfBuff.ToString() + level;

		return typeOfBuffPlusLv;
	}

}
