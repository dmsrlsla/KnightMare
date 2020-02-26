using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EquipEvent();

public class ItemManager : MonoSingleton<ItemManager>
{
	bool IsInit = false;

	public EquipEvent EquipE;

	// 소지 아이템
	List<ItemInstance> listItem = new List<ItemInstance>();
	// 착용 아이템
	Dictionary<eSlotType, ItemInstance> DicEquipItem =
		new Dictionary<eSlotType, ItemInstance>();

	public List<ItemInstance> LIST_ITEM
	{
		get { return listItem; }
	}

	public Dictionary<eSlotType, ItemInstance> DIC_EQUIP
	{
		get { return DicEquipItem; }
	}

	Dictionary<int, ItemInfo> DicItemInfo = new Dictionary<int, ItemInfo>();
	public Dictionary<int,ItemInfo> DIC_ITEMINFO
	{
		get { return DicItemInfo; }
	}

	public void ItemInit()
	{
		if (IsInit == true)
			return;

		TextAsset itemInfo = Resources.Load<TextAsset>("JSON/ITEM_INFO");
		JSONNode rootNode = JSON.Parse(itemInfo.text);

		foreach (KeyValuePair<string, JSONNode> pair
			in rootNode["ITEM_INFO"]as JSONObject)
		{
			ItemInfo info = new ItemInfo(pair.Key, pair.Value);
			DicItemInfo.Add(int.Parse(info.KEY), info);
		}
		GetLocalData();
		IsInit = true;
	}

	public void GetLocalData()
	{
		// ITEM_ID _ SlotType _ ITEM_NO | ITEM_ID _ SlotType _ ITEM_NO |
		string instanceStr = PlayerPrefs.GetString(
			ConstValue.LocalSave_ItemInstance, string.Empty);

		Debug.Log(instanceStr);
		

		string[] array = instanceStr.Split('|');

		for (int i = 0; i< array.Length; i++)
		{
			// ITEM_ID _ SlotType _ ITEM_NO
			if (array[i].Length <= 0)
				continue;

			string[] detail = array[i].Split('_');

			if (detail.Length < 2)
				continue;

			int itemId = int.Parse(detail[0]);
			Debug.Log(itemId);

			eSlotType slotType = (eSlotType)int.Parse(detail[1]);	//1번 슬롯인가, 2번 슬롯인가

			ItemInfo info = null;
			DicItemInfo.TryGetValue(int.Parse(detail[2]), out info);
			if(info == null)
			{
				Debug.Log("ID : " + itemId + " ItemNo : "
					+ detail[1] + " is not Valid");
				continue;
			}

			// ItemInstance
			ItemInstance instance = new ItemInstance(itemId, slotType, info);
			
			//listItem.Add(instance);

			// Equip
			if (slotType == eSlotType.SLOT_1)
			{
				//DicEquipItem.Add(eSlotType.SLOT_1, instance);
				EquipItem(instance, eSlotType.SLOT_1, true);

			}
			else if(slotType == eSlotType.SLOT_2)
			{
				//EquipItem(instance, eSlotType.SLOT_1, false);
				//DicEquipItem.Add(eSlotType.SLOT_2, instance);
				EquipItem(instance, eSlotType.SLOT_2, true);
			}


		}

	}

	public void EquipItem(ItemInstance instance, eSlotType slotNum, bool isSave = true)
	{
		ItemInfo info = instance.ITEM_INFO;

		if(DicEquipItem.ContainsKey(slotNum))
		{
			// 현재 장착중인 슬롯
			// 착용 해제
			//DicEquipItem[slotNum].SLOT_TYPE = eSlotType.SLOT_NONE;
			DicEquipItem.Remove(slotNum);

			// 새롭게 착용
			instance.SLOT_TYPE = slotNum;
			DicEquipItem[slotNum] = instance;
		}
		else
		{
			instance.SLOT_TYPE = slotNum;
			DicEquipItem.Add(slotNum, instance);
		}

		if (EquipE != null)
			EquipE();

		if (isSave)
			SetLocalData();

		GameManager.Instance.PlayerActor.equipItemStatus();

	}

	public void SetLocalData()//아이템의 데이터를 받는 부분은 아이템을 얻을때와 게임 시작 시 받아올 때 2번 진행(IsSave).
	{
		// ITEM_ID _ SlotType _ ITEM_NO | ITEM_ID _ SlotType _ ITEM_NO |
		// "1_-1_3 | 2_1_5 | 3_1_5"

		string resultStr = string.Empty;

		if(DicEquipItem.ContainsKey(eSlotType.SLOT_1))
		{
			ItemInstance temp;
			string itemStr = string.Empty;

			DicEquipItem.TryGetValue(eSlotType.SLOT_1, out temp);
			
			itemStr += (temp.ITEM_ID) + "_";
			itemStr += (int)eSlotType.SLOT_1 + "_";    //슬롯 세팅
			itemStr += temp.ITEM_NO;

			
				

			resultStr += itemStr;
		}

		if (DicEquipItem.ContainsKey(eSlotType.SLOT_2))
		{
			ItemInstance temp;
			string itemStr = string.Empty;

			DicEquipItem.TryGetValue(eSlotType.SLOT_2, out temp);

			itemStr += "|";
			itemStr += (temp.ITEM_ID) + "_";
			itemStr += (int)eSlotType.SLOT_2 + "_";    //슬롯 세팅
			itemStr += temp.ITEM_NO;


			resultStr += itemStr;
		}

		//for (int i = 0; i< listItem.Count; i++)
		//{
		//	string itemStr = string.Empty;
		//	itemStr += (i + 1) + "_";
		//	itemStr += (int)listItem[i].SLOT_TYPE + "_";	//슬롯 세팅
		//	itemStr += listItem[i].ITEM_NO;

		//	if (i != listItem.Count - 1)
		//		itemStr += "|";

		//	resultStr += itemStr;
		//}

		PlayerPrefs.SetString(ConstValue.LocalSave_ItemInstance, resultStr);
		Debug.Log(resultStr);
	}

	public void GetItem()
	{
		int no = Random.Range(1, DicItemInfo.Count + 1);
		ItemInfo info = null;

		DicItemInfo.TryGetValue(no, out info);
		if(info == null)
		{
			Debug.LogError(no + " is not valid key");
			return;
		}

		ItemInstance instance = new ItemInstance(
			listItem.Count + 1, eSlotType.SLOT_NONE, info);
		listItem.Add(instance);	//리스트 추가는 얻을때와 게임 시작 시 받아올 때 2번 진행.
		//SetLocalData();

		//아이템이 자동 장착
		//ItemManager.Instance.EquipItem(instance, eSlotType.SLOT_1);


		//// 가챠  UI
		//GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_GACHA);
		//UI_Gacha popup = go.GetComponent<UI_Gacha>();
		//popup.Init(instance);
	}

}
