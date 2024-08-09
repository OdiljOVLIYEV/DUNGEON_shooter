using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using System.Collections;
public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons; // Qurollarning ro'yxati
    public List<GameObject> weapons_icon; // Icon ro`yxati
    public GameObject Katana;

    public static Action SwordEffectCall;
   // public GameObject Katana_icon;
    [SerializeField] private List<BoolVariable> UnlockedWeapons;
    [SerializeField] private IntVariable UnlockedWeapon;// List of BoolVariables representing unlocked weapons
    [SerializeField] private BoolVariable canShoot;
    
    private int currentWeaponIndex = -1; // Joriy qurol indeksi
    private int previousWeaponIndex = -1; // Oldingi qurol indeksi
    private int lastWeaponIndex = -1; // Oxirgi tanlangan qurol indeksi
    private bool change = false;
    void Start()
    {
        canShoot.Value = true;
        InitializeWeapons();
        InitializeWeaponIcons();
    }

    void Update()
    {
        // Foydalanuvchining klaviaturada 1 dan 9 gacha bosilgan har bir kalitini tekshirish
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()) && UnlockedWeapons[i].Value)
            {
                SetActiveWeapon(i);
            }
        }

        // Q tugmasi bosilganda oxirgi tanlangan 2 ta qurolni almashish
        if (Input.GetKeyDown(KeyCode.Q) && previousWeaponIndex != -1)
        {
            SetActiveWeapon(previousWeaponIndex);
        }

        if (Input.GetKeyDown(KeyCode.F)&&change)
        {
            Katana.SetActive(true);
            SwordEffectCall?.Invoke();
            if (currentWeaponIndex != -1)
            {
                weapons[currentWeaponIndex].SetActive(false);
                weapons_icon[currentWeaponIndex].SetActive(false);
            }
            canShoot.Value = false; // Disables shooting while 'F' is pressed
        }

        // 'F' tugmasi qo'yib yuborilganda joriy qurolni qayta yoqish
        if (Input.GetKeyUp(KeyCode.F))
        {
           Invoke("WeaponOn",0.3f);
        }
        
    }

    public void SetActiveWeapon(int index)
    {
        canShoot.Value = true;
        if (index < 0 || index >= weapons.Count || index == currentWeaponIndex)
            return; // Agar indeks noto'g'ri yoki allaqachon faol bo'lsa, chiqish

        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].SetActive(false); // Joriy qurolni o'chirish
            weapons_icon[currentWeaponIndex].SetActive(false); // Joriy qurol ikonkasini o'chirish
        }

        previousWeaponIndex = currentWeaponIndex; // Joriy qurolni oldingi qurolga o'rnatish
        currentWeaponIndex = index; // Yangi qurolni joriy qurolga o'rnatish
        weapons[currentWeaponIndex].SetActive(true); // Yangi qurolni yoqish
        weapons_icon[currentWeaponIndex].SetActive(true); // Yangi qurol ikonkasini yoqish
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
            UnlockedWeapons[i].Value = false; // Initialize all weapons as locked
        }

        // O'yin boshlanganda birinchi ochiq qurolni topish va faollashtirish
        SetActiveWeapon(0); // Dastlab faqat birinchi qurol ochiq bo'ladi
        UnlockedWeapons[0].Value = true; // Ensure the first weapon is unlocked
    }

    private void InitializeWeaponIcons()
    {
        foreach (GameObject icon in weapons_icon)
        {
            icon.SetActive(false);
        }

        // O'yin boshlanganda birinchi ochiq qurol ikonkasini topish va faollashtirish
        if (weapons_icon.Count > 0)
        {
            weapons_icon[0].SetActive(true);
        }
    }

    // Qurolni ochish
    public void UnlockWeapon(int UnlockedWeapon)
    {
        if (UnlockedWeapon >= 0 && UnlockedWeapon < weapons.Count)
        {
            UnlockedWeapons[UnlockedWeapon].Value = true;
        }
    }

    public void CheckAndSetActiveWeapon(int UnlockedWeapon)
    {
        if (UnlockedWeapon >= 0 && UnlockedWeapon < weapons.Count && UnlockedWeapons[UnlockedWeapon].Value)
        {
            SetActiveWeapon(UnlockedWeapon);
        }
    }

    void WeaponOn()
    {
        Katana.SetActive(false);
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].SetActive(true);
            weapons_icon[currentWeaponIndex].SetActive(true);
        }
        canShoot.Value = true; // Re-enables shooting when 'F' is released
        
    }

    IEnumerator  weaponhandchange()
    {
        change = false;
        yield return new WaitForSeconds(0.5f);
        change = true;
        
    }

    private void OnEnable()
    {

        global::UnlockWeapon.KatanaUnlock += Katana_unlock;

    }

    private void OnDisable()
    {
        
    }

    public void Katana_unlock()
    {
        change = true;
      
        
    }
}

