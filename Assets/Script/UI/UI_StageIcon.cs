using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StageIcon : BaseObject {

    StageInfo Info = null;
    public StageInfo INFO
    {
        get { return Info; }
    }
    UILabel StageName = null;

    public void Init(StageInfo _info)
    {
        Info = _info;
        StageName = this.GetComponentInChildren<UILabel>();
        StageName.text = Info.NAME;
    }

    public void OnClick()
    {
        GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_POPUP);
        UI_Popup popup = go.GetComponent<UI_Popup>();

        popup.Set(
            () =>
            {
                Debug.Log(INFO.NAME + " 입장");
                GameManager.Instance.SelectStage = int.Parse(INFO.KEY);
                // 여기에 맵 지웠다 다시 만드는 함수 실행.
                GameManager.Instance.ResetMap();
                UI_Tools.Instance.HideUI(eUIType.PF_UI_POPUP);
                UI_Tools.Instance.HideUI(eUIType.PF_UI_STAGE);
            },
            () =>
            {
                UI_Tools.Instance.HideUI(eUIType.PF_UI_POPUP);
            },
            "단계 선택",
            INFO.NAME + "에 입장 하시겠습니까?");
    }
}
