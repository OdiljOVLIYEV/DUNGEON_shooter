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
    
    public Image image; // UI Image komponenti
    public float fadeDuration = 1.0f; // Fade davomiyligi (soniyalarda)
    private void Start()
    {
        text.text=health.ToString();
        GetComponent<Animator>().enabled = false;
		
    }

    public void TakeDamage(float amount)
    {
	    image.enabled = true;
	    StartCoroutine(FadeOut());
        health -= amount;
        text.text=health.ToString();
        
        if (health <= 0f)
        {
	        
            Die(); 
			
			
        }
    }

    void Die()
    {
	    image.enabled = false;
        GetComponent<Animator>().enabled = true;
        Debug.Log("Player Died");
        // Playerning o'lish logikasini shu yerda amalga oshiring
    }
	
    public IEnumerator FadeOut()
    {
	    float elapsedTime = 0f;
	    Color color = image.color;

	    while (elapsedTime < fadeDuration)
	    {
		    elapsedTime += Time.deltaTime;
		    color.a = Mathf.Clamp01(1 - elapsedTime / fadeDuration);
		    image.color = color;
		    yield return null;
	    }
	    
    }
	
}