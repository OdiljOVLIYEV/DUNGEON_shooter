using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons; // Qurollarning ro'yxati
    //public int maxUnlockedWeaponIndex; // Maksimal ochiq qurol indeksi (boshlang'ich qiymati 0, ya'ni faqat 1 qurol ochiq)
    [SerializeField] private IntVariable UnlockedWeapon;
     
    private int currentWeaponIndex = -1; // Joriy qurol indeksi

    void Start()
    {
        InitializeWeapons();
    }

    void Update()
    {
       // maxUnlockedWeaponIndex = UnlockedWeapon.Value;
      
        // Foydalanuvchining klaviaturada 1 dan 9 gacha bosilgan har bir kalitini tekshirish
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()) && i <= UnlockedWeapon.Value)
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