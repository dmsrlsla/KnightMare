using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoSingleton<ParticleManager>
{
	
	GameObject tempSpeedLineParticle;
	public GameObject SPEED_LINE_P_INS{
		get
		{
			return tempSpeedLineParticle;
		}
	}

	GameObject DustParticle;
	GameObject SpeedLineParticle;
	GameObject FlashParticle;

	private void Awake()
	{
		SpeedLineParticle = Resources.Load("Prefabs/Effects/SpeedLine") as GameObject;
		FlashParticle = Resources.Load("Prefabs/Effects/Flash") as GameObject;
		DustParticle = Resources.Load("Prefabs/Effects/Dust") as GameObject;
	}

	public void CreateSpeedLineParticle(Vector3 pos, Quaternion rotataion)
	{
		tempSpeedLineParticle = GameObject.Instantiate(SpeedLineParticle);
	
		tempSpeedLineParticle.transform.localPosition = new Vector3(pos.x, pos.y + 1f, pos.z);
		tempSpeedLineParticle.transform.localRotation = rotataion;
	}

	public void CreateDustParticle(Vector3 pos)
	{
		GameObject tempDustParticle = GameObject.Instantiate(DustParticle);
		tempDustParticle.transform.localPosition = pos;
	}

	//public void CreateFlashParticle(Vector3 pos, Quaternion rotataion)
	//{
	//	GameObject tempFlashParticle = GameObject.Instantiate(FlashParticle);

	//	tempFlashParticle.transform.localPosition = new Vector3(pos.x, pos.y + 1f, pos.z);

	//	//Vector3 rot =  rotataion.eulerAngles;
	//	//rot = new Vector3(rot.x, rot.y);
	//	tempFlashParticle.transform.localRotation = rotataion;
	//}

}
