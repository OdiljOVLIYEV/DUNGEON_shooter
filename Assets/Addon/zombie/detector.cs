using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class detector : MonoBehaviour
{
	
	
	
	
	
	public int fieldOfView = 45;
	public int viewDistance = 30;
	
	private Transform playerTransform;
	public bool playerDetected ;
	
	private bool isAware=false;
	private NavMeshAgent agent; 
	[HideInInspector]
	public float time=1f;
	
    // Start is called before the first frame update
    void Start()
	{
		
	    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	   
	    agent= GetComponent<NavMeshAgent>();
	  
    }

    // Update is called once per frame
	private void Update()
	{
		
		if(agent.velocity.magnitude<10f){
			
			Animator anim=GetComponent<Animator>();
			anim.SetBool("move",false);
			
		}
		
		
		if(isAware==true){
			animationmove();
			agent.SetDestination(playerTransform.transform.position);
			
			
		}else{
			
			DetectPlayer();
		}
    	
    	
	    
    }
    
	

	private void DetectPlayer()
	{
		RaycastHit hitInfo;
		Vector3 rayDirection = playerTransform.position - transform.position;
		if (Vector3.Angle(rayDirection, transform.forward) < fieldOfView)
		{
			if (Physics.Raycast(transform.position, rayDirection, out hitInfo, viewDistance))
			{
				if(hitInfo.transform.tag=="Player"){
					
					OnAware();
					
					//Debug.Log("PLAYER");
					
				}
				
				
			}
		}
		
	}

	private void OnDrawGizmos()
	{
		if (!Application.isEditor && playerTransform == null)
		{
			return;
		}

		if(playerDetected==true)
		{
			Debug.DrawLine(transform.position, playerTransform.position, Color.green);
		}

		Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);
		Vector3 leftRayPoint = Quaternion.Euler(0f, -fieldOfView, 0f) * frontRayPoint;
		Vector3 rightRayPoint = Quaternion.Euler(0f, fieldOfView, 0f) * frontRayPoint;
        
		Debug.DrawLine(transform.position, frontRayPoint, Color.blue);
		Debug.DrawLine(transform.position, leftRayPoint, Color.blue);
		Debug.DrawLine(transform.position, rightRayPoint, Color.blue);
	}
	
	public void OnAware(){
		
		
		playerDetected = true;
		isAware=true;
	}
	
	public void animationmove(){
		
		Animator anim=GetComponent<Animator>();
		anim.SetBool("move",true);
		
	} 
	
	
	
	
}
