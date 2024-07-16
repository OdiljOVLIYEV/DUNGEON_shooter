using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100f;

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0f)
		{
			Die();
		}
	}

	void Die()
	{
		Debug.Log("Player Died");
		// Playerning o'lish logikasini shu yerda amalga oshiring
	}
}
