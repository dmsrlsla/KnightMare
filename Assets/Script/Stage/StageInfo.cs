using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StageInfo {

    string StrKey = string.Empty;
    string Name = string.Empty;
    double Attack = 0;
    double Armor = 0;
    double Hp = 0;
    double Status = 0;

    public string KEY { get { return StrKey; } }
    public string NAME { get { return Name; } }
    public double ATTACK_SCALE { get { return Attack; } }
    public double ARMOR_SCALE { get { return Armor; } }
    public double HP_SCALE { get { return Hp; } }
    public double STATUS_SCALE { get { return Status; } }

    public StageInfo(string _strKey, JSONNode nodeData)
    {
        StrKey = _strKey;
        Name = nodeData["NAME"];
        Attack = nodeData["ATTACK"].AsDouble;
        Armor = nodeData["ARMOR"].AsDouble;
        Hp = nodeData["HP"].AsDouble;
        Status = nodeData["STATUS"].AsDouble;
    }
}
