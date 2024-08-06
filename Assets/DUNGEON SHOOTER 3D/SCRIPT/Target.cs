using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Target : MonoBehaviour,IDamageable
{
	
	public ParticleSystem bloodParticle;

	public ParticleType particleType;
	
	public GameObject money;
	public float healt=50f;

	public Animator anim;
	public bool dead;
	[SerializeField] private FloatVariable KillEnemy_UI;

	public AudioSource killsound;
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{

		killsound.GetComponent<AudioSource>();

	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		
	}
	
	public void TakeDamage (float amount,Vector3 hitPoint,Vector3 hitNormal){
		
		anim.SetBool("Impact", true);
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
		Invoke("imapctOff", 0.1f);
		
		if(healt<=0f)
		{    
			
		
			
			GetComponent<AIController>().canChase = false;
			GetComponent<NavMeshAgent>().speed=0f;
			
			anim.SetBool("Died",true);
			
			
			//anim.SetBool("dead",true);
			 Collider[] colliders = GetComponents<Collider>();
                    foreach (Collider collider in colliders)
                    {
                        collider.enabled = false;
                    }
			
			//	if(anim.agent.speed!=null)
			//anim.agent.speed=0f;
			//Debug.Log("DEAD");
			
			killsound.Play();
			
			Invoke("Die",0.1f);
		}
		
	}
	
	private void Die()
	{



		KillEnemy_UI.Value--;
		Instantiate(money, transform.localPosition, Quaternion.identity);	
		Destroy(gameObject,1.3f);
			
		
	}
		
	public void imapctOff()
	{
		anim.SetBool("Impact", false);
		
	}
	
			
		
	
}
