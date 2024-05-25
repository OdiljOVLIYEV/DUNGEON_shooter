using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lefthandcheck : MonoBehaviour
{
	public bool chap;
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		chap=false;
	}
	// OnTriggerEnter is called when the Collider other enters the trigger.
	protected void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag=="wall"){
			
			chap=true;
			Debug.Log("chap taraf");
		}
	}
	
	// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	protected void OnTriggerExit(Collider other)
	{
		chap=false;
	}
}
