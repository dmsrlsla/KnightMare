using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(RandomMapCreater))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RandomMapCreater map = target as RandomMapCreater;

        //맵 자동로딩 끔(실행시 생성됨.)
        //map.CreateMap();
    }
}
