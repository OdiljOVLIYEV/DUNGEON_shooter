using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Obvious.Soap;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons; // List of weapons
    public List<GameObject> weapons_icon; // List of weapon icons
    public GameObject Katana;
    public GameObject weaponUI;
    
    public bool change = false;
    public static Action SwordEffectCall;
    [SerializeField] private List<BoolVariable> UnlockedWeapons;
    [SerializeField] private IntVariable UnlockedWeapon;
    [SerializeField] private BoolVariable canShoot;

    private int currentWeaponIndex = -1; 
    private int previousWeaponIndex = -1;
    private int lastWeaponIndex = -1;
    private bool uiVisible = false; // Track if the UI is visible

    void Start()
    {
        canShoot.Value = true;
        InitializeWeapons();
        InitializeWeaponIcons();
        weaponUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowWeaponUI(true);
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true;
            MouseLook MS = FindObjectOfType<MouseLook>();
            MS.enabled = false;
            DeactivateAllWeapons(); // Deactivate all weapons when UI is opened
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            ShowWeaponUI(false);
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor again
            Cursor.visible = false; 
            MouseLook MS = FindObjectOfType<MouseLook>();
            MS.enabled = true;
            ReactivateLastWeapon(); // Reactivate the last selected weapon
        }

        // Check for keyboard inputs 1-9 for weapon switching
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()) && UnlockedWeapons[i].Value)
            {
                SetActiveWeapon(i);
            }
        }

        // Q key switches between the last two selected weapons
        if (Input.GetKeyDown(KeyCode.Q) && previousWeaponIndex != -1)
        {
            SetActiveWeapon(previousWeaponIndex);
        }

        // F key to switch to Katana
        if (Input.GetKeyDown(KeyCode.F) && change)
        {
            Katana.SetActive(true);
            SwordEffectCall?.Invoke();
            if (currentWeaponIndex != -1)
            {
                weapons[currentWeaponIndex].SetActive(false);
                weapons_icon[currentWeaponIndex].SetActive(false);
            }
            canShoot.Value = false; // Disable shooting while 'F' is pressed
        }

        // Re-enable the current weapon when 'F' is released
        if (Input.GetKeyUp(KeyCode.F))
        {
           Invoke("WeaponOn", 0.3f);
        }
    }

    public void SetActiveWeapon(int index)
    {
        canShoot.Value = true;
        if (index < 0 || index >= weapons.Count || index == currentWeaponIndex)
            return;

        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].SetActive(false);
            weapons_icon[currentWeaponIndex].SetActive(false);
        }

        previousWeaponIndex = currentWeaponIndex;
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].SetActive(true);
        weapons_icon[currentWeaponIndex].SetActive(true);
        lastWeaponIndex = currentWeaponIndex; // Update lastWeaponIndex
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
            UnlockedWeapons[i].Value = false;
        }

        SetActiveWeapon(0);
        UnlockedWeapons[0].Value = true;
    }

    private void InitializeWeaponIcons()
    {
        foreach (GameObject icon in weapons_icon)
        {
            icon.SetActive(false);
        }

        if (weapons_icon.Count > 0)
        {
            weapons_icon[0].SetActive(true);
        }
    }

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
        canShoot.Value = true;
    }

    private void DeactivateAllWeapons()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        foreach (GameObject icon in weapons_icon)
        {
            icon.SetActive(false);
        }
    }

    private void ReactivateLastWeapon()
    {
        if (lastWeaponIndex != -1)
        {
            weapons[lastWeaponIndex].SetActive(true);
            weapons_icon[lastWeaponIndex].SetActive(true);
        }
    }

    IEnumerator weaponhandchange()
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
        global::UnlockWeapon.KatanaUnlock -= Katana_unlock;
    }

    public void Katana_unlock()
    {
        change = true;
    }

    // This method can be called by the weapon wheel buttons
    public void OnWeaponIconClick(int weaponIndex)
    {
        Debug.Log("Weapon icon clicked: " + weaponIndex);
    
        if (UnlockedWeapons[weaponIndex].Value)
        {
            SetActiveWeapon(weaponIndex);
        }
        else
        {
            Debug.Log("Weapon " + weaponIndex + " is locked.");
        }
    }
    
    void ShowWeaponUI(bool show)
    {
        weaponUI.SetActive(show);
        uiVisible = show;
    }
}
