using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Transform player;
    public float attackDamage = 10f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    { 
        
      
      
        if (other.gameObject.CompareTag("Player"))
        {
          
           PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Player attacked!");
                Destroy(gameObject);
            }
            
            
        }
    }
}