using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBoard : BaseBoard
{
	UILabel StatusLabel;    //적 상태 글자표시.
	UISprite StatusSprite;	//적 상태 이미지 표시

	//UISprite StatusSprite;	//적 상태 스프라이트로 표기.

	Vector3 StatusBoardPosition;    //스테이터스 보드의 위치.

	public override eBoardType BOARD_TYPE
	{
		get
		{
			return eBoardType.BOARD_STATUS;
		}
	}

	private void Awake()
	{
		StatusLabel = this.GetComponentInChildren<UILabel>();
		if (StatusLabel == null)
		{
			Debug.LogError("StatusLabel Is Null");
			return;
		}
		StatusSprite = this.GetComponentInChildren<UISprite>();
		if (StatusSprite == null)
		{
			Debug.LogError("StatusSprite Is Null");
			return;
		}
	}

	public override void UpdateBoard()
	{
		base.UpdateBoard();

		this.transform.localPosition= this.transform.localPosition + (Vector3.up * 15f);
		//gameObject.transform.position = StatusBoardPosition;

	}

	public override void SetData(string strKey, params object[] datas)
	{

		if (strKey.Equals(ConstValue.SetData_Status)) 
		{
			eStateType CurStateType = (eStateType)datas[0];
			StatusSprite.GetComponent<UISprite>().spriteName = "rpg icons"; //아래 스프라이트를 저장한 아틀라스 소환.

			BaseObject SelfType = null;	//적의 종류.

			if (CurStateType == eStateType.STATE_ATTACK)
			{
				SelfType = (BaseObject)datas[1];

				if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_1")
				{
					StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_SwordSprite;
					StatusLabel.text = "공격";
				}
				else if(SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_2")
				{
					StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_BowSprite;
					StatusLabel.text = "공격";
				}
				else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_3")
				{
					StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_AxeSprite;
					StatusLabel.text = "공격";
				}
				else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_MINI_BOSS")
				{
					StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_BowSprite;
					StatusLabel.text = "공격";
				}
                else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_BOSS")
                {
                    StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_BowSprite;
                    StatusLabel.text = "공격";
                }

            }

			else if (CurStateType == eStateType.STATE_SPECIAL)
			{
				SelfType = (BaseObject)datas[1];


                if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_1")
                {
                    StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_ShieldSprite;
                    StatusLabel.text = "방어";
                }
                else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_2")
                {
                    StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_AvoidSprite;
                    StatusLabel.text = "회피";
                }
                else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_3")
                {
                    StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_LeapSprite;
                    StatusLabel.text = "도약";
                }
                //재인이형 부탁해요
                else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_BOSS"
                    && SelfType.GetComponent<NonPlayer>().AI.IS_SECOND_SKILL == false)
				{
					StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_ShieldSprite;
					StatusLabel.text = "화염발사";
				}
                else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_BOSS"
                    && SelfType.GetComponent<NonPlayer>().AI.IS_SECOND_SKILL == true)
                {
                    StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_ShieldSprite;
                    StatusLabel.text = "알낳기";
                }
                else if (SelfType.GetComponent<NonPlayer>().TEMPLATE_KEY == "ENEMY_MINI_BOSS")
                {
                    StatusSprite.GetComponent<UISprite>().spriteName = ConstValue.GetData_ShieldSprite;
                    StatusLabel.text = "화염발사";
                }
            }
			else
			{
				StatusLabel.text = "";
			}
		}
	}
}
