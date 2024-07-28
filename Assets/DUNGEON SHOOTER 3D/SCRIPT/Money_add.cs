using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class Money_add : MonoBehaviour
{
    
    public IntVariable moneyCount;
    // Start is called before the first frame update
   /* private void OnTriggerEnter(Collider other)
    {
        
    }*/

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moneyCount.Value ++;
            Destroy(gameObject);
        }
    }
}
