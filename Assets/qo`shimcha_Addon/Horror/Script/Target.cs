using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour,IDamageable
{
	public ParticleSystem bloodParticle;
	

	public ParticleType particleType;
	public GameObject BloodSprite;
	//public GameObject game;
	public float healt=50f;
	private NavMeshAgent navMeshAgent;
	[SerializeField] private BoolVariable stopAgent;

	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		
		navMeshAgent = GetComponent<NavMeshAgent>();
		
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		
	}
	
	public void TakeDamage (float amount,Vector3 hitPoint,Vector3 hitNormal){
		
		healt-=amount;
		ParticleSystem particleToSpawn = null;
		
		switch (particleType)
		{
			case ParticleType.Blood:
				particleToSpawn = bloodParticle;

				break;
			
		}

		if (particleToSpawn != null)
		{
			Instantiate(particleToSpawn, hitPoint, Quaternion.identity);
		}
		
		if(healt<=0f)
		{
			
			Animator anim=GetComponent<Animator>();
			anim.SetBool("Walk", false);
			anim.enabled=false;
			
			if (navMeshAgent.isOnNavMesh)
			{
				stopAgent.Value = true;
				navMeshAgent.enabled = false;
			}

			GetComponent<Collider>().enabled = false;
		
			//agent.speed=0f;
			//agent.enabled = false;
			
			
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
