using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggEnemy : BaseAI
{
    public Actor EnemyActor;
    bool IsDestroySelf;
    float CheckDestroyTime;
    public float EggDestroyTime = 0.0f;


    private void Start()
    {
        IsDestroySelf = false;
        CheckDestroyTime = EggDestroyTime;
        Invoke("EggDestroyMyself", 5.0f);
    }

    void EggDestroyMyself()
    {
        IsDestroySelf = true;
        processDie();
    }

    protected override void processDie()
    {
        if (IsDestroySelf)
        {
            if (BoardManager.Instance != null)
                BoardManager.Instance.ClearBoard(this);
            EnemyActor = ActorManager.Instance.MiniBossLoad(transform.localPosition);
        }
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log("Egg Break!");
        Destroy(gameObject);
        base.processDie();
    }
}
