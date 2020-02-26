
public enum eBaseObjectState
{
    STATE_NORMAL,
    STATE_DIE,
}

public enum eStateType
{
    STATE_NONE = 0,
    STATE_IDLE,
    STATE_ATTACK,
    STATE_WALK,
    STATE_DEAD,
	STATE_SPECIAL,
}
//은기 스턴, 워크 상태 추가
public enum ePlayerStateType
{
	STATE_NONE = 0,
	STATE_IDLE,
	STATE_ATTACK,
	STATE_CROUCH,
	STATE_DEAD,
	STATE_STUN,
    STATE_WALK,
}

public enum eStatusData
{
    MAX_HP,
    ATTACK,
    DEFENCE,
    MAX,
}

public enum eTeamType
{
    TEAM_1,
    TEAM_2,
}


// Monster 관련
public enum eRegeneratorType
{
    NONE,
    REGENTIME_EVENT,
    TRIGGER_EVENT,
}

public enum eEnemyType
{
    A_Enemy,
	B_Enemy,
	C_Enemy,
	Boss_Enemy,
	MAX,
}

// Skill 관련
public enum eSkillTemplateType
{
    TARGET_ATTACK,
	TARGET_BLOCK,
    RANGE_ATTACK,
	CHARGE_ATTACK,
}

public enum eSkillAttackRangeType
{
    RANGE_BOX,
    RANGE_SPHERE,
	RANGE_CAPSULE,
}

public enum eSkillModelType
{
    BOX,
	Arrow_Regular,
	MAX
}

public enum eBoardType
{
    BOARD_NONE,
    BOARD_HP,
	BOARD_OBJ_HP,
	BOARD_DAMAGE,
	BOARD_STATUS,
}

public enum eClearType
{
    CLEAR_KILLCOUNT = 0,
    CLEAR_TIME,
}

public enum eSceneType
{
    SCENE_NONE,
    SCENE_TITLE,
    SCENE_GAME,
    SCENE_LOBBY,
}

public enum eUIType
{
    PF_UI_TITLE,
    PF_UI_LOADING,
    PF_UI_LOBBY,
    PF_UI_INVENTORY,
    PF_UI_POPUP,
	PF_UI_MENU,
	PF_UI_STATUS_UP,
	PF_UI_INGAME,
    PF_UI_STAGESELECT,
    PF_UI_TOBOSS,
    PF_UI_STAGE,
    PF_UI_STAGEICON,
}

public enum eSlotType
{
    SLOT_NONE = -1,
	SLOT_1,
	SLOT_2,
	SLOT_MAX,
}

// AI 관련
public enum eAIType
{
	NormalEnemy,
	RangeEnemy,
	GiantEnemy,
	BossEnemy,
    MiniBossEnemy,
	MAX,
}

public enum STEP
{

    NONE = -1,

    IDLE = 0,       // 대기 중.
    DRAWING,        // 라인 그리는 중 （드래그 중）.
    DRAWED,         // 라인 그리기 종료. 
    CREATED,        // 도로 모델이 생성됨.

    NUM,
};

public enum eBuffType
{
	NONE,
	ATTACK,
	DEFENCE,
	HEAL,
	MAX
}
public enum eSelectItem
{
	NONE,
	SELECT1,
	SELECT2,
}

public enum eSelectStatus
{
	NONE,
	HP,
	AP,
	DP,
}

//던전 타입 추가
public enum eMapTemplateType
{
    D_TYPE = 0,
    F_TYPE,
    BOSS_ROOM,
}