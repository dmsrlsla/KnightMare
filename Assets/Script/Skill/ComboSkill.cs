using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSkill : MonoBehaviour
{
    public GameObject TempDrawObject = null;
    public DrawTimeSkill DrawSkill = null;
    public float SkillTimeCheck = 0.0f;
    private bool IsStartSkill = false;
    public bool IS_START_SKILL
    {
        get { return IsStartSkill; }
        set { IsStartSkill = value; }
    }
    //*******은기 추가구문 06_28***********//
    float ComboGage = 0.0f;
    public float COMBO_GAGE
    {
        get { return ComboGage; }
        set { ComboGage = value; }
    }
    bool PowerOn = false;
    public bool POWER_ON
    {
        get { return PowerOn; }
        set { PowerOn = value; }
    }
    float DownGage = 0.0003f;
    float DownGageCheckTime = 0.3f;
    float TempCheckTime = 0;
    bool PowerDownState = false;
    public bool POWER_DOWN_STATE
    {
        get { return PowerDownState; }
        set { PowerDownState = value; }
    }
    public DrawTimeSkill DRAW_SKILL
    {
        get { return DrawSkill; }
        //셋은 필요없음.
    }
    //***********************************//

    // 그림만큼 이동하는 시간.
    public float DrawTimeScale;

    public void Start()
    {
        //*******은기 추가구문 06_29***********//
        //TempDrawObject = Instantiate(Resources.Load("Prefabs/Combo/SS")) as GameObject;
        DrawSkill = this.transform.Find("DrawTimeSkill").GetComponent<DrawTimeSkill>();
        //TempDrawObject.transform.parent = this.transform; 
        //*******은기 추가구문 06_23***********//
        //DrawSkill = GameObject.FindGameObjectWithTag("Skill").GetComponent<DrawTimeSkill>();
        //DrawSkill.gameObject.SetActive(false);
        //SkillTimeCheck = 0.0f;
        //IsStartSkill = false;
        //*******은기 추가구문 06_28***********//
        ComboGage = 0.0f;
        PowerOn = false;
        PowerDownState = false;

        //************************************//
    }

    public void SKillCheck()
    {
        //*******은기 추가구문 06_23***********//
        if (DrawSkill.IS_DRAW_SKILL && SkillTimeCheck < 2f)
        {
            DrawSkill.gameObject.SetActive(true);

            SkillTimeCheck += Time.deltaTime;
        }
        else if (SkillTimeCheck >= 2f)
        {
            IsStartSkill = true;
            DrawSkill.IS_DRAW_SKILL = false;
            DrawSkill.gameObject.SetActive(false);
            SkillTimeCheck += Time.deltaTime;
            if (DrawSkill.step == STEP.DRAWED)
            {
                DrawSkill.next_step = STEP.IDLE;
            }
        }
        //*******은기 추가구문 06_23***********//

        if (ComboGage < 1.0f)
        {
            TempCheckTime += Time.deltaTime;
            if (TempCheckTime > DownGageCheckTime
                && PowerDownState == false)
            {
                PowerDownState = true;
                TempCheckTime = 0;
            }

            if (PowerDownState == true)
            {
               // Debug.Log("파워다운....! : " + ComboGage);
                if (TempCheckTime > (DownGageCheckTime / 3))
                    if (ComboGage >= 0)
                        ComboGage -= DownGage;
            }
        }
        else if (ComboGage >= 1.0f)
        {
         //   Debug.Log("콤보어택 On! : " + ComboGage);
            PowerOn = true;
            PowerDownState = false;
        }
    }
}
