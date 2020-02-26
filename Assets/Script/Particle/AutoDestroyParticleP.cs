using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticleP : MonoBehaviour {

	ParticleSystem particle;

	// Use this for initialization
	void Start()
	{
		particle = GetComponentInChildren<ParticleSystem>();
	}

	// Update is called once per frame
	void Update()
	{
		if (particle.isPlaying == false) Destroy(gameObject);
	}
}

