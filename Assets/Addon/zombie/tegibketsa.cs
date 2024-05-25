using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tegibketsa : MonoBehaviour
{   
	
    
	public bool attack;
	private Transform playertransform;
	public float  tezlik;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		
		
		playertransform=GameObject.FindGameObjectWithTag("Player").transform;
		
	}
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.

    // Start is called before the first frame update
	// OnTriggerEnter is called when the Collider other enters the trigger.
	
	// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
	private void OnCollisionStay(Collision col)
	{
		
		
		if(col.gameObject.tag=="Player"){
			
			//transform.LookAt(playertransform);
			detector dec= GetComponent<detector>();
			dec.OnAware();
			Debug.Log("tegdi");
				attack=true;
			zombieattack at=GetComponent<zombieattack>();
			at.hujum=true;
			
			Animator anim=GetComponent<Animator>();
			anim.SetBool("attack",true);
			Invoke("enablmesh",1f);
			
			if(attack==true){
				NavMeshAgent agent=GetComponent<NavMeshAgent>();
				anim.applyRootMotion=false;
				agent.speed=0f;
				
			}
		
			
			
			
			
		}
		
	}
	// OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.
	protected void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag=="Player"){
			 
			zombieattack at=GetComponent<zombieattack>();
			at.hujum=false;
			at.meshdisabled();
			
			Invoke("attacks",1f);
			
			
		}
	}
	
	void attacks(){
		  
		    attack=false;
			Animator anim=GetComponent<Animator>();
		anim.SetBool("attack",false);
		anim.applyRootMotion=true;
			NavMeshAgent agent=GetComponent<NavMeshAgent>();
		agent.speed=tezlik;
		
	}
	void enablmesh(){
		
		
		zombieattack at=GetComponent<zombieattack>();
		at.meshenabled();
		
	}
}