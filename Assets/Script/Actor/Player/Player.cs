using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]

public class Player : Actor {



	

	Potion PlayerPotion;

	public Potion PLAYER_POTION
	{
		get
		{
			return PlayerPotion;
		}
	}

	protected ePlayerStateType CurrentState = ePlayerStateType.STATE_IDLE;
	public ePlayerStateType CURRENT_STATE
	{
		get { return CurrentState; }
	}
	Animator Anim = null;
	public Animator ANIMATOR
	{
		get
		{
			if (Anim == null)
			{
				Anim = gameObject.GetComponentInChildren<Animator>();
			}
			return Anim;
		}
	}

	bool bAttack = false;
	public bool IS_ATTACK
	{
		get { return bAttack; }
		set { bAttack = value; }
	}
	//홍은기 스턴 상태 추가
	bool bStun = false;
	public bool IS_STUN
	{
		get { return bStun; }
		set { bStun = value; }
	}


	bool playerAlive = true;
	/// <summary>
	/// /
	/// </summary>

	

	//Vector3 vec3Dis;
	Vector3 playerposition;
	Vector3 preTransform;
	float curTime=0f;
	bool hoverOnPlayer = false;

	//public float MoveSpeed;
	public float Dis = 0;
	public float limitDis = 8;
	public float moveSpeed = 5;
	PlayerController controller;
	Camera viewCamera;
	Vector3 heightCorrectedPoint;
	bool moving=false;
    //*********은기 소스 추가구문 6.29*********//
    public float Velocity = 0.0f;
	//*********은기 소스 추가 콤보정리 7.12*********//
    public DrawTimeSkill DrawSkillSet = null;
    public ComboSkill ComboSkillSet = null;
    private Vector3 NextPosition = Vector3.zero;
    private int NextPositionNum = 0;

	 void Awake()
	{

		//0702 교준, Actor에게 자신이 플레이어 Actor라는 것을 알려주기 위해, actor에서 awake단에서 HPbar를 생성하므로 이곳에 넣었다.
		this.IS_PLAYER = true;

		base.Awake();
	}

	//*********은기 소스 추가구문 6.29*********//
	// Use this for initialization
	void Start () {

		PlayerPotion = GameObject.Find("Potion").GetComponent<Potion>();

		controller = GetComponent<PlayerController>();
		viewCamera = Camera.main;
        //*********은기 소스 추가구문 6.29*********//
        DrawSkillSet = ComboSkillSet.DrawSkill;
       // ProcessWalk();
    }

