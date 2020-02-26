using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ObjectManager : MonoSingleton<ObjectManager>
{
	Dictionary<string, ObjectTemplateData> DicTemplateData
		= new Dictionary<string, ObjectTemplateData>();

	private void Awake()
	{
		TextAsset objectText =
			Resources.Load(ConstValue.ObjectTemplatePath)
			as TextAsset;

		if (objectText != null)
		{
			JSONObject rootNodeText =
				JSON.Parse(objectText.text) as JSONObject;

			if (rootNodeText != null)
			{
				JSONObject objectTemplateNode =
					rootNodeText[ConstValue.ObjectTemplateKey]
					as JSONObject;

				foreach (KeyValuePair<string, JSONNode> templateNode
					in objectTemplateNode)
				{
					DicTemplateData.Add(templateNode.Key,
						new ObjectTemplateData(
							templateNode.Key,
							templateNode.Value));
				}
			}
		}
	}

	public ObjectTemplateData GetTemplate(string strTemplateKey)
	{
		ObjectTemplateData templateData = null;
		DicTemplateData.TryGetValue(strTemplateKey, out templateData);
		if (templateData == null)
		{
			Debug.LogError(
				"Key : " + strTemplateKey
				+ " 해당 데이터 미등록!");
			return null;
		}

		return templateData;
	}

	public GameObjects AddObject(string strTemplateKey)
	{
		ObjectTemplateData templateData =
			GetTemplate(strTemplateKey);

		if (templateData == null)
			return null;

		GameObjects gameObjects = new GameObjects();
		gameObjects.SetTemplate(templateData);
		return gameObjects;
	}


}