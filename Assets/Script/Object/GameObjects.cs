using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjects {

	public BaseObject TargetComponenet = null;

	ObjectTemplateData TemplateData = null;

	CharacterStatusData ObjectStatus = new CharacterStatusData();

	public ObjectTemplateData OBJECT_TEMPLATE
	{ get { return TemplateData; } }
	public CharacterStatusData OBJECT_STATUS
	{ get { return ObjectStatus; } }

	double CurrentHP = 0;
	public double CURRENT_HP
	{ get { return CurrentHP; } }


	public void IncreaseCurrentHP(double valueData)
	{
		CurrentHP += valueData;
		if (CurrentHP < 0)
			CurrentHP = 0;

		double maxHP =
			ObjectStatus.GetStatusData(eStatusData.MAX_HP);
		if (CurrentHP > maxHP)
			CurrentHP = maxHP;

		if (CurrentHP == 0)
			TargetComponenet.OBJECT_STATE =
				eBaseObjectState.STATE_DIE;

		Debug.Log(CurrentHP);
	}

	public void SetTemplate(ObjectTemplateData _templateData)
	{
		TemplateData = _templateData;
		ObjectStatus.AddStatusData(
			ConstValue.CharacterStatusDataKey,
			TemplateData.STATUS);
		CurrentHP =
			ObjectStatus.GetStatusData(eStatusData.MAX_HP);
	}

}
