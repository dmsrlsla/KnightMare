using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : InterObject
{
    public float minusDegreeDrag = 4f;
    public Actor EnemyActor;
    bool IsDestroySelf;
    float CheckDestroyTime;
    public float EggDestroyTime = 0.0f;

    private void Start()
    {
        //시간지나서 자기 힘으로 알을 깨고 나왔는지 확인용
        IsDestroySelf = false;
        CheckDestroyTime = EggDestroyTime;
    }

    //지 힘으로 깨고 나왔을경우
    void EggDestroyMyself()
    {
        IsDestroySelf = true;
        Break();
    }

    private new void Update()
    {
        //CheckDestroyTime += Time.deltaTime;
        //if (CheckDestroyTime > 10.0f)
        //{
        //    IsDestroySelf = true;
        //    Break();
        //}
        Invoke("EggDestroyMyself", 5.0f);
    }



    protected override void Break()
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
        //StartCoroutine("EggDestroy");
    }
}
