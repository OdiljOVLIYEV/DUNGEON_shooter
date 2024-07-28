using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Target : MonoBehaviour,IDamageable
{
	[SerializeField] private BoolVariable stopAgentchase;
	public ParticleSystem bloodParticle;

	public ParticleType particleType;
	
	public GameObject money;
	public float healt=50f;

	public Animator anim;
	public bool dead;
	[SerializeField] private FloatVariable KillEnemy_UI;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		stopAgentchase.Value = true;
		
		
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
			anim.SetBool("Died",true);
			stopAgentchase.Value = false;
			GetComponent<NavMeshAgent>().speed=0f;
			
			
			
			//anim.SetBool("dead",true);
			 Collider[] colliders = GetComponents<Collider>();
                    foreach (Collider collider in colliders)
                    {
                        collider.enabled = false;
                    }
			
			//	if(anim.agent.speed!=null)
			//anim.agent.speed=0f;
			//Debug.Log("DEAD");
			
			
			
			Die();
		}
		
	}
	
	private void Die()
	{



		KillEnemy_UI.Value--;
		Instantiate(money, transform.localPosition, Quaternion.identity);	
		Destroy(gameObject,1f);
			
		
	}
		
		
	
			
		
	
}
