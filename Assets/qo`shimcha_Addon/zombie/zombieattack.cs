using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieattack : MonoBehaviour
{
	public BoxCollider bx;
	public float  damage=10f;
	public bool hujum;
	private Transform playertransform;
    // Start is called before the first frame update
    void Start()
    {
	    playertransform=GameObject.FindGameObjectWithTag("Player").transform;
	    bx= FindObjectOfType<BoxCollider>();
	    hujum=false;
    }
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		/*if(Input.GetKey(KeyCode.F)){
			
			bx.enabled=true;
			
		}*/
	}
    // Update is called once per frame
  
    
	// OnTriggerEnter is called when the Collider other enters the trigger.
	protected void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Player"){
			Debug.Log("hujum");
			
			
			if(hujum==true){
				//transform.LookAt(playertransform);
				PlayerHealt healt=FindObjectOfType<PlayerHealt>();
			healt.damage(damage);
			
			}
			
			
		}
	}
	// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	protected void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag=="Player"){
			hujum=false;
			
			//meshdisabled();
		}
	}
	
	
	public void meshenabled(){
		
		bx.enabled=true;
	}
	
	public void meshdisabled(){
		
		bx.enabled=false;
	}
}