	// Update is called once per frame
	new void Update() {

        //은기 (플레이어가 콤보를 가지고 체크함)
        ComboSkillSet.SKillCheck();

        bool onCrouch = false;

		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		controller.Move(moveVelocity);

		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayDistance;
        //홍은기 첫등장시, 스테이지 클리어시
        if (GameManager.Instance.IsStartedGame)
        {
            ProcessWalk();
        }
        

        //0626 교준, 죽었을 경우
        if (SELF_CHARACTER.TargetComponenet.OBJECT_STATE == eBaseObjectState.STATE_DIE && playerAlive==true)
		{
			ProcessDie();
			BloodManager.Instance.CreateBloodParticle(gameObject,gameObject.transform.localPosition,new Vector3(-90,0,0),100,300,3,0.3f);
			playerAlive = false;

			PlayerPrefs.DeleteKey(ConstValue.LocalSave_ItemInstance);   // 게임 시작때 지정한 키값과 데이터값을 제거한다.
		}

		if(playerAlive)
		{

			if (groundPlane.Raycast(ray, out rayDistance))
			//true라면 ray가 바닥 플레인과 교차한 것이고 
			//그러면 카메라에서 ray가 부딫친 지점까지의 거리를 알 수 있고,
			//그럼 실제 교차 지점을 지정할 수 있음.
			{


                Vector3 point = ray.GetPoint(rayDistance);
				//Debug.Log(rayDistance);
				Debug.DrawLine(ray.origin, point, Color.red);

				RaycastHit hit;
				//홍은기 스턴일 경우 멈춤
				if (Physics.Raycast(ray, out hit, rayDistance) && Input.GetMouseButton(0) && bStun == false)
				{
					if (hit.collider.tag == "Player")
					{
						hoverOnPlayer = true;	
					}
                    //클릭시 바로 게임 시작 은기
                    if (GameManager.Instance.IsStartedGame)
                    {
                        GameManager.Instance.StartGame();
                    }
                }

				//
				if (moving == false && hoverOnPlayer == true)
				{

					//
					ProcessCrouch();
					onCrouch = true;

					heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);


					//	vec3Dis = heightCorrectedPoint - transform.position;
					//con = vec3Dis.normalized * moveSpeed;


					//Debug.Log(heightCorrectedPoint);
					controller.LookAt(heightCorrectedPoint);

					preTransform = transform.position;
					playerposition = preTransform;
					//Dis = Vector3.Distance(preTransform, heightCorrectedPoint);

				}

				

			}


			if (Input.GetMouseButtonUp(0) && hoverOnPlayer == true)
			{

				moving = true;
				hoverOnPlayer = false;
			}

			if(moving)
			{

                //onCrouch = false;
                ProcessAttack();
                

                curTime += Time.deltaTime;

				//움직이는 속도를 일정하게 할 것인가?
				playerposition = Vector3.MoveTowards(preTransform, heightCorrectedPoint, limitDis * curTime * 3f);
				//playerposition = Vector3.Lerp(playerposition, heightCorrectedPoint, curTime*3f );

				transform.position = playerposition;

				Dis = Vector3.Distance(preTransform, playerposition);

				float goalDis = Vector3.Distance(transform.position, heightCorrectedPoint);

				//transform.position==heightCorrectedPoint
				if (goalDis ==0)
				{
					Debug.Log("도착");
					
					moving = false;
					curTime = 0;
					//hoverOnPlayer = false;
					ProcessIdle();
					Destroy(ParticleManager.Instance.SPEED_LINE_P_INS);

					//ParticleManager.Instance.CreateFlashParticle(gameObject.transform.localPosition, gameObject.transform.localRotation);
				}

				else if (Dis >= limitDis)
				{
					moving = false;
					curTime = 0;
					//hoverOnPlayer = false;
					ProcessIdle();
					Destroy(ParticleManager.Instance.SPEED_LINE_P_INS);

					//ParticleManager.Instance.CreateFlashParticle(gameObject.transform.localPosition, gameObject.transform.localRotation);
				}
				
			}
            //************* 은기 6.29 추가구문***********//
            else if (ComboSkillSet.IS_START_SKILL == true)
            {
                //DrawSkill.DrawReal();
                ProcessAttack();
                transform.position = DrawSkillSet.positions[NextPositionNum];

                NextPositionNum++;
                if (DrawSkillSet.position_num <= NextPositionNum)
                {
                    ComboSkillSet.IS_START_SKILL = false;
                    ComboSkillSet.SkillTimeCheck = 0.0f;
                    ComboSkillSet.POWER_ON = false;
                    ComboSkillSet.COMBO_GAGE = 0.0f;
                    curTime = 0;
                    ProcessIdle();
                    NextPositionNum = 0;
                    DrawSkillSet.position_num = 0;
                }

            }
			//************* 은기 6.29 추가구문***********//
			//if (Input.GetMouseButtonUp(0)|| Moving==true)
			//{
			//	Moving = true;
			//	Debug.Log("Move!");

			//	Vector3 ContmoveVelocity = heightCorrectedPoint.normalized * moveSpeed;
			//	controller.Move(ContmoveVelocity);

			//	if(controller.transform.position== heightCorrectedPoint)
			//	{
			//		Moving = false;
			//	}
			//}


		
			//0626 교준, 버프체크를 위해.
			base.Update();
		}

		
	}

	//!!
	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.gameObject.tag == "Enemy")
	//	{
	//		Enemy TargetEnemy =  other.gameObject.GetComponent<Enemy>();
	//		ThrowEvent(ConstValue.ActorData_SetTarget, TargetEnemy);

	//		//스킬이 생성될 때 타켓을 정해주는데, throw이벤트로 타겟을 정해주는 것은 그 후임.
	//		SkillManager.Instance.makeSkill.TARGET = SkillManager.Instance.makeSkill.OWNER.GetData(ConstValue.ActorData_GetTarget) as BaseObject;
	//		//Destroy(other.gameObject);
	//	}
	//}



	//private void OnCollisionEnter(Collision collision)
	//{
	//	if(collision.gameObject.tag=="Enemy")
	//	{
	//		Target.ThrowEvent(ConstValue.ActorData_SetTarget,	collision.gameObject);
	//		Destroy(collision.gameObject);
	//	}
	//}

	public override void basicStatusUp(eStatusData type)
	{
		switch (type)
		{
			case eStatusData.MAX_HP:
				HPStatus++;
				SELF_CHARACTER.CHARACTER_STATUS.AddStatusData(type.ToString(), BasicStatusManager.Instance.Get(type.ToString(), HPStatus).STATUS);

				BaseBoard board = BoardManager.Instance.GetBoardData(this,eBoardType.BOARD_HP);
				if (board != null)
					board.SetData(ConstValue.SetData_HP,
						GetStatusData(eStatusData.MAX_HP),
						SELF_CHARACTER.CURRENT_HP);

				//// Board 초기화
				board = null;

				break;
			case eStatusData.ATTACK:
				APStatus++;
				SELF_CHARACTER.CHARACTER_STATUS.AddStatusData(type.ToString(), BasicStatusManager.Instance.Get(type.ToString(), APStatus).STATUS);

				break;
			case eStatusData.DEFENCE:
				DPStatus++;
				SELF_CHARACTER.CHARACTER_STATUS.AddStatusData(type.ToString(), BasicStatusManager.Instance.Get(type.ToString(), DPStatus).STATUS);
				break;

			default:
				break;
		}

		Debug.Log("스테이터스 업! -> "+ type.ToString() +"이"+ SELF_CHARACTER.CHARACTER_STATUS.GetStatusData(type));

	

	}

	public override void equipItemStatus()
	{
		equipDic = ItemManager.Instance.DIC_EQUIP;
		ItemInstance tempItemIns;
		if (equipDic.Count >= 1)
		{
			equipDic.TryGetValue(eSlotType.SLOT_1, out tempItemIns);
			SELF_CHARACTER.CHARACTER_STATUS.AddStatusData("ITEM1", tempItemIns.ITEM_INFO.STATUS);
		}

		if (equipDic.Count == 2)
		{
			equipDic.TryGetValue(eSlotType.SLOT_2, out tempItemIns);
			SELF_CHARACTER.CHARACTER_STATUS.AddStatusData("ITEM2", tempItemIns.ITEM_INFO.STATUS);
		}

		BaseBoard board = BoardManager.Instance.GetBoardData(this,
			eBoardType.BOARD_HP);
		if (board != null)
			board.SetData(ConstValue.SetData_HP,
				GetStatusData(eStatusData.MAX_HP),
				SELF_CHARACTER.CURRENT_HP);

		//// Board 초기화
		board = null;

	}

	public void Stun()
	{
		//Vector3 dirNormal = transform.rotation.eulerAngles.normalized;

		//GetComponent<Rigidbody>().AddForce(dirNormal * 1000f);

		//	Vector3 dirNormal = moveDir.normalized;

		Destroy(ParticleManager.Instance.SPEED_LINE_P_INS);

		moving = false;
		curTime = 0;
		//hoverOnPlayer = false;
		ProcessStun();

		//GetComponent<Rigidbody>().AddForce(dirNormal * 100f);

	}

	void ChangeAnimation()
	{
		if (ANIMATOR == null)
		{
			Debug.LogError(gameObject.name + " 에게 Animator가 없습니다.");
			return;
		}
		//은기 에니메이션 상태 확인 로그 추가
        //Debug.Log("현재 애니메이션 상태는"+CurrentState + " 입니다.");
        ANIMATOR.SetInteger("State", (int)CurrentState);
	//	Debug.Log(CurrentState);
	}
	// 은기 걷기 상태 추가
    protected virtual void ProcessWalk()
    {
        CurrentState = ePlayerStateType.STATE_WALK;
        ChangeAnimation();
    }

	protected virtual void ProcessIdle()
	{
		CurrentState = ePlayerStateType.STATE_IDLE;
		ChangeAnimation();
	}

	protected virtual void ProcessCrouch()
	{
		CurrentState = ePlayerStateType.STATE_CROUCH;
		ChangeAnimation();
	}

	protected virtual void ProcessAttack()
	{
		//Target.ThrowEvent(ConstValue.EventKey_SelectSkill, 0);
		this.ThrowEvent(ConstValue.EventKey_SelectSkill, 0);
		CurrentState = ePlayerStateType.STATE_ATTACK;
		ChangeAnimation();
	}

	protected virtual void ProcessDie()
	{
		CurrentState = ePlayerStateType.STATE_DEAD;
		ChangeAnimation();
	}
	//스턴상태 추가
	protected virtual void ProcessStun()
	{
		CurrentState = ePlayerStateType.STATE_STUN;
		ChangeAnimation();
		//Debug.LogError("");
	}

}
