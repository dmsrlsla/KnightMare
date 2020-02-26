using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoSingleton<BloodManager> {

	GameObject bloodParticle;
	GameObject bloodDecal;

	GameObject DecalParent;

	private void Awake()
	{

		//decal 하이어키 상에서의 정리용.
		DecalParent = GameObject.Find("Decal");


		bloodParticle = Resources.Load("Prefabs/Blood") as GameObject;
		bloodDecal = Resources.Load("Prefabs/BloodTrace") as GameObject;
	}
	
	
	public void CreateBloodParticle(GameObject go, Vector3 damagedPos)
	{
		GameObject tempBloodParticle = GameObject.Instantiate(bloodParticle);
		//tempBloodParticle.transform.parent = go.transform;
		//tempBloodParticle.transform.localPosition = damagedPos;
		tempBloodParticle.transform.localPosition = new Vector3(damagedPos.x, damagedPos.y+1f, damagedPos.z);
	}

	public void CreateBloodParticle(GameObject go, Vector3 damagedPos, Quaternion attackDir)
	{
		GameObject tempBloodParticle = GameObject.Instantiate(bloodParticle);
		//tempBloodParticle.transform.parent = go.transform;
		//tempBloodParticle.transform.localPosition = damagedPos;
		//tempBloodParticle.transform.localPosition = new Vector3(0, 1f, 0);
		tempBloodParticle.transform.localPosition = new Vector3(damagedPos.x, damagedPos.y + 1f, damagedPos.z);
		tempBloodParticle.transform.localRotation=attackDir;
	}

	public void CreateBloodParticle(GameObject go, Vector3 damagedPos, Vector3 rotataion, int min,int max,int cycle, float interval )
	{
		GameObject tempBloodParticle = GameObject.Instantiate(bloodParticle);
		//tempBloodParticle.transform.parent = go.transform;
		//tempBloodParticle.transform.localPosition = new Vector3(0, 1f, 0);

		tempBloodParticle.transform.localPosition = new Vector3(damagedPos.x, damagedPos.y + 1f, damagedPos.z);

		
		tempBloodParticle.transform.localRotation=Quaternion.Euler(rotataion);
		tempBloodParticle.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, (short)min, (short)max, cycle, interval) });
	}

	public void CreateBloodDecal(Transform trans,Vector3 pos)
	{
		GameObject tempBloodDecal = GameObject.Instantiate(bloodDecal);
		tempBloodDecal.transform.parent = DecalParent.transform;
		tempBloodDecal.transform.localPosition = new Vector3(pos.x,0.1f, pos.z);

		Vector3 tempRot=trans.localRotation.eulerAngles; //Quaternion을 vector3로 변환
														 //Quaternion.FromToRotation(trans.localRotation);


		//if (tempRot.x < 0)
		//{
		//	tempRot.x = 0;
		//	tempRot.y += Random.Range(0, 181);
		//}

		tempRot.x = 0;
		tempRot.y += 180;
		tempBloodDecal.transform.localRotation = Quaternion.Euler(tempRot);
	}
}
