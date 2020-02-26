using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUp : BaseObject
{
	Actor PlayerActor = null;

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

	eSelectStatus selStatus = eSelectStatus.NONE;

	//UITexture rewardItemTex = null;
	UIButton ExchangeBtn = null;
	UIButton PassBtn = null;
	UILabel selStatus1Label = null;
	UILabel selStatus2Label = null;
	UILabel selStatus3Label = null;

	UITexture curStatus1Tex = null;
	UITexture curStatus2Tex = null;
	UITexture curStatus3Tex = null;

	UIToggle slot1 = null;
	UIToggle slot2 = null;
	UIToggle slot3 = null;

	UILabel curStatusLabel = null;
	UILabel slot1Label = null;
	UILabel slot2Label = null;
	UILabel slot3Label = null;

	UISprite borderOfCurStatus1Tex = null;
	UISprite borderOfCurStatus2Tex = null;
	UISprite borderOfCurStatus3Tex = null;

	//bool ItemSwitch = true;

	private void Awake()
	{

		PlayerActor = GameManager.Instance.PLAYER_ACTOR;

		ExchangeBtn = GameObject.Find("ExchangeButton").GetComponent<UIButton>();
		PassBtn = GameObject.Find("PassButton").GetComponent<UIButton>();

	//	rewardItemTex = FindInChild("RewardItem").Find("Texture").GetComponent<UITexture>();
		selStatus1Label = FindInChild("StatusDescription1").GetComponent<UILabel>();
		selStatus2Label = FindInChild("StatusDescription2").GetComponent<UILabel>();
		selStatus3Label = FindInChild("StatusDescription3").GetComponent<UILabel>();

		curStatus1Tex = FindInChild("SelStatus1").GetComponentInChildren<UITexture>();
		curStatus2Tex = FindInChild("SelStatus2").GetComponentInChildren<UITexture>();
		curStatus2Tex = FindInChild("SelStatus3").GetComponentInChildren<UITexture>();

		curStatusLabel = FindInChild("Status").GetComponentInChildren<UILabel>();

		borderOfCurStatus1Tex = FindInChild("SelStatus1").GetComponentInChildren<UISprite>();
		borderOfCurStatus2Tex = FindInChild("SelStatus2").GetComponentInChildren<UISprite>();
		borderOfCurStatus3Tex = FindInChild("SelStatus3").GetComponentInChildren<UISprite>();

		//itemdescription을 건드려서 켜있는지 꺼있는지 확인할 것  -> 이걸로 아이템을 어디에 장착할 것인지 확인.

		slot1 = this.transform.Find("Panel").Find("SelStatus1").GetComponent<UIToggle>();
		slot2 = this.transform.Find("Panel").Find("SelStatus2").GetComponent<UIToggle>();
		slot3 = this.transform.Find("Panel").Find("SelStatus3").GetComponent<UIToggle>();

		slot1Label = FindInChild("SelStatus1").GetComponentInChildren<UILabel>();
		slot2Label = FindInChild("SelStatus2").GetComponentInChildren<UILabel>();
		slot3Label = FindInChild("SelStatus3").GetComponentInChildren<UILabel>();
	}



	public void Init()
	{
		//if (IsInit == true)
		//	return;

		itemList = ItemManager.Instance.LIST_ITEM;
		equipDic = ItemManager.Instance.DIC_EQUIP;

		ShowCurStatus();
		ShowSelStatus();

	}

	public void ShowCurStatus()
	{
		string desc = "플레이어의 현재 스테이터스" + "\n\n"
				+ "HP = +" + PlayerActor.SELF_CHARACTER.CHARACTER_STATUS.GetStatusData(eStatusData.MAX_HP) + "\n"
				+ "Attack = +" + PlayerActor.SELF_CHARACTER.CHARACTER_STATUS.GetStatusData(eStatusData.ATTACK) + "\n"
				+ "Defence = +" + PlayerActor.SELF_CHARACTER.CHARACTER_STATUS.GetStatusData(eStatusData.DEFENCE);


		curStatusLabel.text = desc;

		////아이템들은 쌓이지 않는다.
		//if (itemList.Count - 1 < 0)
		//{
		//	rewardItemTex.mainTexture = Resources.Load<Texture>("Textures/" + "f");



		//	curStatusLabel.text = "현재 가진 아이템이 아무것도 없습니다!";

		//}

		//else
		//{
		//	rewardItemTex.mainTexture = Resources.Load<Texture>("Textures/" + itemList[itemList.Count - 1].ITEM_INFO.ITEM_IMAGE);


		//	string desc = itemList[itemList.Count - 1].ITEM_INFO.NAME + "\n\n"
		//		+ "HP = +" + itemList[itemList.Count - 1].ITEM_INFO.STATUS.GetStatusData(eStatusData.MAX_HP) + "\n"
		//		+ "Attack = +" + itemList[itemList.Count - 1].ITEM_INFO.STATUS.GetStatusData(eStatusData.ATTACK) + "\n"
		//		+ "Defence = +" + itemList[itemList.Count - 1].ITEM_INFO.STATUS.GetStatusData(eStatusData.DEFFENCE);


		//	curStatusLabel.text = desc;
		//}
	}

	public void ShowSelStatus()
	{


		string desc = string.Empty ;
		string desc1 = string.Empty;
		string desc2 = string.Empty;

		//	ItemInstance tempItemIns = null;


		//if (equipDic.Count >= 1)
		//{
		//equipDic에서 아이템들을 받아옴
		//equipDic.TryGetValue(eSlotType.SLOT_1, out tempItemIns);

		//curStatus1Tex.mainTexture = Resources.Load<Texture>("Textures/" + tempItemIns.ITEM_INFO.ITEM_IMAGE);

		desc += "스테이터스 변동";
			//
			desc += "MAX HP: " + PlayerActor.HPStatus*10 + "->"+"MAX HP: " + (PlayerActor.HPStatus+1) * 10;

			selStatus1Label.text = desc;



		//slot1Label.alpha = 0f;

		//selItem = eSelectItem.SELECT1;
		//}
		//else
		//{
		//	selStatus1Label.text = "1번 슬롯의 장착 아이템이 존재하지 않습니다.";
		//}

		//if (equipDic.Count == 2)
		//{
		//	equipDic.TryGetValue(eSlotType.SLOT_2, out tempItemIns);

		//	curStatus2Tex.mainTexture = Resources.Load<Texture>("Textures/" + tempItemIns.ITEM_INFO.ITEM_IMAGE);

		desc = string.Empty;
		desc += "스테이터스 변동";
		desc += "AP: " + PlayerActor.APStatus * 3 + "->" + "AP: " + (PlayerActor.APStatus + 1) * 3;

		selStatus2Label.text = desc;

		//	slot2Label.alpha = 0f;

		//selItem = eSelectItem.SELECT2;
		//}
		//else
		//{
		//	selStatus3Label.text = "2번 슬롯의 장착 아이템이 존재하지 않습니다.";
		//}

		desc = string.Empty;
		desc += "스테이터스 변동";
		desc += "DP: " + PlayerActor.DPStatus * 2 + "->" + "DP: " + (PlayerActor.DPStatus + 1) * 2;

		selStatus3Label.text = desc;

		//slot3Label.alpha = 0f;

	}



	void Start()
	{

	

		EventDelegate.Add(ExchangeBtn.onClick, () =>
		{
			//bool selCompleted = false;
			print("Exchange Button is OK.");
			//int listCount = itemList.Count; //이렇게 하지 않으면 리스트가 지워지면서 최대값도 줄어들기 때문!

		


			switch (selStatus)
			{

				case eSelectStatus.HP:

					PlayerActor.basicStatusUp(eStatusData.MAX_HP);

					break;
				case eSelectStatus.AP:
					PlayerActor.basicStatusUp(eStatusData.ATTACK);

					break;

				case eSelectStatus.DP:
					PlayerActor.basicStatusUp(eStatusData.DEFENCE);

					break;
				default:
					break;
			}

			

			gameObject.SetActive(false);

			

			Time.timeScale = 1.0f;
		}
		);

		EventDelegate.Add(PassBtn.onClick, () =>
		{
			print("Pass Button is OK.");

			itemList.Clear();

			gameObject.SetActive(false);


			Time.timeScale = 1.0f;
		}
		);

		EventDelegate.Add(slot1.onChange, () =>
		{
			if (slot1.value == true)
			{
				borderOfCurStatus1Tex.alpha = 1f;
				borderOfCurStatus2Tex.alpha = 0f;
				borderOfCurStatus3Tex.alpha = 0f;



				selStatus = eSelectStatus.HP;

			}
			

		}
		);

		EventDelegate.Add(slot2.onChange, () =>
		{
			if (slot2.value == true)
			{
				borderOfCurStatus2Tex.alpha = 1f;
				borderOfCurStatus1Tex.alpha = 0f;
				borderOfCurStatus3Tex.alpha = 0f;


				selStatus = eSelectStatus.AP;
			}
		

		}
		);

		EventDelegate.Add(slot3.onChange, () =>
		{
			if (slot3.value == true)
			{
				borderOfCurStatus3Tex.alpha = 1f;
				borderOfCurStatus1Tex.alpha = 0f;
				borderOfCurStatus2Tex.alpha = 0f;


				selStatus = eSelectStatus.DP;
			}
			

		}
		);


	}

	
}
