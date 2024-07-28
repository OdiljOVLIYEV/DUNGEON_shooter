using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using TMPro; 
public class GunShop : MonoBehaviour
{
    public  TextMeshProUGUI GunMoneyText;
    public IntVariable moneyCount;
    public int Weapon_Price;
    public GameObject Lock;
    public GameObject Unlock;

    private void Start()
    {
        
        Lock.SetActive(false);
        Unlock.SetActive(false);
    }

    private void Update()
    {
        GunMoneyText.text =Weapon_Price.ToString()+" $";
    }

   

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (moneyCount.Value == Weapon_Price)
                {
                    moneyCount.Value -= Weapon_Price;
                    Lock.SetActive(false);
                    Unlock.SetActive(true);
                    StartCoroutine(timeWave());
                    Destroy(gameObject,1f);
                }
                else
                {
                    
                    Lock.SetActive(true);
                    Unlock.SetActive(false);
                    
                }
               
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(timeWave());
    }

    IEnumerator timeWave()
    {

        

            
            yield return new WaitForSeconds(1f);
            Lock.SetActive(false);
            Unlock.SetActive(false);
        

    }
}
