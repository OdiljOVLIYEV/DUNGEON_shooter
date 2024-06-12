using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Box : MonoBehaviour,IDamageable
{
	//public GameObject game;
	public float healt=50f;
	
	
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		
		
		
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		
	}
	
	public void TakeDamage (float amount){
		
		healt-=amount;
		if(healt<=0f){
			
			
			
			
			Die();
		}
		
	}
	
	private void Die(){
		
	
	
		
			
		Destroy(gameObject);
			
		
	}
		
		
	
			
		
	
}
