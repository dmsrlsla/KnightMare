using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StatusData
{
    //능력치를 저장할수 있는 애들을 만듬
	Dictionary<eStatusData, double> DicData =
		new Dictionary<eStatusData, double>();

	public void InitData()
	{
		DicData.Clear();
	}

	public void Copy(StatusData data)
	{
		foreach(KeyValuePair<eStatusData,double> pair
			in data.DicData)
		{
			IncreaseData(pair.Key, pair.Value);
		}
	}
    //키값이 존재한다면 preValue에 값넣음.
    public void IncreaseData(
		eStatusData statusData, double valueData)
	{
		double preValue = 0.0;//초기화 필수. 키값이 없으면 아무일도 하지 않을것이기 때문
		DicData.TryGetValue(statusData, out preValue);
		DicData[statusData] = preValue + valueData;
	}
    //리스트는 키가 없으면 error, 딕셔너리는 일단 세팅됨.
	public void DecreaseData(
		eStatusData statusData, double valueData)
	{
		double preValue = 0.0;
		DicData.TryGetValue(statusData, out preValue);
		DicData[statusData] = preValue - valueData;
	}
    //세팅
	public void SetData(
		eStatusData statusData, double valueData)
	{
		DicData[statusData] = valueData;
	}
    //삭제
	public void RemoveData(eStatusData statusData)
	{
		if(DicData.ContainsKey(statusData) == true)
			DicData.Remove(statusData);
	}
    //원하는 데이터가 있으면 가져오고 없으면 반환
	public double GetStatusData(eStatusData statusData)
	{
		double preValue = 0.0;
		DicData.TryGetValue(statusData, out preValue);
		return preValue;
	}
    // UI용
    public string StatusString()
    {
        string returnStr = string.Empty;

        foreach(var pair in DicData)
        {
            returnStr += pair.Key.ToString();
            returnStr += " "+pair.Value.ToString();
            returnStr += "\n";
        }

        return returnStr;
    }
}
