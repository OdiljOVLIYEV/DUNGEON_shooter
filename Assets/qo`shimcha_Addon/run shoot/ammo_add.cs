using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ammo_add : MonoBehaviour
{
	[SerializeField] private IntVariable gun_ammo_add;
	[SerializeField] private IntVariable shotgun_ammo_add;
	[SerializeField] private IntVariable rifle_ammo_add;
	[SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
	//[SerializeField] private IntVariable rifle_ammo_add;
	public int gun_ammo;
	public int shotgun_ammo;
	public int rifle_ammo;
	public Image white; 
	public LayerMask groundLayer;
	private bool isCollected = false;
	
	
    // Start is called before the first frame update
 

    // Update is called once per frame
    private void Update()
    {
	    if (IsGrounded())
	    {
		    GetComponent<Rigidbody>().useGravity = false;
		    Invoke("triggerOn",0.3f);
		 
	    }
	    else
	    {
		  GetComponent<Rigidbody>().useGravity = true;
		  Invoke("triggeroff",0.3f);
	    }

	   
    }

    private void OnTriggerEnter(Collider other)
    
    {

	    if(other.gameObject.tag=="Player"&& !isCollected)
	    {
		    isCollected = true; 
		    white.enabled = true;
		    Invoke("whiteoff",0.2f);
		    gun_ammo_add.Value += gun_ammo;// + shotgun_ammo_add + rifle_ammo_add;
		    shotgun_ammo_add.Value += shotgun_ammo;
		    rifle_ammo_add.Value += rifle_ammo;
		    
			   
		  
		    
		    
		   
		    UI_AMMO_UPDATE.Raise(gun_ammo_add.Value);
		    UI_AMMO_UPDATE.Raise(shotgun_ammo_add.Value);
		    UI_AMMO_UPDATE.Raise(rifle_ammo_add.Value);
		 
	    }



    }

    // OnTriggerEnter is called when the Collider other enters the trigger.
	/*private void OnCollisionEnter(Collision other)
	{
		
	}*/
	bool IsGrounded()
	{
		// Trigger boxning pozitsiyasi
		Vector3 position = transform.position;
		

		// CheckSphere natijasi
		bool grounded = Physics.CheckSphere(position, 0.5f, groundLayer);
		
	

		return grounded;
		
	}
	
	public void whiteoff()
	{
		white.enabled = false;
		Destroy(gameObject);
	}

	public void triggerOn()
	{
		GetComponent<BoxCollider>().isTrigger = true;
		
		
	}
	
	public void triggeroff()
	{
		GetComponent<BoxCollider>().isTrigger = false;
		
		
	}
}
