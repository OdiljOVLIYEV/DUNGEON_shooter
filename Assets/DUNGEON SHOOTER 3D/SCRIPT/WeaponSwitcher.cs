using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons; // Qurollarning ro'yxati
    public int maxUnlockedWeaponIndex = 2; // Maksimal ochiq qurol indeksi (boshlang'ich qiymati 2, ya'ni 1, 2, va 3 qurollar ochiq)

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
            if (Input.GetKeyDown((i + 1).ToString()) && i <= maxUnlockedWeaponIndex)
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

        // O'yin boshlanganda birinchi ochiq qurolni topish va faollashtirish
        for (int i = 0; i <= maxUnlockedWeaponIndex && i < weapons.Count; i++)
        {
            if (i == 0 || !weapons[i].activeSelf) // Agar birinchi qurol yoki hali ochiq bo'lmagan qurol bo'lsa
            {
                SetActiveWeapon(i);
                break;
            }
        }
    }

    // Qurolni ochish
    public void UnlockWeaponsUpTo(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            maxUnlockedWeaponIndex = index;
        }
    }
}