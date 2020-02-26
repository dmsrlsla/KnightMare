using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : BaseObject
{
	bool equipedSlot1 = false;
	bool IsInit = false;
	ItemInstance equipItemInstance;
	List<ItemInstance> itemList;
	Dictionary<eSlotType, ItemInstance> equipDic;

	//public ItemInstance ITEM_INSTANCE
	//{
	//	get { return itemInstance; }
	//	set { itemInstance = value; }
	//}

	eSelectItem selItem = eSelectItem.NONE;

	UITexture rewardItemTex = null;
	UIButton ExchangeBtn = null;
	UIButton PassBtn = null;
	UILabel curItem1Label = null;
	UILabel curItem2Label = null;

	UITexture curItem1Tex = null;
	UITexture curItem2Tex = null;

	UIToggle slot1 = null;
	UIToggle slot2 = null;

	UILabel rewardItemLabel = null;
	UILabel slot1Label = null;
	UILabel slot2Label = null;

	UISprite borderOfCurItem1Tex = null;
	UISprite borderOfCurItem2Tex = null;

	//bool ItemSwitch = true;

	private void Awake()
	{

		
		ExchangeBtn = GameObject.Find("ExchangeButton").GetComponent<UIButton>();
		PassBtn = GameObject.Find("PassButton").GetComponent<UIButton>();

		rewardItemTex = FindInChild("RewardItem").Find("Texture").GetComponent<UITexture>();
		curItem1Label = FindInChild("ItemDescription1").GetComponent<UILabel>();
		curItem2Label = FindInChild("ItemDescription2").GetComponent<UILabel>();

		curItem1Tex = FindInChild("CurrentItem1").GetComponentInChildren<UITexture>();
		curItem2Tex = FindInChild("CurrentItem2").GetComponentInChildren<UITexture>();
		
		rewardItemLabel = FindInChild("Reward Item Description").GetComponentInChildren<UILabel>();

		borderOfCurItem1Tex = FindInChild("CurrentItem1").GetComponentInChildren<UISprite>();
		borderOfCurItem2Tex = FindInChild("CurrentItem2").GetComponentInChildren<UISprite>();

		//itemdescription을 건드려서 켜있는지 꺼있는지 확인할 것  -> 이걸로 아이템을 어디에 장착할 것인지 확인.

		slot1 = this.transform.Find("Panel").Find("CurrentItem1").GetComponent<UIToggle>();
		slot2 = this.transform.Find("Panel").Find("CurrentItem2").GetComponent<UIToggle>();

		slot1Label = FindInChild("CurrentItem1").GetComponentInChildren<UILabel>();
		slot2Label = FindInChild("CurrentItem2").GetComponentInChildren<UILabel>();
	}



	public void Init()
	{
		//if (IsInit == true)
		//	return;

		itemList = ItemManager.Instance.LIST_ITEM;
		equipDic = ItemManager.Instance.DIC_EQUIP;

		ShowRewardItem();
		ShowEquipItem();

	}

	public void ShowRewardItem()
	{

		//아이템들은 쌓이지 않는다.
		if (itemList.Count - 1 < 0)
		{
			rewardItemTex.mainTexture = Resources.Load<Texture>("Textures/" + "f");


		
			rewardItemLabel.text = "현재 가진 아이템이 아무것도 없습니다!";
			
		}

		else
		{
			rewardItemTex.mainTexture = Resources.Load<Texture>("Textures/" + itemList[itemList.Count - 1].ITEM_INFO.ITEM_IMAGE);


			string desc = itemList[itemList.Count - 1].ITEM_INFO.NAME + "\n\n"
				+ "HP = +" + itemList[itemList.Count - 1].ITEM_INFO.STATUS.GetStatusData(eStatusData.MAX_HP) + "\n"
				+ "Attack = +" + itemList[itemList.Count - 1].ITEM_INFO.STATUS.GetStatusData(eStatusData.ATTACK) + "\n"
				+ "Defence = +" + itemList[itemList.Count - 1].ITEM_INFO.STATUS.GetStatusData(eStatusData.DEFENCE);


			rewardItemLabel.text = desc;
		}
	}

	public void ShowEquipItem()
	{
		
	
		string desc;

		ItemInstance tempItemIns = null;

	
		if(equipDic.Count >= 1)
		{
			//equipDic에서 아이템들을 받아옴
			equipDic.TryGetValue(eSlotType.SLOT_1, out tempItemIns);

			curItem1Tex.mainTexture = Resources.Load<Texture>("Textures/" + tempItemIns.ITEM_INFO.ITEM_IMAGE);

			

			desc = tempItemIns.ITEM_INFO.NAME + "\n\n"
				+ "HP = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.MAX_HP) + "\n"
				+ "Attack = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.ATTACK) + "\n"
				+ "Defence = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.DEFENCE);


			curItem1Label.text = desc;


			
			slot1Label.alpha = 0f;

			//selItem = eSelectItem.SELECT1;
		}
		else
		{
			curItem1Label.text = "1번 슬롯의 장착 아이템이 존재하지 않습니다.";
		}

		if (equipDic.Count == 2)
		{
			equipDic.TryGetValue(eSlotType.SLOT_2, out tempItemIns);

			curItem2Tex.mainTexture = Resources.Load<Texture>("Textures/" + tempItemIns.ITEM_INFO.ITEM_IMAGE);

			

			desc = tempItemIns.ITEM_INFO.NAME + "\n\n"
				+ "HP = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.MAX_HP) + "\n"
				+ "Attack = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.ATTACK) + "\n"
				+ "Defence = +" + tempItemIns.ITEM_INFO.STATUS.GetStatusData(eStatusData.DEFENCE);


			curItem2Label.text = desc;

			slot2Label.alpha = 0f;

			//selItem = eSelectItem.SELECT2;
		}
		else
		{
			curItem2Label.text = "2번 슬롯의 장착 아이템이 존재하지 않습니다.";
		}


	}

	void Start ()
	{

		

		EventDelegate.Add(ExchangeBtn.onClick, () =>
		{
			//bool selCompleted = false;
			print("Exchange Button is OK.");
			//int listCount = itemList.Count; //이렇게 하지 않으면 리스트가 지워지면서 최대값도 줄어들기 때문!

			equipItemInstance = itemList[itemList.Count - 1];

			if(ItemManager.Instance.DIC_EQUIP.ContainsKey(eSlotType.SLOT_1))
				equipedSlot1 = true;


			switch (selItem)
			{
				
				case eSelectItem.SELECT1:
						
						ItemManager.Instance.EquipItem(equipItemInstance, eSlotType.SLOT_1);
						equipedSlot1 = true;
						//selCompleted = true;


					break;
				case eSelectItem.SELECT2:

					if (equipedSlot1==true)
					{
						
						ItemManager.Instance.EquipItem(equipItemInstance, eSlotType.SLOT_2);
						//selCompleted = true;
					}
					else
					{
						//equipItemInstance.SLOT_TYPE = eSlotType.SLOT_1;	//1슬롯에 넣는다.
						ItemManager.Instance.EquipItem(equipItemInstance, eSlotType.SLOT_1);

						equipedSlot1 = true;
						//selCompleted = true;
					}
					break;
				default:
					break;
			}

			itemList.Clear();

			gameObject.SetActive(false);

			//if (selCompleted == true)
			//{
			//	itemList.Clear();

			//	gameObject.SetActive(false);
			//}

			Time.timeScale = 1.0f;

            GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_STAGESELECT);
            UI_StageSelect stageSelect = go.GetComponent<UI_StageSelect>();
            stageSelect.Init();
		}
		);

		EventDelegate.Add(PassBtn.onClick, () =>
		{
			print("Pass Button is OK.");

			itemList.Clear();
		
			gameObject.SetActive(false);


			Time.timeScale = 1.0f;

            GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_STAGESELECT);
            UI_StageSelect stageSelect = go.GetComponent<UI_StageSelect>();
            stageSelect.Init();
        }
		);

		EventDelegate.Add(slot1.onChange, () =>
		{
			if (slot1.value == true)
			{
				borderOfCurItem1Tex.alpha = 1f;
				borderOfCurItem2Tex.alpha = 0f;



				selItem = eSelectItem.SELECT1;

			}

		}
		);

		EventDelegate.Add(slot2.onChange, () =>
		{
			if (slot2.value == true)
			{
				borderOfCurItem2Tex.alpha = 1f;
				borderOfCurItem1Tex.alpha = 0f;

				
				selItem = eSelectItem.SELECT2;
			}
				
		}
		);


	}

	private void Update()
	{
		//if (curItem1Label.gameObject.activeInHierarchy)
		//	selItem = eSelectItem.SELECT1;

		//else if (curItem2Label.gameObject.activeInHierarchy)
		//	selItem = eSelectItem.SELECT2;
		//else
		//	selItem = eSelectItem.NONE;




	}
}
