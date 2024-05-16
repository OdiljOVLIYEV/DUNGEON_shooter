using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
	//public GameObject game;
	public float healt=50f;
	
	public bool dead;
	
	
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
			Animator anim=GetComponent<Animator>();
			anim.enabled=false;
			//anim.SetBool("dead",true);
			CapsuleCollider capsuleCollider=GetComponent<CapsuleCollider>(); 
			capsuleCollider.enabled=false;
			NavMeshAgent agent=GetComponent<NavMeshAgent>();
			agent.speed=0f;
			//	if(anim.agent.speed!=null)
			//anim.agent.speed=0f;
			//Debug.Log("DEAD");
			
			
			
			Die();
		}
		
	}
	
	private void Die(){
		
	
	
		
			
		Destroy(gameObject,50f);
			
		
	}
		
		
	
			
		
	
}
