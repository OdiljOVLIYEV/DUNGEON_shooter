using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons; // Qurollarning ro'yxati
    [SerializeField] private IntVariable UnlockedWeapon;
     
    private int currentWeaponIndex = -1; // Joriy qurol indeksi
    private int previousWeaponIndex = -1; // Oldingi qurol indeksi
    private int lastWeaponIndex = -1; // Oxirgi tanlangan qurol indeksi

 
    void Start()
    {
        InitializeWeapons();
    }

    void Update()
    {
        // Foydalanuvchining klaviaturada 1 dan 9 gacha bosilgan har bir kalitini tekshirish
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()) && i <= UnlockedWeapon.Value)
            {
                SetActiveWeapon(i);
              
            }
        }

        // Q tugmasi bosilganda oxirgi tanlangan 2 ta qurolni almashish
        if (Input.GetKeyDown(KeyCode.Q) && previousWeaponIndex != -1)
        {
            SetActiveWeapon(previousWeaponIndex);
        }
    }

    public void SetActiveWeapon(int index)
    {
        
        
        if (index < 0 || index >= weapons.Count || index == currentWeaponIndex) 
            return; // Agar indeks noto'g'ri yoki allaqachon faol bo'lsa, chiqish
        
        if (currentWeaponIndex != -1) 
            weapons[currentWeaponIndex].SetActive(false); // Joriy qurolni o'chirish
        
        previousWeaponIndex = currentWeaponIndex; // Joriy qurolni oldingi qurolga o'rnatish
        currentWeaponIndex = index; // Yangi qurolni joriy qurolga o'rnatish
        weapons[currentWeaponIndex].SetActive(true); // Yangi qurolni yoqish
        
    }
    
    private void InitializeWeapons()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        // O'yin boshlanganda birinchi ochiq qurolni topish va faollashtirish
        SetActiveWeapon(0); // Dastlab faqat birinchi qurol ochiq bo'ladi
    }

    // Qurolni ochish
    public void UnlockWeaponsUpTo(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            UnlockedWeapon.Value = index;
        }
    }
    
    public void CheckAndSetActiveWeapon(int index)
    {
        if (index <= UnlockedWeapon.Value)
        {
            SetActiveWeapon(index);
        }
    }
}
