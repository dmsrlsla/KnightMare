using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StageSelect : BaseObject {

    UIButton NextRoom = null;
    UIButton BossRoom = null;

	public void Init()
    {
        Transform trans = FindInChild("ToNextStageButton");
        if (trans == null)
        {
            Debug.LogError("ToNextStageButton is not found");
            return;
        }
        NextRoom = trans.GetComponent<UIButton>();
        EventDelegate.Add(NextRoom.onClick,
            new EventDelegate(this, "NextRoomGo"));


        trans = FindInChild("ToBossRoomButton");
        if (trans == null)
        {
            Debug.LogError("ToBossRoomButton is not found");
            return;
        }
        BossRoom = trans.GetComponent<UIButton>();
        EventDelegate.Add(BossRoom.onClick,
            new EventDelegate(this, "BossRoomGo"));
    }

    void NextRoomGo()
    {
        UI_Tools.Instance.HideUI(eUIType.PF_UI_STAGESELECT);
        GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_STAGE);
        UI_Stage stage = go.GetComponent<UI_Stage>();
        stage.Init();
    }

    void BossRoomGo()
    {
        UI_Tools.Instance.HideUI(eUIType.PF_UI_STAGESELECT);
        GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_TOBOSS);
        UI_ToBoss toBoss = go.GetComponent<UI_ToBoss>();
        toBoss.Init();
    }
}
