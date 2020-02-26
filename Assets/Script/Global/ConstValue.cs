using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstValue
{
    public const string CharacterTemplatePath =
        "JSON/CHARACTER_TEMPLATE";
    public const string CharacterTemplateKey =
        "CHARACTER_TEMPLATE";

	//6.24 교준
	public const string ObjectTemplatePath =
	"JSON/OBJECT_TEMPLATE";
	public const string ObjectTemplateKey =
	   "OBJECT_TEMPLATE";
	public const string BuffTemplatePath = "JSON/BUFF_TEMPLATE";
	public const string BuffTemplateKey =
	   "BUFF_TEMPLATE";

	public const string SkillTemplatePath = "JSON/SKILL_TEMPLATE";
    public const string SkillDataPath = "JSON/SKILL_DATA";


	public const string BasicStatusTemplatePath = "JSON/BASIC_STATUS_TEMPLATE";
	public const string BasicStatusTemplateKey =
	   "BASIC_STATUS_TEMPLATE";

	//6.27 은기
	public const string MapTemplatePath = "JSON/MAP_TEMPLATE";
    public const string MapTemplateKey = "MAP_TEMPLATE";

    // StatusData Key 관련
    public const string CharacterStatusDataKey =
        "CHARACTER_TEMPLATE";

    // GetData Key관련
    public const string ActorData_Team = "TEAM_TYPE";
    public const string ActorData_SetTarget = "SET_TARGET";
	//6.25 교준
	public const string ActorData_GetThisActor = "GET_ACTOR";
	public const string ActorData_GetTarget = "GET_TARGET";


    public const string ActorData_AttackRange = "ATTACK_RANGE";
    public const string ActorData_Character = "CHARACTER";
    public const string ActorData_Hit = "HIT";
    public const string ActorData_SkillData = "SKILL_DATA";

    // ThrowEvent Key 관련
    public const string EventKey_EnemyInit = "E_INIT";
    public const string EventKey_Hit = "E_HIT";
    public const string EventKey_SelectSkill = "SELECT_SKILL";
    public const string EventKey_SelectModel = "SELECT_SKILL_MODEL";
	public const string EventKey_SelectState = "SELECT_STATE";

	// SetData Key 관련
	public const string SetData_HP = "BOARD_HP";
    public const string SetData_Damage = "BOARD_DAMAGE";
	public const string SetData_Status = "BOARD_STATUS";	//0705 추가분.


	// UI Path  관련
	public const string UI_PATH_HP = "Prefabs/UI/HP_Board";
	public const string UI_PATH_ObjHP = "Prefabs/UI/ObjHP_Board";
    public const string UI_PATH_Damage = "Prefabs/UI/Damage_Board";
	public const string UI_PATH_STATUS = "Prefabs/UI/StatusBoard";    //0705 추가분.

	public const string LocalSave_ItemInstance = "ITEM_INSTANCE";

	// 아이템 관련
	public const string GetData_ShieldSprite = "Equipment_Shield";
	public const string GetData_SwordSprite = "Weapons_Sword";
	public const string GetData_BowSprite = "Weapons_Bow";
	public const string GetData_AxeSprite = "Weapons_Axe";
	public const string GetData_AvoidSprite = "Modifiers_BoostSpeed";
	public const string GetData_LeapSprite = "Modifiers_LevelUp";
}
