using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealt : MonoBehaviour
{
	public float Healt=10f;
	public  TMP_Text text;
	public GameObject HEALTFULL;
	
    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
	{
		if(Healt<0){
		
			Healt=0f;
			
		}
		text.text=Healt.ToString();


		

	}
    
    
    
    
	public void damage(float amount){
		Healt-=amount;
		if(Healt<=0f){
			
			
			Debug.Log("dead");
		}
	}

    
	public void alarmOn()
	{
		HEALTFULL.SetActive(true);
		
	}
    public void alarmOff()
    {
       
        HEALTFULL.SetActive(false);

    }
}
