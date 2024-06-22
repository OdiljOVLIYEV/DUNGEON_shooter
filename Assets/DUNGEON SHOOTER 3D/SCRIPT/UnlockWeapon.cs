using System;
using Obvious.Soap;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour
{
    [SerializeField] private IntVariable UnlockedWeapon;
    [SerializeField] private int unlockWeaponIndex; // Yangi qurol indeksini ochish
    private WeaponSwitcher weaponSwitcher;

    private void Start()
    {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Unlock weapon at index: " + unlockWeaponIndex);
            UnlockedWeapon.Value = unlockWeaponIndex;
            weaponSwitcher.CheckAndSetActiveWeapon(unlockWeaponIndex); // Qurolni almashtirishni chaqirish
            Destroy(gameObject);
        }
    }
}