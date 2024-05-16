using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoGunScript : MonoBehaviour
{
	public GameObject qurol1;
	public GameObject qurol2;
	public bool twogun;
	private int currentGunIndex = 0;
	
	
	public float damage= 10f;
	public float range =100f;

	public Camera cam;
	public Animator anim;
	public Animator anim2;
	
	public ParticleSystem guns;
	public AudioSource sound;
	public ParticleSystem gun2;
	public AudioSource sound2;
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
			anim2.SetBool("walk",true);
		}else if(mov.x>-1||mov.x<1||mov.z>-1||mov.z<1){
			
			anim.SetBool("walk",false);	
			anim2.SetBool("walk",false);	
			
		}
	
		if(Input.GetKeyDown(KeyCode.R)){
			
			anim.SetBool("reload",true);
			anim2.SetBool("reload",true);
			StartCoroutine(pistol());
		}
		Weapon_AMMO gun=FindObjectOfType<Weapon_AMMO>();
		
		if(gun.gun_ammo>0){
		if(Input.GetButtonDown("Fire1"))
		{
			if(twogun==false){
			
				shoot();
				anim.SetBool("shoot",true);
				guns.Play();
				sound.Play();
				sound2.Play();
			}else{
				
				ShootGun();
			}
		  }
		}
		
		if (Input.GetKey("left shift"))
		{
			
			anim.SetBool("walk",false);	
			anim2.SetBool("walk",false);	

			
			
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
		
		
		
		GameObject currentGun = GetCurrentGun();
		if (currentGun != null)
		{
			// Bu yerda tortishish mantig'ingizni amalga oshiring
			Debug.Log("O'q otish quroli: " + currentGun.name);

			// Misol uchun, siz o'q ob'ektini qurolning joylashuvi va aylanishidan tasvirlashingiz mumkin
			//Instantiate(bulletPrefab, currentGun.transform.position, currentGun.transform.rotation);
            
			// Navbatdagi otishma uchun joriy qurol indeksini oshiring
			currentGunIndex = (currentGunIndex + 1) % 2;
		}
	}

	private GameObject GetCurrentGun()
	{
		if (currentGunIndex == 0)
		{
			shoot();
			anim.SetBool("shoot",true);
			Debug.Log("1 qurol");
			guns.Play();
			sound.Play();
			return qurol1;
		}
		else if(currentGunIndex == 1)
		{ 
			shoot();
			anim2.SetBool("shoot",true);
			Debug.Log("2 qurol");
			gun2.Play();
			sound2.Play();
			return qurol2;
		}

		return null;
	}
	
	IEnumerator pistol(){
		
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("reload",false);
		anim2.SetBool("reload",false);
		anim.SetBool("shoot",false);
		anim2.SetBool("shoot",false);
	}
}
