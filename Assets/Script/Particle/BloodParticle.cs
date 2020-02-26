using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour {

	public ParticleSystem part;
	public List<ParticleCollisionEvent> collisionEvents;
	private void Start()
	{
		part = GetComponent<ParticleSystem>();
		collisionEvents = new List<ParticleCollisionEvent>();
	}



	private void OnParticleCollision(GameObject other)
	{
		int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

		//Debug.Log("!");
		//BloodManager.Instance.CreateBloodDecal(gameObject.transform);

		//Rigidbody rb = other.GetComponent<Rigidbody>();
		int i = 0;

		while (i < numCollisionEvents)
		{
			
				Vector3 pos = collisionEvents[i].intersection;


				BloodManager.Instance.CreateBloodDecal(gameObject.transform,pos);
				//Vector3 force = collisionEvents[i].velocity * 10;
				//rb.AddForce(force);
			
			i+=5; //조절
		}
	}

}

