using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
	public GameObject qurol1;
	
	public bool twogun;
	private int currentGunIndex = 0;
	
	
	public float damage= 10f;
	public float range =100f;

	public Camera cam;
	public Animator anim;
	
	
	public ParticleSystem guns;
	public AudioSource sound;
	public GameObject Blood;
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		//twogun=false;
	}
	//public GameObject bulletPrefab;
	// Update is called every frame, if the MonoBehaviour is enabled.
	private void Update()
	{
		
		PlayerMovment mov=FindObjectOfType<PlayerMovment>();
		
		if(mov.x<0||mov.x>0||mov.z>-0||mov.z<0){
			anim.SetBool("walk",true);
			
		}else if(mov.x>-1||mov.x<1||mov.z>-1||mov.z<1){
			
			anim.SetBool("walk",false);	
			
			
		}
	
		/*if(Input.GetKeyDown(KeyCode.R)){
			
			anim.SetBool("reload",true);
			
			StartCoroutine(pistol());
		}*/
		Weapon_AMMO gun=FindObjectOfType<Weapon_AMMO>();
		
		if(gun.gun_ammo>0){
		if(Input.GetButtonDown("Fire1"))
		{
			if(twogun==false){
			
				shoot();
				anim.SetBool("shoot",true);
				guns.Play();
				sound.Play();
				
			}else{
				
				ShootGun();
			}
		  }
		}
		
		if (Input.GetKey("left shift"))
		{
			
			anim.SetBool("walk",false);	
				

			
			
		} 
	}
	
	private void shoot(){
		
		Weapon_AMMO gun=FindObjectOfType<Weapon_AMMO>();
		gun.gun_ammo--;
			
		
		StartCoroutine(pistol());
		
		
		
		RaycastHit hit;
		if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,range)){
			

			if(hit.transform.tag=="Enemy"){
				Instantiate(Blood,hit.point,Quaternion.FromToRotation(Vector3.right,hit.normal));
				Target target	= hit.transform.GetComponent<Target>();
				if(target!=null)
				target.TakeDamage(damage);
				if(target!=null){
					
				
				
				}
				
				
			}
			
		 		
		  }
		}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(cam.transform.position,cam.transform.forward * range);
		
	}		


	private void ShootGun()
	{
		
		shoot();
		anim.SetBool("shoot",true);
		Debug.Log("1 qurol");
		guns.Play();
		sound.Play();
	
		
		
	}

	
	
	IEnumerator pistol(){
		
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("reload",false);
		
		anim.SetBool("shoot",false);
		
	}
}
