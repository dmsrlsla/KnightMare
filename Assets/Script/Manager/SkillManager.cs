using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
	public BaseSkill makeSkill = null;
	Transform parentTransform = null;

	Dictionary<BaseObject, List<BaseSkill>> DicUseSkill
		= new Dictionary<BaseObject, List<BaseSkill>>();
	public Dictionary<BaseObject, List<BaseSkill>> DIC_USE_SKILL
	{ get { return DicUseSkill; } }

	Dictionary<string, SkillData> DicSkillData = 
		new Dictionary<string, SkillData>();
	Dictionary<string, SkillTemplate> DicSkillTemplate =
		new Dictionary<string, SkillTemplate>();

	Dictionary<eSkillModelType, GameObject> DicModel =
		new Dictionary<eSkillModelType, GameObject>();

	private void Awake()
	{
		LoadSkillData(ConstValue.SkillDataPath);
		LoadSkillTemplate(ConstValue.SkillTemplatePath);
		LoadSkillModel();
	}

	void LoadSkillData(string strFilePath)
	{
		TextAsset skillAssetData =
			Resources.Load(strFilePath) as TextAsset;
		if(skillAssetData == null)
		{
			Debug.LogError("Skill Data 불러오지 못함.");
			return;
		}

		JSONNode rootNode = JSON.Parse(skillAssetData.text);
		if (rootNode == null)
			return;

		JSONObject skillDataNode = rootNode["SKILL_DATA"] as JSONObject;
		foreach(KeyValuePair<string,JSONNode> pair in skillDataNode)
		{
			SkillData skillData = new SkillData(pair.Key, pair.Value);
			DicSkillData.Add(pair.Key, skillData);
		}
	}

	void LoadSkillTemplate(string strFilePath)
	{
		TextAsset skillAssetData =
			Resources.Load(strFilePath) as TextAsset;

		if(skillAssetData == null)
		{
			Debug.LogError("Skill Template 로드 실패");
			return;
		}

		JSONNode rootNode = JSON.Parse(skillAssetData.text);
		if (rootNode == null)
			return;

		JSONObject skillDataNode = rootNode["SKILL_TEMPLATE"] as JSONObject;
		foreach(KeyValuePair<string ,JSONNode> pair in skillDataNode)
		{
			SkillTemplate skillTemplate = new SkillTemplate(pair.Key,pair.Value);
			DicSkillTemplate.Add(pair.Key, skillTemplate);
		}
	}

	public void LoadSkillModel()
	{
		for (int i = 0; i < (int)eSkillModelType.MAX; i++)
		{
			GameObject go = Resources.Load("Prefabs/Skill_Models/"
				+ ((eSkillModelType)i).ToString()) as GameObject;

			if( go == null)
			{
				Debug.LogError("Prefabs/Skill_Models/"
				+ ((eSkillModelType)i).ToString() + " 파일을 찾지 못함");
				continue;
			}

			DicModel.Add((eSkillModelType)i, go);
		}
	}

	 public GameObject GetModel(eSkillModelType type)
	{
		if(DicModel.ContainsKey(type))
		{
			return DicModel[type];
		}
		else
		{
			Debug.LogError(type.ToString() + " is null");
			return null;
		}
	}

	public SkillData GetSkillData(string _strKey)
	{
		SkillData skillData = null;
		DicSkillData.TryGetValue(_strKey, out skillData);
		return skillData;
	}

	public SkillTemplate GetSkillTemplate(string _strKey)
	{
		SkillTemplate skillTemplate = null;
		DicSkillTemplate.TryGetValue(_strKey, out skillTemplate);
		return skillTemplate;
	}

	public void RunSkill(
		BaseObject keyObject, string strSkillTemplateKey)
	{
		SkillTemplate template = GetSkillTemplate(strSkillTemplateKey);
		if(template == null)
		{
			Debug.LogError(strSkillTemplateKey +
				" 키를 찾을수 없습니다.");
			return;
		}
		BaseSkill runSkill = CreateSkill(keyObject, template);
		RunSkill(keyObject, runSkill);
	}

	public void RunSkill(BaseObject keyObject, BaseSkill runSkill)
	{
		List<BaseSkill> listSkill = null;
		if (DicUseSkill.ContainsKey(keyObject) == false)
		{
			listSkill = new List<BaseSkill>();
			DicUseSkill.Add(keyObject, listSkill);
		}
		else
			listSkill = DicUseSkill[keyObject];

		listSkill.Add(runSkill);		
	}
	
	BaseSkill CreateSkill(
		BaseObject owner, SkillTemplate skillTemplate)
	{
		//BaseSkill makeSkill = null;
		makeSkill = null;
		GameObject skillObject = new GameObject();

		//Transform parentTransform = null;
		 parentTransform = null;
		//은기 주력으로 수정해야 할 부분
		switch (skillTemplate.SKILL_TYPE)
		{

			case eSkillTemplateType.TARGET_ATTACK:
				{
					makeSkill = skillObject.AddComponent<MeleeSkill>();
					parentTransform = owner.SelfTransform;
				}
				break;

			case eSkillTemplateType.TARGET_BLOCK:
				{
					makeSkill = skillObject.AddComponent<BlockSkill>();
					parentTransform = owner.SelfTransform;
				}
				break;

			case eSkillTemplateType.RANGE_ATTACK:
				{
					makeSkill = skillObject.AddComponent<RangeSkill>();
					parentTransform = owner.SelfTransform;
					parentTransform = owner.FindInChild("FirePos");


					makeSkill.ThrowEvent(ConstValue.EventKey_SelectModel,
						 GetModel(eSkillModelType.Arrow_Regular));
				}
				break;
		}

		skillObject.name = skillTemplate.SKILL_TYPE.ToString();

		//skillObject.transform.localScale = parentTransform.localScale;

		if (makeSkill != null)
		{
			makeSkill.transform.position = parentTransform.position;
			makeSkill.transform.rotation = parentTransform.rotation;
			//

			makeSkill.OWNER = owner;
			makeSkill.SKILL_TEMPLATE = skillTemplate;
			makeSkill.TARGET = 
				owner.GetData(ConstValue.ActorData_GetTarget) as BaseObject;

			makeSkill.InitSkill();
		}

		switch (skillTemplate.RANGE_TYPE)
		{
			//skillTemplate	
			case eSkillAttackRangeType.RANGE_BOX:
				{
					BoxCollider collider = skillObject.AddComponent<BoxCollider>();
					collider.size = new Vector3(skillTemplate.RANGE_DATA_1,
						skillTemplate.RANGE_DATA_2, skillTemplate.RANGE_DATA_3);// 건희 06/27
					collider.center = new Vector3(
						skillTemplate.RANGE_CENTER_1,
						skillTemplate.RANGE_CENTER_2,
						skillTemplate.RANGE_CENTER_3);// 건희 06/27
					collider.isTrigger = true;
				}
				break;
			case eSkillAttackRangeType.RANGE_SPHERE:
				{
					SphereCollider collider = skillObject.AddComponent<SphereCollider>();
					collider.radius = skillTemplate.RANGE_DATA_1;
					collider.isTrigger = true;
				}
				break;
			case eSkillAttackRangeType.RANGE_CAPSULE:
				{
					CapsuleCollider collider = skillObject.AddComponent<CapsuleCollider>();
					collider.radius = skillTemplate.RANGE_DATA_1;
					collider.height = skillTemplate.RANGE_DATA_2;
					collider.direction = 2;
					collider.isTrigger = true;
				}
				break;
		}

		return makeSkill;
	}

	public void Update()
	{
        //교준, 은기, 건희 셋중
        //if (GameManager.Instance.GAME_OVER)
        //    return;

		foreach(KeyValuePair<BaseObject, List<BaseSkill>> pair 
			in DicUseSkill)
		{
			List<BaseSkill> list = pair.Value;

			for(int i =0; i < list.Count;i++)
			{
				BaseSkill updateSkill = list[i];
				updateSkill.UpdateSkill();
				if(updateSkill.END)
				{
					list.Remove(updateSkill);
					Destroy(updateSkill.gameObject);
				}
			}
		}

		////추가 170622 am10:13
		//if (makeSkill != null)
		//{
		//	makeSkill.transform.position = parentTransform.position;
		//	makeSkill.transform.rotation = parentTransform.rotation;
		//	//
		//}


		}
		public void ClearSkill()
    {
        foreach (KeyValuePair<BaseObject, List<BaseSkill>> pair
            in DicUseSkill)
        {
            List<BaseSkill> list = pair.Value;

            for (int i = 0; i < list.Count; i++)
            {
                BaseSkill updateSkill = list[i];
                list.Remove(updateSkill);
                Destroy(updateSkill.gameObject);
            }
        }
        DicUseSkill.Clear();
    }

}
