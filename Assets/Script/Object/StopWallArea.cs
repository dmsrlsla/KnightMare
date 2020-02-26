using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWallArea : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")

        {
            float backDis = 2f;
            Vector3 moveDir = other.transform.position - gameObject.transform.position;
            moveDir = moveDir.normalized;

            other.transform.position = new Vector3(other.transform.position.x + moveDir.x * backDis
                , other.transform.position.y
                , other.transform.position.z + moveDir.z * backDis);

            other.gameObject.GetComponent<Player>().Stun();
            //Vector3 moveDir = gameObject.transform.position - other.transform.position;
            //other.gameObject.GetComponent<Player>().ReturnPosition(moveDir);

            //other.gameObject.GetComponent<Rigidbody>().AddForce(moveDir * 10f);
        }
        //((Player)(GameManager.Instance.PlayerActor)).sturn();
    }
}
