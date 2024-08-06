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
    [SerializeField] private FloatVariable HealthPlayer;

    public Image image; // UI Image komponenti
    public float fadeDuration = 1.0f;
    
    private string unlimitedSymbol = "∞";// Fade davomiyligi (soniyalarda)
    private void Start()
    {
	   
	    
        GetComponent<Animator>().enabled = false;
        text.text=HealthPlayer.Value.ToString();
    }

    private void Update()
    {
	    float largeNumber = 1e+30f;
	    if (HealthPlayer.Value==largeNumber)
	    {
		   
		    text.text = unlimitedSymbol;

	    }
	    else
	    {
		    
	    }
    }

    private void OnEnable()
    {
	    HealtPacks.PlayerHealth += PlayerHealt_UI;
    }

    private void OnDisable()
    {
	    HealtPacks.PlayerHealth -= PlayerHealt_UI;
    }

    private void PlayerHealt_UI()
    {
	    
	    text.text=HealthPlayer.Value.ToString();
	    
    }
    public void TakeDamage(float amount)
    {
	    image.enabled = true;
	    StartCoroutine(FadeOut());
	    HealthPlayer.Value -= amount;
	    text.text=HealthPlayer.Value.ToString();
        
        if (HealthPlayer.Value <= 0f)
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