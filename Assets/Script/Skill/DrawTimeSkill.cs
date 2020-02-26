using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬 스텝


public class DrawTimeSkill : MonoBehaviour {

    private LineRenderer line;
    private GameObject game_camera;
    public GameObject Trail;
    public Vector3[] positions;
    public int position_num = 0;
    public static int POSITION_NUM_MAX = 1000;

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.NONE;

    private bool IsDrawSkill = false;
    public float TimeCheck = 0;



    public bool IS_DRAW_SKILL
    {
        get { return IsDrawSkill; }
        set { IsDrawSkill = value; }
    }

    void Start()

    {
        this.game_camera = GameObject.FindGameObjectWithTag("MainCamera");
        //line = this.GetComponent<LineRenderer>();
        //line.SetVertexCount(0);

        Trail = gameObject.transform.parent.gameObject;
        this.positions = new Vector3[POSITION_NUM_MAX];

        IsDrawSkill = false;
        TimeCheck = 0;
    }

    void Update()
    {
        switch (this.step)
        {

            case STEP.NONE:
                {
                    this.next_step = STEP.IDLE;
                }
                break;

            case STEP.IDLE:
                {
                    if (Input.GetMouseButton(0))
                    {

                        this.next_step = STEP.DRAWING;
                        IS_DRAW_SKILL = true;
                    }
                }
                break;

            case STEP.DRAWING:
                {
                    if (!Input.GetMouseButton(0))
                    {

                        if (this.position_num >= 2)
                        {

                            this.next_step = STEP.DRAWED;

                        }
                        else
                        {

                            this.next_step = STEP.IDLE;
                        }
                    }
                }
                break;
        }

        // 상태가 변하면 초기화.

        if (this.next_step != STEP.NONE)
        {

            switch (this.next_step)
            {

                case STEP.IDLE:
                    {
                        // 이전에 작성한 것을 삭제한다.

                        this.position_num = 0;

                        //line.SetVertexCount(0);
                    }
                    break;
            }

            this.step = this.next_step;

            this.next_step = STEP.NONE;
        }
        switch (this.step)
        {

            case STEP.DRAWING:
                {
                    //TimeCheck += Time.deltaTime;
                    //if (TimeCheck > 0.5f)
                    //{
                        DrawLines();

                    //   TimeCheck = 0;
                    //}
                    //DrawReal();
                }
                break;
        }
    }


    private Vector3 unproject_mouse_position()
    {
        Vector3 mouse_position = Input.mousePosition;

        // 중심을 지나는 수평한 면（법선이 Y축. XZ평면）
        Plane plane = new Plane(Vector3.up, new Vector3(0.0f, 0.0f, 0.0f));

        // 마우스 커서와 카메라 위치를 지나는 직선.
        Ray ray = this.game_camera.GetComponent<Camera>().ScreenPointToRay(mouse_position);

        // 이 두 조건이 교차하는 지점을 구한다. .

        float depth;

        plane.Raycast(ray, out depth);

        Vector3 world_position;

        world_position = ray.origin + ray.direction * depth;

        return (world_position);
    }
#if true
    private void DrawLines()
    {
        Vector3 Clickposition = this.unproject_mouse_position();

        bool is_append_position = false;

        if (this.position_num == 0)
        {

            // 처음 한 개는 무조건 추가.

            is_append_position = true;

        }
        // 최대 개수를 초과한 경우에는 추가할 수 없다.
        else if (this.position_num >= POSITION_NUM_MAX)
        {
            is_append_position = false;
        }
        else
        {
            if (Vector3.Distance(this.positions[this.position_num - 1], Clickposition) > 0.5f)
            {
                is_append_position = true;
            }
        }

        if (is_append_position)
        {
            if (this.position_num > 0)
            {
                Vector3 distance = Clickposition - this.positions[this.position_num - 1];

                distance *= 0.5f / distance.magnitude;

                Clickposition = this.positions[this.position_num - 1] + distance;
            }

            this.positions[this.position_num] = Clickposition;

            this.position_num++;

            //line.SetVertexCount(this.position_num);

            for (int i = 0; i < this.position_num; i++)
            {
                Trail.transform.localPosition = Clickposition;
                //line.SetPosition(i, this.positions[i]);
                //Debug.Log(this.positions[i]);
            }

        }
    }
#else
    IEnumerator CoDrawLines()
    {
        while(IS_DRAW_SKILL)
        {
            Vector3 Clickposition = this.unproject_mouse_position();

            bool is_append_position = false;

            if (this.position_num == 0)
            {

                // 처음 한 개는 무조건 추가.

                is_append_position = true;

            }
            // 최대 개수를 초과한 경우에는 추가할 수 없다.
            else if (this.position_num >= POSITION_NUM_MAX)
            {
                is_append_position = false;
            }
            else
            {
                if (Vector3.Distance(this.positions[this.position_num - 1], Clickposition) > 0.5f)
                {
                    is_append_position = true;
                }
            }

            if (is_append_position)
            {
                if (this.position_num > 0)
                {
                    Vector3 distance = Clickposition - this.positions[this.position_num - 1];

                    distance *= 0.5f / distance.magnitude;

                    Clickposition = this.positions[this.position_num - 1] + distance;
                }

                this.positions[this.position_num] = Clickposition;

                this.position_num++;


            }

            yield return new WaitForSeconds(0.2f);
        }
 
    }
#endif
    public void DrawReal()
    {
        line.SetVertexCount(this.position_num);

        for (int i = 0; i < this.position_num; i++)
        {
            line.SetPosition(i, this.positions[i]);
            //Debug.Log(this.positions[i]);
        }

    }
}
