using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArea : MonoBehaviour
{
    // 0번 : 콤보게이지를 깎는 타입
    // 1번 : 체력이 깎이는 타입
    public int TrapType = 0;
    //Actor PlayerActor = null;
    ComboSkill ComboSystem = null; 
    private void Awake()
    {
        //Actor PlayerActor = ActorManager.Instance.PlayerLoad();
        ComboSystem = GameManager.Instance.PLAYER_ACTOR.GetComponent<Player>().ComboSkillSet;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (TrapType)
        {
            case 0:
            {
                if (other.tag == "Player")
                {
                    if (!other.gameObject.GetComponent<Player>().IS_INVUL)
                    {
                        Vector3 moveDir = other.transform.position - gameObject.transform.position;
                        other.gameObject.GetComponent<Player>().Stun();
                        ComboSystem.COMBO_GAGE -= 0.2f;
                        other.transform.localPosition = Vector3.zero;
                        Destroy(this.gameObject);
                    }
                }

            }
                break;
            case 1:
                if (other.tag == "Player")
                {
                    Vector3 moveDir = other.transform.position - gameObject.transform.position;
                    other.gameObject.GetComponent<Player>().Stun();
                    if (!other.gameObject.GetComponent<Player>().IS_INVUL)
                    {
                        //other.GetComponent<Actor>().SELF_CHARACTER.IncreaseCurrentHP(-500.0);
                        GameManager.Instance.PLAYER_ACTOR.SELF_CHARACTER.IncreaseCurrentHP(-20.0);
                        BaseBoard board = BoardManager.Instance.GetBoardData(other.GetComponent<Actor>(),
                            eBoardType.BOARD_HP);
                        if (board != null)
                            board.SetData(ConstValue.SetData_HP,
                                other.GetComponent<Actor>().GetStatusData(eStatusData.MAX_HP),
                                other.GetComponent<Actor>().SELF_CHARACTER.CURRENT_HP);

                        //재인이형
                        //// Board 초기화
                        board = null;

                        if (other.transform.localRotation != null)
                            BloodManager.Instance.CreateBloodParticle(gameObject, other.transform.position, other.transform.localRotation);
                        else
                            BloodManager.Instance.CreateBloodParticle(gameObject, other.transform.position);
                    }

                }
                break;
            default:
                break;

        }
        //((Player)(GameManager.Instance.PlayerActor)).sturn();
    }
}
