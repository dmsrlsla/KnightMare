using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StageManager : MonoSingleton<StageManager> {

    Dictionary<int, StageInfo> DicStageInfo = new Dictionary<int, StageInfo>();
    public Dictionary<int, StageInfo> DIC_STAGEINFO
    {
        get { return DicStageInfo; }
    }

    public void StageInit()
    {
        TextAsset stageInfo = Resources.Load<TextAsset>("JSON/STAGE_INFO");
        JSONNode rootNode = JSON.Parse(stageInfo.text);

        foreach(KeyValuePair<string, JSONNode> pair 
            in rootNode["STAGE_INFO"] as JSONObject)
        {
            StageInfo info = new StageInfo(pair.Key, pair.Value);
            DicStageInfo.Add(int.Parse(info.KEY), info);
        }
    }
}
