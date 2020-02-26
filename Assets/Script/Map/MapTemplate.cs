using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class MapTemplate
{
    string StrKey = string.Empty;
    //맵 타입을 받아옴(노멀)
    eMapTemplateType MapType = eMapTemplateType.D_TYPE;

    int StageLevel = 0;
    int BuffAreaCount = 0;
    int EnemyCount = 0;
    int TrapCount = 0;
    int Enemy_B_Count = 0;
    int Trap_Cutter_count = 0;

    int tempMap = 0;
    
    //추후 보스 및 맵타입 구현예정
    //int BossCount = 0;


    public string KEY { get { return StrKey; } }
    public int BUFFAREA_COUNT { get { return BuffAreaCount; } }
    public int ENEMY_COUNT { get { return EnemyCount; } }
    public int TRAP_COUNT { get { return TrapCount; } }
    public int ENEMY_B_COUNT { get { return Enemy_B_Count; } }
    public int TRAP_CUTTER_COUNT { get { return Trap_Cutter_count; } }
    public eMapTemplateType MAP_TYPE { get { return MapType; } }
    // 보스 맵 처리는 나중에 받기로 함.
    //public int BOSS_COUNT { get { return BossCount; } }
    //public eMapTemplateType MAP_TYPE { get { return MapType; } }

    public MapTemplate(string _strKey, JSONNode nodeData)
    {
        StrKey = _strKey;

        BuffAreaCount = nodeData["BUFF"].AsInt;
        EnemyCount = nodeData["ENEMY"].AsInt;
        TrapCount = nodeData["TRAP"].AsInt;
        Enemy_B_Count = nodeData["ENEMY_B"].AsInt;
        Trap_Cutter_count = nodeData["TRAP_CUTTER"].AsInt;
        tempMap = nodeData["TYPE"].AsInt;
        MapType = (eMapTemplateType)tempMap;
        //보스,맵 타입(추후 구현예정)
        //BossCount = nodeData["BOSS"].AsInt;
        //MapType = nodeData["TYPE"].AsInt;
    }
}
