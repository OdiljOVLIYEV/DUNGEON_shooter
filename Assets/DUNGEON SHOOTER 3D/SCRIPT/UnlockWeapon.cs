using System;
using Obvious.Soap;
using Obvious.Soap.Example;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour
{
    [SerializeField] private IntVariable UnlockedWeapon;
    [SerializeField] private int unlockWeapon;
   

    // Update is called once per frame
    void Update()
    {
        
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("unlock");
            UnlockedWeapon.Value += unlockWeapon;
            Destroy(gameObject);
            
        }
    }
}
