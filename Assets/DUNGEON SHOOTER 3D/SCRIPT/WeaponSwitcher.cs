using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons; // Qurollarning ro'yxati
    private int currentWeaponIndex = -1; // Joriy qurol indeksi

    void Start()
    {
        InitializeWeapons();
    }

    void Update()
    {
        // Foydalanuvchining klaviaturada 1 dan 9 gacha bosilgan har bir kalitini tekshirish
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SetActiveWeapon(i);
            }
        }
    }

    public void SetActiveWeapon(int index)
    {
        if (index < 0 || index >= weapons.Count || index == currentWeaponIndex) 
            return; // Agar indeks noto'g'ri yoki allaqachon faol bo'lsa, chiqish
        
        if (currentWeaponIndex != -1) 
            weapons[currentWeaponIndex].SetActive(false); // Joriy qurolni o'chirish
        
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].SetActive(true); // Yangi qurolni yoqish
    }
    
    private void InitializeWeapons()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        
        if (weapons.Count > 0)
        {
            SetActiveWeapon(0); // O'yin boshlanganda birinchi qurolni faollashtirish
        }
    }
}