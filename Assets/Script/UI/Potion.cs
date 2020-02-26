using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {

	bool isPotionAble = false;
	//float liquidPosY = -54.5f;
	//float liquidScaleX = 0.5f;
	//float liquidScaleY = 0.1f;


	[SerializeField]
	public int unit = 6;

	//float curTime=0;
	float amountOfPotion=0;
	//int numOfUsedPotion = 0;

	//public int AMOUNT_POTION
	//{
	//	get
	//	{
	//		return amountOfPotion;
	//	}
	//	set
	//	{
	//		amountOfPotion = value;
	//	}
	//}
	UIButton potionBtn = null;
	//AnchorPoint liquidAnc = null;
	UITexture liquidTex = null;
	Transform liauidTrans = null;

	eBuffType buffType = eBuffType.NONE;
	string typeOfBuff = string.Empty;

	private void Awake()
	{
		potionBtn = GetComponentInChildren<UIButton>();
		//liquidAnc = transform.Find("Liquid").GetComponent<UITexture>().topAnchor.Set(,);
		liauidTrans = transform.Find("Model").Find("Liquid").GetComponent<Transform>();
		liquidTex = transform.Find("Model").Find("Liquid").GetComponent<UITexture>();
	}


	// Use this for initialization
	void Start () {

		

		EventDelegate.Add(potionBtn.onClick, () =>
		 {

			// Debug.Log(ActorManager.Instance.DIED_ENEMY_NUM);

			 if (isPotionAble)
			 {
				 //PotionHeal();
				 //liquidScaleX = 0.5f;
				 //liquidScaleY = 0.1f;
				 //liquidPosY = -54.5f;

				 //liauidTrans.localScale = new Vector3(liquidScaleX, liquidScaleY, 0);
				 //liauidTrans.localPosition = new Vector3(0, liquidPosY, 0);
				 //isPotionAble = false;
				 //numOfUsedPotion++;

				 PotionHeal();
				 liquidTex.width = 0;
				 liquidTex.height = 0;

			
				 isPotionAble = false;
				 //numOfUsedPotion++;
				 amountOfPotion = 0;
			 }
		 }
		 );	
	}
	
	// Update is called once per frame
	void Update () {
		//curTime += Time.deltaTime * amountOfPotion;
	//	PotionAmount();
		//liquidTex.topAnchor.Set(curTime, liquidTex.topAnchor.absolute);


	}

	void PotionHeal()
	{
		buffType = eBuffType.HEAL;
		typeOfBuff = BuffManager.Instance.CreateBuff(buffType, 1);
		GameManager.Instance.PlayerActor.CUR_BUFF = typeOfBuff;
	}

	//public void PotionAmount(int diedEnemyNum)
	//{
	//	amountOfPotion = diedEnemyNum;
	////	Debug.Log()

	//	if (liquidScaleX < 0.81)
	//		liquidScaleX = liauidTrans.localScale.x + (amountOfPotion / 5f) - (numOfUsedPotion * 0.31f);

		
			



	//	if (liquidScaleY < 1.8)
	//		liquidScaleY = liauidTrans.localScale.y + (amountOfPotion / 2f) - (numOfUsedPotion * 1.7f);
		

	//	if (liquidPosY < -32)
	//		liquidPosY = liauidTrans.localPosition.y + (amountOfPotion / 0.1f) - (numOfUsedPotion * 22.5f);
		

	//	if(liquidScaleX>=0.81f)
	//	{
	//		liquidScaleX = 0.81f;
	//	}
	//	if(liquidScaleY >=1.8f)
	//	{
	//		liquidScaleY = 1.8f;
	//		isPotionAble = true;
	//	}
	//	if (liquidPosY >= -32)
	//	{
	//		liquidPosY = -32;
	//	}

	//		liauidTrans.localScale = new Vector3(liquidScaleX, liquidScaleY, 0);
	//	liauidTrans.localPosition = new Vector3(0, liquidPosY, 0);
	//}


	public void PotionAmount()
	{
		//amountOfPotion = deadEnemyNum - numOfUsedPotion* unit;
		amountOfPotion++;
		//	Debug.Log()

		if (amountOfPotion < unit)
		{
			liquidTex.width = (int)((amountOfPotion/unit)*80f);
			liquidTex.height = (int)((amountOfPotion / unit)*80f);
		}

	
		if (amountOfPotion >= unit)
		{
			liquidTex.width = 80;
			liquidTex.height = 80;
			isPotionAble = true;
		}
		
	}
}
