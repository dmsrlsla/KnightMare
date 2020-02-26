using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ToBoss : BaseObject {

    UIButton BossRoomBtn = null;
    UIButton ContinueBtn = null;

    public void Init()
    {
        Transform trans = FindInChild("BossRoomButton");
        if (trans == null)
        {
            Debug.LogError("BossRoomButton is not found");
            return;
        }
        BossRoomBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(BossRoomBtn.onClick,
            new EventDelegate(this, "BossRoomGo"));


        trans = FindInChild("ContinueButton");
        if (trans == null)
        {
            Debug.LogError("ContinueButton is not found");
            return;
        }
        ContinueBtn = trans.GetComponent<UIButton>();
        EventDelegate.Add(ContinueBtn.onClick,
            new EventDelegate(this, "Continue"));
    }

    void BossRoomGo()
    {
        UI_Tools.Instance.HideUI(eUIType.PF_UI_TOBOSS);
        GameManager.Instance.SelectStage = 21;
        GameManager.Instance.ResetMap();
    }

    void Continue()
    {
        UI_Tools.Instance.HideUI(eUIType.PF_UI_TOBOSS);
        GameObject go = UI_Tools.Instance.ShowUI(eUIType.PF_UI_STAGESELECT);
        UI_StageSelect stageSelect = go.GetComponent<UI_StageSelect>();
        stageSelect.Init();
    }
}
