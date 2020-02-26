using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //홍은기 등장씬 추가
    public bool IsStartedGame = false;
    float StartWalk = 0.0f;
	GameObject TreasureBox = null;
    GameObject Box1 = null;
    GameObject Box2 = null;
    GameObject Box3 = null;

    bool IsInit = false;
	public Actor PlayerActor;
    public Actor PLAYER_ACTOR
    {
        get { return PlayerActor; }
    }
    ComboSkill combo;
    DoorOpen DoorLeft;
    DoorOpen DoorRight;
    public ComboSkill COMBOSYSTEM
    {
        get { return combo; }
        set { combo = value; }
    }
    //public int SelectStage = 0;
    //StageInfo SelectStageInfo = null;

    // Stage 관련 ======================
    public int SelectStage = 1;
    StageInfo SelectStageInfo = null;
    GameObject Map = null;

	bool isGameClear = false;

	public bool GAME_CLEAR
	{
		get
		{
			return isGameClear;
		}
		set
		{
			isGameClear = value;
		}
	} 

    bool IsGameOver = true;
	public bool GAME_OVER { get { return IsGameOver; } }
	//float StackTime = 0.0f;
	//int KillCount = 0;


	private void Awake()
	{
		TreasureBox = Resources.Load("Prefabs/Objects/Chest_Close") as GameObject;
        
	}

	public void GameInit()
	{
		if (IsInit == true)
			return;

        StageManager.Instance.StageInit();

        //교준 0625
        //	PlayerPrefs.DeleteKey(ConstValue.LocalSave_ItemInstance);	// 게임 시작때 지정한 키값과 데이터값을 제거한다.

        //
        //combo = new ComboSkill();
        //combo.Start();
		//홍은기 왼쪽문 오른쪽문
		//DoorLeft = GameObject.Find("Door/DoorOpenLeft").GetComponent<DoorOpen>();
		//DoorRight = GameObject.Find("Door/DoorOpenRight").GetComponent<DoorOpen>();
		//StartWalk = 0.0f;
		IsInit = true;
		//IsStartedGame = true;

		UI_Tools.Instance.HideUI(eUIType.PF_UI_INGAME);
	}

    public void LoadGame()
	{
		////Init
		//StackTime = 0.0f;
		//KillCount = 0;
		IsGameOver = false;

		////StageLoad
		//SelectStageInfo = StageManager.Instance.LoadStage(SelectStage);

		//PlayerLoad
		PlayerActor = ActorManager.Instance.PlayerLoad();

		//홍은기 게임 로드시 무적
		//PlayerActor.IS_INVUL = true;
		//PlayerActor.transform.localPosition = new Vector3(-4, 0, 0);
		//PlayerActor.transform.rotation = Quaternion.Euler(0, 70f, 0);
		//DoorLeft.m_IsOpen = true;
		//DoorRight.m_IsOpen = true;

		ItemManager.Instance.ItemInit();
		UI_Tools.Instance.ShowUI(eUIType.PF_UI_INGAME);
        ResetMap();
	}

    public void StartGame()
    {
        IsStartedGame = false;
        DoorLeft.m_IsOpen = false;
        DoorRight.m_IsOpen = false;
        PlayerActor.IS_INVUL = false;
    }

	void Update()
	{
		//홍은기 게임시작중일시 문이 열림
        //if(IsStartedGame)
        //{

        //    StartWalk += Time.deltaTime;
        //    PlayerActor.transform.localPosition += new Vector3(0.03f, 0, 0);
        //    if (StartWalk > 2.5f)
        //    {
        //        StartGame();
        //    }
        //}


        //if (IsGameOver == true)
        //	return;

        ////Scene != GameScene return;
        //if (Scene_Manager.Instance.CURRENT_SCENE != eSceneType.SCENE_GAME)
        //	return;


        //if (SelectStageInfo.CLEAR_TYPE == eClearType.CLEAR_TIME)
        //{
        //	StackTime += Time.deltaTime;
        //	//UISetting
        //	UIManager.Instance.SetText(false, (float)SelectStageInfo.CLEAR_FINISH - StackTime);


        //	if (SelectStageInfo.CLEAR_FINISH < StackTime)
        //	{
        //		IsGameOver = true;
        //		SetGameOver();

        //	}
        //}

        //combo.SKillCheck();
    }
	

	void SetGameOver()
	{
		//Time.timeScale = 0.1f;
		//Debug.Log("GameOver");
		//Invoke("GoLobby", 0.1f);    //Time.timescale로 시간의 스케일을 바꾸었기 때문에 0.1로 하면 1초를 의미한다. 이는 공용이기 때문
		//							//timescale을 조정하는 클래스를 만드는 것이 유용. 래핑.
	}

	public void GameClear()
	{
		isGameClear = true;
		CreateTreasureBox();
	}

	void CreateTreasureBox()
	{
		Box2 = Instantiate(TreasureBox);
		Box2.transform.position = new Vector3(0, 0, 0);
		ParticleManager.Instance.CreateDustParticle(Box2.transform.position);

		Box1 = Instantiate(TreasureBox);
		Box1.transform.position = new Vector3(-3, 0, 0);
		ParticleManager.Instance.CreateDustParticle(Box1.transform.position);

		Box3 = Instantiate(TreasureBox);
		Box3.transform.position = new Vector3(3, 0, 0);
		ParticleManager.Instance.CreateDustParticle(Box3.transform.position);
	}

    //void GoLobby()
    //{

//	Time.timeScale = 1f;
//	Scene_Manager.Instance.LoadScene(eSceneType.SCENE_LOBBY);
//	//Scene Load Lobby
//}

    public void ResetMap()
    {
        if (Map != null)
        {
            Destroy(Map);
        }

        GameObject map = Resources.Load("Prefabs/Map/MapGenerator") as GameObject;
        Map = Instantiate(map);
        DestroyChest();
    }

    void DestroyChest()
    {
        Destroy(Box1);
        Destroy(Box2);
        Destroy(Box3);
    }
}
