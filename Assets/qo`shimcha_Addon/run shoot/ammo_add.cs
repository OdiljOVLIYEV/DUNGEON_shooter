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
	[SerializeField] private IntVariable plasma_ammo_add;
	[SerializeField] private IntVariable rocket_launcher_ammo_add;
	[SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
	//[SerializeField] private IntVariable rifle_ammo_add;
	public int gun_ammo;
	public int shotgun_ammo;
	public int rifle_ammo;
	public int plasma_ammo;
	public int rocket_launcher_ammo;
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

	    if (other.gameObject.tag == "Player" && !isCollected)
	    {
		    bool ammoCollected = false;

		    if (gun_ammo_add.Value < 80 && gun_ammo > 0)
		    {
			    isCollected = true; 
			    white.enabled = true;
			    Invoke("whiteoff", 0.2f);
			    gun_ammo_add.Value = Mathf.Clamp(gun_ammo_add.Value + gun_ammo, 0, 80);
			    UI_AMMO_UPDATE.Raise(gun_ammo_add.Value);
			    ammoCollected = true;
		    }

		    if (shotgun_ammo_add.Value < 40 && shotgun_ammo > 0 && !ammoCollected)
		    {
			    isCollected = true; 
			    white.enabled = true;
			    Invoke("whiteoff", 0.2f);
			    shotgun_ammo_add.Value = Mathf.Clamp(shotgun_ammo_add.Value + shotgun_ammo, 0, 40);
			    UI_AMMO_UPDATE.Raise(shotgun_ammo_add.Value);
			    ammoCollected = true;
		    }

		    if (rifle_ammo_add.Value<200 && rifle_ammo > 0 && !ammoCollected)
		    {
			    isCollected = true; 
			    white.enabled = true;
			    Invoke("whiteoff", 0.2f);
			    rifle_ammo_add.Value = Mathf.Clamp(rifle_ammo_add.Value += rifle_ammo, 0, 200);
			    UI_AMMO_UPDATE.Raise(rifle_ammo_add.Value);
			    ammoCollected = true;
		    }
		    
		    if (plasma_ammo_add.Value<100 && plasma_ammo > 0 && !ammoCollected)
		    {
			    isCollected = true; 
			    white.enabled = true;
			    Invoke("whiteoff", 0.2f);
			    plasma_ammo_add.Value = Mathf.Clamp(plasma_ammo_add.Value += plasma_ammo, 0, 200);
			    UI_AMMO_UPDATE.Raise(plasma_ammo_add.Value);
			    ammoCollected = true;
		    }
		    
		    if (rocket_launcher_ammo_add.Value<20 && rocket_launcher_ammo > 0 && !ammoCollected)
		    {
			    isCollected = true; 
			    white.enabled = true;
			    Invoke("whiteoff", 0.2f);
			    rocket_launcher_ammo_add.Value = Mathf.Clamp(rocket_launcher_ammo_add.Value += rocket_launcher_ammo, 0, 200);
			    UI_AMMO_UPDATE.Raise(rocket_launcher_ammo_add.Value);
			    ammoCollected = true;
		    }
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
