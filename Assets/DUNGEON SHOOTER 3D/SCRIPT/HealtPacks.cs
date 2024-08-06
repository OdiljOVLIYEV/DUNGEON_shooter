using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

public class HealtPacks : MonoBehaviour
{

    [SerializeField] private FloatVariable HealthPlayer;
    public float HealthPackBig;
    public float HealthPackSmall;
    private bool isCollected = false;
    public static Action PlayerHealth;
    public LayerMask groundLayer;
    public Image white; 
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            GetComponent<Rigidbody>().useGravity = false;
            Invoke("triggerOn",0.3f);
		 
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = true;
            Invoke("triggeroff",0.3f);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    
    {

        if(other.gameObject.tag=="Player"&& !isCollected)
        {
            
           
            if (HealthPlayer.Value < 100f)
            {   isCollected = true; 
                white.enabled = true;
                Invoke("whiteoff",0.2f);
                HealthPlayer.Value = Mathf.Clamp(HealthPlayer.Value + HealthPackBig, 0f, 100f);
                HealthPlayer.Value = Mathf.Clamp(HealthPlayer.Value + HealthPackSmall, 0f, 100f);
                
                PlayerHealth?.Invoke();
            }
		    
            
          
        }



    }
    
    bool IsGrounded()
    {
        // Trigger boxning pozitsiyasi
        Vector3 position = transform.position;
		

        // CheckSphere natijasi
        bool grounded = Physics.CheckSphere(position, 0.5f, groundLayer);
		
	

        return grounded;
		
    }
    
    public void whiteoff()
    {
        white.enabled = false;
        Destroy(gameObject);
    }

    public void triggerOn()
    {
        GetComponent<BoxCollider>().isTrigger = true;
		
		
    }
	
    public void triggeroff()
    {
        GetComponent<BoxCollider>().isTrigger = false;
		
		
    }
}
