using System;
using Obvious.Soap;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour
{
    [SerializeField] private IntVariable UnlockedWeapon;
    private WeaponSwitcher weaponSwitcher;
    [SerializeField] private int unlockweaponnumber;
    //public WeaponSwitcher weaponSwitcher;
    // Start is called before the first frame update
    void Start()
    {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // UnlockedWeapons[2].Value = true;
            // weaponSwitcher.UnlockWeapon(0);
            // Debug.Log("Unlock weapon at index: " + unlockWeaponIndex);
            //UnlockedWeapon.Value = unlockWeaponIndex;
            // weaponSwitcher.CheckAndSetActiveWeapon(unlockWeaponIndex); // Qurolni almashtirishni chaqirish
            UnlockedWeapon.Value = unlockweaponnumber;
            weaponSwitcher.UnlockWeapon(UnlockedWeapon.Value);
            weaponSwitcher.CheckAndSetActiveWeapon(UnlockedWeapon.Value);
            
        }
    }
}