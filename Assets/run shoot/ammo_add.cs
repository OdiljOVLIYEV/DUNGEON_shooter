using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_add : MonoBehaviour
{
	
	public int gun_ammo_add;
	public int shotgun_ammo_add;
	public int rifle_ammo_add;
	
    // Start is called before the first frame update
    void Start()
	{
		
        
    }

    // Update is called once per frame
   
    
	// OnTriggerEnter is called when the Collider other enters the trigger.
	protected void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Player"){
			
			Weapon_AMMO gun=FindObjectOfType<Weapon_AMMO>();
			gun.gun_ammo+=gun_ammo_add;
			gun.shotgun_ammo+=shotgun_ammo_add;
			gun.rifle_ammo+=rifle_ammo_add;
			
			Destroy(gameObject);
		}
	}
}
