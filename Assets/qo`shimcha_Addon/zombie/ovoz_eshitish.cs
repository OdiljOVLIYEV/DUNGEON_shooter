using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ovoz_eshitish : MonoBehaviour
{
	private Transform playerTransform;
	[HideInInspector]
	public Vector3 transformsound; 
	
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		
		
		
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		
	}
    // Start is called before the first frame update
	// OnTriggerEnter is called when the Collider other enters the trigger.
	private void OnTriggerStay(Collider other)
	{
		
		if(other.gameObject.tag=="tovush"){
			
			Animator anim=gameObject.GetComponent<Animator>();
			
			anim.SetBool("move",true);
			
			transformsound=new Vector3(playerTransform.transform.position.x,playerTransform.transform.position.y,playerTransform.transform.position.z);
			
			NavMeshAgent agent=GetComponent<NavMeshAgent>();
			agent.SetDestination(transformsound);
			
			
			
		}
		
		
		if(other.gameObject.tag=="picsound"){
			Animator anim=gameObject.GetComponent<Animator>();
			if(anim!=null)
			anim.SetBool("move",true);
			
			transformsound=new Vector3(playerTransform.transform.position.x,playerTransform.transform.position.y,playerTransform.transform.position.z);
			
			NavMeshAgent agent=GetComponent<NavMeshAgent>();
			agent.SetDestination(transformsound);
		}
	}
	// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	
	
}
