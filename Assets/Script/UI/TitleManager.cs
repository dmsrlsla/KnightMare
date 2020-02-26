using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

	private void Start()
	{
		UI_Tools.Instance.ShowUI(eUIType.PF_UI_TITLE);
	}
}
