using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AKSshoot : MonoBehaviour
{
	public float damage= 10f;
	public float range =100f;
	public float time;
	
	public Camera cam;
	public ParticleSystem gun;
	public AudioSource sound;
	public AudioClip shoot;
	public AudioClip reloaded;
	
	//o'qdori kodi
	//public int Magazinammo=12;
	private magazinaks ammo;
	public int maxammo=6;
	public int bullet;
	public int qoldiq;
	public int raqam;
	public bool tugadi;
    public TMP_Text text;
	
	public float Reloadtime =  2f;
	public bool  isReloading;
	
	public Animator anim;
	private switchweapon change;
	public GameObject Blood;
	public GameObject devor;
	public SphereCollider sphera;
	public bool canshoot;
	private zombie_easy zombie;
	private BACKMENU back;
	private PlayerMovment movement;
	bool run;
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		anim.keepAnimatorStateOnDisable=true;
		ammo=FindObjectOfType<magazinaks>();
		canshoot=true;
		anim=GetComponent<Animator>();
		//reloaded=GetComponent<AudioSource>();
		bullet=maxammo;
		zombie=FindObjectOfType<zombie_easy>();
		sphera=GetComponent<SphereCollider>();
		movement=FindObjectOfType<PlayerMovment>();
		
	}
	// Update is called once per frame
    
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		
		if (Input.GetKey("left shift")&&movement.z==1f){
			Debug.Log(movement.z);
			run=true;
			anim.SetBool("run",true);
		}else{
			anim.SetBool("run",false);
			run=false;
		}
		
		
		if(bullet>0){
			
			
			anim.SetBool("shoot2",false);	
			
			
				
				back=FindObjectOfType<BACKMENU>();
				if(back.menu==false){
	    if(Input.GetButton("Fire1")&&canshoot&&run==false){
	    	
	    	
		    Shoot();
	    	
	    	
	    }			
	    
			}
			
		
		}else{
			
			anim.SetBool("shoot2",false);
		}
		
		
		
	}
    
    
	void Update()
	{
		
		
		/*if(bullet==0&&Input.GetButtonDown("Fire1")&&Input.GetButton("Fire2")&&!isReloading){
			Debug.Log("O'Q TAMOM");
			return;
		}
		*/
		change=FindObjectOfType<switchweapon>();
		
		
		
		if(bullet==0&&Input.GetButtonDown("Fire1")&&!isReloading){
			
			anim.SetBool("aksshoot",false);
			anim.SetBool("aksreload",true);
			//reloaded.Play();
		StartCoroutine(Reload());
			
			}
		
		
		
		
		
		
			
		if(Input.GetKeyDown(KeyCode.R)&&bullet<maxammo&&ammo.Magazinammo>0&&canshoot){
			
			canshoot=false;
			anim.SetBool("aksreload",true);
			Invoke("relaodesound",1f);
			Invoke("reload",2f);
			
			
		
		}
		
		if(Input.GetKeyDown(KeyCode.R)&&bullet==maxammo){
			
			
			anim.SetBool("aksreload",false);
	
			
			
		
		}
		
		text.text=bullet+"/"+ammo.Magazinammo;
		
		if(bullet==0 && ammo.Magazinammo==0){
			
			anim.SetBool("aksreload",false);
			return;
		}
		
		if(isReloading)
			return;
		
		
		
		
		
			
		
		}
	
	void reload(){
		
		if(ammo.Magazinammo<maxammo){
				
				qoldiq=maxammo-bullet;
				ammo.Magazinammo-=qoldiq;
			    raqam=ammo.Magazinammo+bullet;
				
				
				if(raqam<maxammo){
				
					raqam=maxammo+ammo.Magazinammo;
					if(raqam<maxammo){
						bullet+=ammo.Magazinammo;
					}else{
						
						
						Debug.Log("error");
					}
					
				}
				
			if(ammo.Magazinammo<=0){
					
					ammo.Magazinammo=0;
				}
			
				
				bullet=bullet+qoldiq;
				Debug.Log(qoldiq);
					
			}else{
						
				qoldiq=maxammo;
				qoldiq-=bullet;
				ammo.Magazinammo-=qoldiq;
				bullet=maxammo;
				//Debug.Log(Magazinammo);
						
			}
			
		canshoot=true;
		anim.SetBool("aksreload",false);
	}
		
	void relaodesound(){
		
		sound.PlayOneShot(reloaded);
	}
		
		
    
	void Shoot(){
		gun.Play();
		sound.PlayOneShot(shoot);
		anim.SetBool("aksshoot",true);
		canshoot=false;
		bullet--;
		StartCoroutine(shootgun());
		
		RaycastHit hit;
		if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,range)){
			
			
			if(hit.transform.tag=="Enemy"){
				Instantiate(Blood,hit.point,Quaternion.FromToRotation(Vector3.right,hit.normal));
				detector dec= hit.transform.GetComponent<detector>();
				dec.OnAware();
					
				Target target	= hit.transform.GetComponent<Target>();
				if(target!=null){
					target.TakeDamage(damage);
				
				}
				
				Debug.Log("DUSHMAN");
			}
			
				
			if(hit.transform.tag=="devor"){
				Instantiate(devor,hit.point,Quaternion.FromToRotation(Vector3.right,hit.normal));
				Debug.Log("DUSHMAN");
			}
			
			Debug.Log(hit.transform);
			
			
				
				
		}
	}
	
	
	IEnumerator Reload(){
		
		isReloading = true;
		
		
		
		yield return new WaitForSeconds(Reloadtime);
		
		
		
		if(ammo.Magazinammo>=maxammo){
			
			bullet=maxammo;	
			ammo.Magazinammo-=maxammo;
		}
		else{
			bullet=ammo.Magazinammo;
			ammo.Magazinammo=0;
			
		}
		
		isReloading=false;
		anim.SetBool("aksreload",false);
		
	}
	
	IEnumerator shootgun(){
		
		yield return new WaitForSeconds(0.1f);
		canshoot=true;
		anim.SetBool("aksshoot",false);
	}
	
	
	
	
	}
	
	

