using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
	
	
    // Start is called before the first frame update
    void Start()
    {
	 
	   
    }

    // Update is called once per frame
    void Update()
    {
	    
    }
	// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
	
	// OnTriggerEnter is called when the Collider other enters the trigger.
	protected void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag=="Player"){
			
			SceneManager.LoadScene("SampleScene");
		}
		
		
	}
}
