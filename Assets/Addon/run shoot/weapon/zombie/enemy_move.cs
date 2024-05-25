using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_move : MonoBehaviour
{  
	public NavMeshAgent agent;
	public Transform oyuncu; // Oyuncu obyektining referansi
	public float takipHizi = 2.0f; 
	// Dushmanni oyuncuni qanday tezda takip qilishini belgilovchi tezlik
	public Transform groundCheck;
	public float groundDistans = 0.4f;
	public LayerMask groundMask;
	Vector3 velocity;
	public bool isGrounded;
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		
	}
	void Update()
	{  
		isGrounded = Physics.CheckSphere(groundCheck.position,groundDistans, groundMask);
		if (isGrounded && velocity.y < 0){
			velocity.y = -2f;
		}
		// Dushmanni oyuncuni takip qilish
		if (oyuncu != null)
		{
			Vector3 oyuncuPozisyonu = oyuncu.position;
			Vector3 yeniPozisyon = Vector3.MoveTowards(transform.position, oyuncuPozisyonu, takipHizi * Time.deltaTime);
			transform.position = yeniPozisyon;
			//velocity.y = -2f;
			
			// Dushmanning oyuncuga qarab qurilishi uchun yuzini qaratish
			transform.LookAt(oyuncu);
		}
	}
	// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
	protected void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="sakrash"){
			
			
			agent.enabled=false;
			
			
		}
	}
	// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	protected void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag=="sakrash"){
			
			
			agent.enabled=true;
			
			
		}
	}
}
