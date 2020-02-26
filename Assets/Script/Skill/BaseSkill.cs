using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : BaseObject
{
	public BaseObject OWNER
	{
		get;
		set;
	}

	public BaseObject TARGET
	{
		get;
		set;
	}

	public SkillTemplate SKILL_TEMPLATE
	{
		get;
		set;
	}

	public bool END
	{
		get;
		protected set;
	}

    public bool TEMP_OFF
    {
        get;
        set;
    }

	abstract public void InitSkill();
	abstract public void UpdateSkill();
}
