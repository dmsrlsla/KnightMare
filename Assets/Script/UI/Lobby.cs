using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
	UIButton NewGameBtn = null;
	UIButton ContinueBtn = null;
	UIButton QuitBtn = null;

	private void Awake()
	{
		NewGameBtn = GameObject.Find("NewGameButton").GetComponent<UIButton>();
		ContinueBtn = GameObject.Find("ContinueButton").GetComponent<UIButton>();
		QuitBtn = GameObject.Find("QuitButton").GetComponent<UIButton>();

		//뉴 게임 버튼. 말 그대로 새 게임 시작. 이전 저장파일들은 초기화(?)
		EventDelegate.Add(NewGameBtn.onClick, 
			new EventDelegate(this, "ShowGame"));

		//이어하기 버튼. 이전 저장파일을 불러온다. // 차후 구현
		EventDelegate.Add(ContinueBtn.onClick,
			new EventDelegate(this, "ShowGame"));

		//종료 버튼. 어플리케이션을 종료한다. // 07.07 12:35 건희수정 (타이틀로 굳이 가서 종료하는것 보단 여기서 종료하는게 좋아보임)
		EventDelegate.Add(QuitBtn.onClick, () =>
		{
			Application.Quit();
		}
		);
	}

	void ShowGame()
	{
		Scene_Manager.Instance.LoadScene(eSceneType.SCENE_GAME);
	}
}
