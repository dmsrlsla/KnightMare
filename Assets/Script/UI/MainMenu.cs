using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	UIButton ContinueBtn;
	UIButton LobbyBtn;
	GameObject ThisObject;

	void Start ()
	{
		ContinueBtn = GameObject.Find("ContinueButton").GetComponent<UIButton>();
		LobbyBtn = GameObject.Find("LobbyButton").GetComponent<UIButton>();
		ThisObject = this.gameObject;	

		//컨티뉴 버튼 : 그냥 메뉴 접힘.
		EventDelegate.Add(ContinueBtn.onClick, () =>	
		{
			//버튼 기능 삽입.
			//print("ContinueButton is OK.");
			NGUITools.SetActive(ThisObject, false);
			Time.timeScale = 1f;
		}
		);

		//세이브 앤 로비 : 저장과 동시에 로비 화면으로 전환.
		EventDelegate.Add(LobbyBtn.onClick, () =>	
		{
			//버튼 기능 삽입.
			//print("Save and LobbyButton is OK.");
			//SceneManager.LoadScene("Lobby");
		}
		);
	}
	

	// Update is called once per frame
	void Update ()
	{
		
	}
}
