using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
	public CharacterController controller;
	
	public Transform groundCheck;
	
	public LayerMask groundMask;

	public LayerMask wallrunmask;

	public LayerMask devor;


	public  float speed;
	public float gravity = -9.8f;
	public float groundDistans = 0.4f;
	
	public float jumpHeight = 3f;
	[HideInInspector]
	public float z;
	[HideInInspector]
	public float x;
	
	Vector3 velocity;
	public bool isGrounded;
	public bool wallrun;
	public bool devors;
	public bool crouch;
	public bool crouchtwo;
	public bool sakradi;
    // Start is called before the first frame update
    void Start()
    {
	    crouch=false;
	    
    }

    // Update is called once per frame
    void Update()
	{
		
		/*devors=Physics.CheckSphere(groundCheck.position,groundDistans, devor);
		if(devors&&velocity.y < 0){
			//isGrounded=false;
		}else{
			isGrounded=true;
			
		}*/
		
		//if(wallrun==false){
		isGrounded = Physics.CheckSphere(groundCheck.position,groundDistans, groundMask);
		if (isGrounded && velocity.y < 0){
			velocity.y = -2f;
		}	
		//}/*else{*/
				
		wallrun = Physics.CheckSphere(groundCheck.position,groundDistans, wallrunmask);
			if (wallrun && velocity.y < 0){
				velocity.y = -2f;
				//isGrounded=true;
}
		//}
		      x = Input.GetAxis("Horizontal");
		      z = Input.GetAxis("Vertical");
		      
		

		// Character Controller komponenti orqali yugurtishni amalga oshiramiz
		
		
		
			
		
	
		if (Input.GetButtonDown("Jump") && isGrounded||Input.GetButtonDown("Jump") && devors)
		{
			velocity.y= Mathf.Sqrt(jumpHeight * -4 * gravity);
			wallrun=true;
		}else{
			
			
		}
		
		if (Input.GetKey(KeyCode.LeftControl))
		{
			
			crouchtwo=true;
			controller.height = 1f;
		} else
		{  	crouchtwo=false;
			controller.height = 2f;
		}
		
		if (Input.GetKey("left shift"))
		{
			
			
			
				CameraShake shake=FindObjectOfType<CameraShake>();
			shake.bobbingSpeed=20;
			
				speed = 15;
				
			
			
			
	
		} else
		{
			
				CameraShake shake=FindObjectOfType<CameraShake>();
				shake.bobbingSpeed=10.5f;
			
				speed = 10;
				
			
			
			//isGrounded=true;
			//wallrun=false;
			
			
		    
		
		}
		
		
		
		
	    
		Vector3 move = transform.right * x + transform.forward * z;
		controller.Move(move * speed * Time.deltaTime);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	    
	
	}
	
	
	
	IEnumerator kamayish(){
		
	
		while(speed>1){
			yield return new WaitForSeconds(0.1f);
			speed-=0.05f;
			speed = Mathf.Max(speed, 0f);
			
		}
		
	}
	
	void kamaytirish(){
		
		speed -=1f;
	}
	
	void sakrash(){
		
		sakradi=false;
	}
}
	
