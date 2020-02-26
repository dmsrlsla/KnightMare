using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamObject : BaseObject {

	[SerializeField]
	eTeamType TeamType;
	public eTeamType TEAM_TYPE
	{
		get
		{
			return TeamType;
		}

		set
		{
			TeamType = value;
		}
	}
}
