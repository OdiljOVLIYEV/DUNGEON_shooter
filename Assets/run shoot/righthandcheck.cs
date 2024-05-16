using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class righthandcheck : MonoBehaviour
{
	public bool ong;
	
	protected void Start()
	{
		ong=false;
	}
	
	protected void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag=="wall"){
			ong=true;
			
			Debug.Log("o`ng taraf");
		}
	}
	
	protected void OnTriggerExit(Collider other)
	{
		ong=false;
	}
}
