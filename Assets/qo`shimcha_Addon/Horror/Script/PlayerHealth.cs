using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public TextMeshProUGUI text;
	public float health = 100f;
	private void Start()
	{
		text.text=health.ToString();
		GetComponent<Animator>().enabled = false;
		
	}

	public void TakeDamage(float amount)
	{	
		health -= amount;
		text.text=health.ToString();
	
		if (health <= 0f)
		{
			Die(); 
			
			
		}
	}

	void Die()
	{
		GetComponent<Animator>().enabled = true;
		Debug.Log("Player Died");
		// Playerning o'lish logikasini shu yerda amalga oshiring
	}
	
	
	
}
