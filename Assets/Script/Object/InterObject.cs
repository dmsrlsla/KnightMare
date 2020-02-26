using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObject : TeamObject {

	

	[SerializeField]
	string TemplateKey = string.Empty;
	public string TEMPLATE_KEY
	{ get { return TemplateKey; } }

	GameObjects SelfObject = null;
	public GameObjects SELF_CHARACTER
	{ get { return SelfObject; } }


	// AI
	//BaseAI ai = null;
	//public BaseAI AI
	//{ get { return ai; } }


	BaseObject TargetObject = null;

	// Board -> HP Bar
	[SerializeField]
	bool bEnableBoard = true;

	protected void Awake()
	{
	
		GameObjects gameObjects =
			ObjectManager.Instance.AddObject(TemplateKey);
		gameObjects.TargetComponenet = this;
		SelfObject = gameObjects;


		if (bEnableBoard)//UI(boar)
		{
			BaseBoard board = BoardManager.Instance.AddBoard(
				this, eBoardType.BOARD_OBJ_HP);

			board.SetData(ConstValue.SetData_HP,
				GetStatusData(eStatusData.MAX_HP),
				SelfObject.CURRENT_HP);
		}

		InterObjectManager.Instance.AddObject(this);//Actor가 생성된 이후에 처리
	}

	public double GetStatusData(eStatusData statusData)
	{
		return SelfObject.OBJECT_STATUS.GetStatusData(statusData);
	}
	//대문자 오브젝트 클래스의 상위객체
	//소문자 오브젝트 자료형태(int형 float형 등등....)

	//파람즈 - 매개변수들(갯수제한, 형태제한 없음.) 불안전함. 최대한 간단한 데이터만. 규칙을 정리해야함.
	public override object GetData(string keyData, params object[] datas)
	{
		if (keyData == ConstValue.ActorData_Team)
			return TEAM_TYPE;
		else if (keyData == ConstValue.ActorData_Character)
			return SelfObject;
		else if (keyData == ConstValue.ActorData_GetTarget)
			return TargetObject;
	


		// Base 부모클래스 -> BaseObject
		return base.GetData(keyData, datas);
	}
	//메소드가 사용되어야 하는 상황을 구분짓기 위해 두개를 나눔.
	public override void ThrowEvent(string keyData, params object[] datas)
	{
		if (keyData == ConstValue.EventKey_Hit)
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
			{
				
				return;
			}
			// 공격 주체의 케릭터
			GameCharacter casterCharacter
				= datas[0] as GameCharacter;
			SkillTemplate skillTemplate =
				datas[1] as SkillTemplate;

			casterCharacter.CHARACTER_STATUS.AddStatusData("SKILL",
				skillTemplate.STATUS_DATA);

			//0625 교준, 버프에 관련된 구문이 없다 = 오브젝트들은 버프의 영향을 받지 않게 만들었다. 

			//데미지 수치를 랜덤값으로 만들면 더 좋다. 규칙을 정한것이지 구조가 아님. 다른곳에서도 처리 가능함.
			double attackDamage =
				casterCharacter
				.CHARACTER_STATUS
				.GetStatusData(eStatusData.ATTACK);
			// SelfCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.DEFFENCE);

			casterCharacter.CHARACTER_STATUS.RemoveStatusData("SKILL");

			SelfObject.IncreaseCurrentHP(-attackDamage);

			if(SelfObject.TargetComponenet.OBJECT_STATE== eBaseObjectState.STATE_DIE)
			{
				Break();
			}


			// HPBoard
			BaseBoard board = BoardManager.Instance.GetBoardData(this,
				eBoardType.BOARD_HP);
			if (board != null)
				board.SetData(ConstValue.SetData_HP,
					GetStatusData(eStatusData.MAX_HP),
					SelfObject.CURRENT_HP);


			// Board 초기화
			board = null;

			//// DamageBoard 
			//board = BoardManager.Instance.AddBoard(this, eBoardType.BOARD_DAMAGE);
			//if (board != null)
			//	board.SetData(ConstValue.SetData_Damage, attackDamage);

			//건희
			//// 피격 에니메이션
			//AI.ANIMATOR.SetInteger("Hit", 1);
		}
		
		else if (keyData == ConstValue.ActorData_SetTarget)
		{
			TargetObject = datas[0] as BaseObject;
		}
		else
			base.ThrowEvent(keyData, datas);
	}

	protected virtual void Break()
	{

	}


	protected virtual void Update()
	{


		//건희
		//AI.UpdateAI();
		//if(AI.END)
		//{
		//	Destroy(SelfObject);
		//}
	}
	

	private void OnEnable()
	{

		if (BoardManager.Instance != null)
			BoardManager.Instance.ShowBoard(this, true);
	}

	public void OnDisable()
	{

		if (BoardManager.Instance != null
				  && GameManager.Instance.GAME_OVER == false)
			BoardManager.Instance.ShowBoard(this, false);
	}
	//플레이어를 찾아보고 있을때 클리어함.
	public void OnDestroy()
	{
		if (BoardManager.Instance != null)
			BoardManager.Instance.ClearBoard(this);

		if (InterObjectManager.Instance != null)
		{
			InterObjectManager.Instance.RemoveObject(this);
		}
	}
}
