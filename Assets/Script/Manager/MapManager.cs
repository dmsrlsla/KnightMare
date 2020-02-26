using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class MapManager : MonoSingleton<MapManager>
{
    Dictionary<string, MapTemplate> DicMap =
        new Dictionary<string, MapTemplate>();

    string stage = string.Empty;

    private void Awake()
    {
        TextAsset MapText =
            Resources.Load(ConstValue.MapTemplatePath) as TextAsset;
        if(MapText != null)
        {
            JSONObject rootNodeData =
                JSON.Parse(MapText.text) as JSONObject;
            if(rootNodeData != null)
            {
                JSONObject MapNodes =
                    rootNodeData[ConstValue.MapTemplateKey] as JSONObject;

                foreach(KeyValuePair<string,JSONNode> node
                    in MapNodes)
                {
                    DicMap.Add(node.Key,
                        new MapTemplate(node.Key, node.Value));
                }
            }
        }
        else
        {
            Debug.Log("Path :" + ConstValue.MapTemplatePath);
        }
    }

    public MapTemplate Get()
    {
        StageSelect();
        MapTemplate tempData = null;
        DicMap.TryGetValue(stage, out tempData);
        if(tempData == null)
        {
            Debug.Log(stage + " is not containsed key");
        }


        return tempData;
    }

    public void StageSelect()
    {
        stage = GameManager.Instance.SelectStage.ToString();
    }
}
