using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Box : MonoBehaviour,IDamageable
{
	public GameObject bulletHolePrefab;
	public ParticleType particleType;
	//public GameObject game;
	public float healt=50f;
	public float offset = 0.01f;
	
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		
		
		
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		
	}
	
	public void TakeDamage (float amount,Vector3 hitPoint,Vector3 hitNormal){
		
		healt-=amount;
		Vector3 spawnPosition = hitPoint + hitNormal * offset;
		switch (particleType)
		{
			
			case ParticleType.BulletHole:
				// O'q izini devorga joylashtirish
				GameObject bulletHole = Instantiate(bulletHolePrefab, spawnPosition, Quaternion.LookRotation(hitNormal));
				bulletHole.transform.SetParent(this.transform); // Izni devor ob'ektiga biriktirish
				break;
		
	   }
		
		if(healt<=0f){
			
			
			
			
			Die();
		}
		
	}
	
	private void Die(){
		
	
	
		
			
		Destroy(gameObject);
			
		
	}
		
		
	
			
		
	
}
