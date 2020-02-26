using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoSingleton<Scene_Manager>
{


	bool IsAsyc = true;
	AsyncOperation Operation = null;

	eSceneType CurrentState = eSceneType.SCENE_TITLE;
	eSceneType NextState = eSceneType.SCENE_NONE;

	float StackTime = 0.0f;
	public eSceneType CURRENT_SCENE
	{
		get { return CurrentState; }
	}

	public void LoadScene(eSceneType _type, bool _asyne = true)
	{
		if (CurrentState == _type)
			return;

		NextState = _type;
		IsAsyc = _asyne;
	}

	void Update()
	{

		if (Operation != null)
		{
			StackTime += Time.deltaTime;

			UI_Tools.Instance.ShowLoadingUI(Operation.progress);

			if (Operation.isDone == true)
			{
				CurrentState = NextState;
				ComplateLoad(CurrentState);

				Operation = null;
				NextState = eSceneType.SCENE_NONE;

				// Loding UI 삭제
				UI_Tools.Instance.HideUI(eUIType.PF_UI_LOADING, true);
			}
			else
				return;
		}

		if (CurrentState == eSceneType.SCENE_NONE)
			return;

		if (NextState != eSceneType.SCENE_NONE &&
			CurrentState != NextState)
		{
			DisableScene(CurrentState);

			if (IsAsyc)
			{// 비동기 로드
				Operation = SceneManager.LoadSceneAsync(NextState.ToString("F"));
				StackTime = 0f;

				UI_Tools.Instance.ShowLoadingUI(0.0f);
			}
			else
			{
				SceneManager.LoadScene(NextState.ToString("F"));
				CurrentState = NextState;
				NextState = eSceneType.SCENE_NONE;
				ComplateLoad(CurrentState);
			}
		}
	}

	void ComplateLoad(eSceneType _type)
	{
		switch (_type)
		{
			case eSceneType.SCENE_NONE:
				break;
			case eSceneType.SCENE_TITLE:
				break;
			case eSceneType.SCENE_GAME:
				{
					GameManager.Instance.GameInit();
					GameManager.Instance.LoadGame();
				}
				break;
			case eSceneType.SCENE_LOBBY:
				{
					LobbyManager.Instance.LoadLobby();
				}
				break;
			default:
				break;
		}
	}

	void DisableScene(eSceneType _type)
	{
		// Scene UI 삭제
		UI_Tools.Instance.Clear();

		switch (_type)
		{
			case eSceneType.SCENE_NONE:
				break;
			case eSceneType.SCENE_TITLE:
				break;
			case eSceneType.SCENE_GAME:
                {
                    UI_Tools.Instance.HideUI(eUIType.PF_UI_INGAME);
                    SkillManager.Instance.ClearSkill();
                }
				break;
			case eSceneType.SCENE_LOBBY:
				LobbyManager.Instance.DisableLobby();
				break;
            default:
				break;
		}
	}
}
