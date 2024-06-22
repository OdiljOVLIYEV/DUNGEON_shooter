using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropen : MonoBehaviour
{
	
	
	public AudioSource opens;
	public AudioSource close;
	public Animator anim;
	public bool birinchi_eshik;
	public GameObject bx1;
	public GameObject bx2;
	private bool opendoor;
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		Animator anim=FindObjectOfType<Animator>();
		
	}
	
    // Start is called before the first frame update
	// OnTriggerEnter is called when the Collider other enters the trigger.
	protected void OnTriggerStay(Collider other)
	{
		
		
		if(other.gameObject.tag=="Player"){
			
			//bx1.SetActive(true);
			//bx2.SetActive(false);
			if(Input.GetKeyDown(KeyCode.E)){
			
				if(opendoor==false){
					
					opendoor=true;
					anim.SetBool("open",true);
					
			
				opens.Play();
				}else{
					
					opendoor=false;
					close.Play();
					anim.SetBool("open",false);
				}
			
				
			}
			
				
		}
			
			
		
	}
	
	// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
	
	
	// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/*protected void OnTriggerExit(Collider other)
	{
		
		if(other.gameObject.tag=="Player"){
			
			
		
			
		
			
			Invoke("closedoor",2f);
				
			
		
			//}
			
		}
		
	}
	
	void closedoor(){
		
		bx1.SetActive(true);
		bx2.SetActive(true);
		 close.Play();
		anim.SetBool("open",false);
		anim.SetBool("open2",false);
		birinchi_eshik=false;
	}*/
}
