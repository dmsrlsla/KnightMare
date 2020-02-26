using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tools : MonoSingleton<UI_Tools> {

	GameObject SubRoot = null;
	Dictionary<eUIType, GameObject> DicUI =
		new Dictionary<eUIType, GameObject>();

	Dictionary<eUIType, GameObject> DicSubUI =
		new Dictionary<eUIType, GameObject>();

	Camera UI_Camera = null;
	Camera UI_CAMERA
	{
		get
		{
			if (UI_Camera == null)
			{
				UI_Camera =
					NGUITools.FindCameraForLayer(LayerMask.NameToLayer("UI"));
			}
			return UI_Camera;
		}
	}

	GameObject GetUI(eUIType _uiType, bool isDontDestroy)
	{
		if (isDontDestroy == false)
		{
			if (DicUI.ContainsKey(_uiType) == true)
			{
				return DicUI[_uiType];
			}
		}
		else
		{
			if (DicSubUI.ContainsKey(_uiType) == true)
			{
				return DicSubUI[_uiType];
			}
		}

		GameObject makeUI = null;
		GameObject prefabUI =
			Resources.Load("Prefabs/UI/" + _uiType.ToString()) as GameObject;

		if (prefabUI != null)
		{
			if (isDontDestroy == false)
			{
				makeUI = NGUITools.AddChild(UI_CAMERA.gameObject, prefabUI);
				DicUI.Add(_uiType, makeUI);
			}
			else
			{
				if(SubRoot == null)
				{
					GameObject root = Resources.Load("Prefabs/UI/SubUI_Root") as GameObject;

					SubRoot = NGUITools.AddChild(this.gameObject, root);
					SubRoot.layer = LayerMask.NameToLayer("UI");
				}
				makeUI = NGUITools.AddChild(SubRoot, prefabUI);
				DicSubUI.Add(_uiType, makeUI);
			}

			makeUI.SetActive(false);
		}
		return makeUI;
	}

	public GameObject ShowUI(eUIType _uiType, bool isSub = false)
	{
		GameObject showObject = GetUI(_uiType, isSub);
		if (showObject != null && showObject.activeSelf == false)
		{
			showObject.SetActive(true);
		}

		return showObject;
	}

	public void HideUI(eUIType _uiType, bool isSub = false)
	{
		GameObject showObject = GetUI(_uiType, isSub);
		if (showObject != null && showObject.activeSelf)
		{
			showObject.SetActive(false);
		}
	}

	public void ShowLoadingUI(float value)
	{
		GameObject loadingUI = GetUI(eUIType.PF_UI_LOADING, true);
		if (loadingUI == null)
			return;

		if (loadingUI.activeSelf == false)
			loadingUI.SetActive(true);

		UI_LoadingBar loading = loadingUI.GetComponent<UI_LoadingBar>();

		if (loading == null)
			return;

		loading.SetValue(value);
	}

	public void Clear()
	{
		foreach(KeyValuePair<eUIType, GameObject> pair in DicUI)
		{
			Destroy(pair.Value);
		}

		DicUI.Clear();
	}
}
