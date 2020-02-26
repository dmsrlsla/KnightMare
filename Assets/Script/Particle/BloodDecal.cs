using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDecal : MonoBehaviour {

	float CurTime=0f;

	[SerializeField]
	public float lifeTime = 0f;

	private void Start()
	{
		float randScale = Random.Range(0.03f,0.08f);
		transform.localScale = new Vector3(randScale, randScale, randScale);
	}



	private void Update()
	{
		CurTime += Time.deltaTime;

		if(CurTime> lifeTime)
		{
			transform.localScale = new Vector3 (transform.localScale.x*0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);
			//Destroy(gameObject);
		}

		if (transform.localScale.x<0.01)
		{
			Destroy(gameObject);
		}
	}


}
