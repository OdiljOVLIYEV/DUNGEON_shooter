using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class GunScript : MonoBehaviour
{
	public float damage= 10f;
	public float range =100f;
	public float shootRadius = 20f;
	[SerializeField] private IntVariable ammo_UI;
	[SerializeField] private IntVariable gun_ammo_add;
	[SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
	
	public Camera cam;
	public Animator anim;
	
	
	
	//public ParticleSystem guns;
	public AudioSource sound;
	public LayerMask enemyLayer;

	

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
	
		
	}
	public void Use()
    {
        Debug.Log("Gun is used");
        // Gun'ni ishlatish logikasi shu yerda.
    }
	//public GameObject bulletPrefab;
	// Update is called every frame, if the MonoBehaviour is enabled.
	private void Update()
	{  
		UI_AMMO_UPDATE.Raise(gun_ammo_add.Value);
		ammo_UI.Value = gun_ammo_add.Value;
	
		
		
		PlayerMovment mov=FindObjectOfType<PlayerMovment>();

		if (anim.GetBool("shoot") == false) // Shoot animatsiyasi ishlamayotganda harakat animatsiyalarini boshqarish
		{
			if (mov.x < 0 || mov.x > 0 || mov.z > 0 || mov.z < 0)
			{
				anim.SetBool("walk", true);
			}
			else
			{
				anim.SetBool("walk", false);
			}

			if (Input.GetKey("left shift"))
			{
				anim.SetBool("Run", true);
			}
			else
			{
				anim.SetBool("Run", false);
			}
		}
		/*if(Input.GetKeyDown(KeyCode.R)){
			
			anim.SetBool("reload",true);
			
			StartCoroutine(pistol());
		}*/
		
		if(gun_ammo_add.Value>0){
			if(Input.GetButtonDown("Fire1"))
			{
			
				
				shoot();
			
			}
		
		}
	
		
		
		if (Input.GetKey("left shift"))
		{
			
				
				
			anim.SetBool("Run",true);
			
			
			
		}
		else
		{
			anim.SetBool("Run",false);
		
			
		}

		
			

	}
	
	
	private void shoot(){
		
	
		anim.SetBool("shoot",true);
		
		sound.Play();
		StartCoroutine(gunanim());
		gun_ammo_add.Value--;
		UI_AMMO_UPDATE.Raise(gun_ammo_add.Value);
		
		
		
		
		
		RaycastHit hit;
		int playerLayer = LayerMask.NameToLayer("Player");
		int layerMask = ~(1 << playerLayer);
		if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,range, layerMask)){
			

		
				
				IDamageable damageable=hit.transform.GetComponent<IDamageable>();
				if (damageable != null)
				{
					//Instantiate(Blood,hit.point,Quaternion.FromToRotation(Vector3.right,hit.normal));
					damageable.TakeDamage(damage,hit.point,hit.normal);
					Debug.Log(hit.transform.gameObject.name);
				}
				// O'q otish ovozini chiqarish
				Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootRadius, enemyLayer);
				foreach (Collider hitCollider in hitColliders)
				{
					AIController aiController = hitCollider.GetComponent<AIController>();
					if (aiController != null)
					{
						aiController.HeardSound(transform.position);
					}
				}
				
				
			
			
		 		
		  }
		}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(cam.transform.position,cam.transform.forward * range);
		
	}

	IEnumerator gunanim()
	{
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("shoot",false);
	}
	

	
	
	
}

