using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons; // List of weapons
    public List<GameObject> weapons_icon;
    public List<GameObject> lock_icon;// List of weapon icons
    public GameObject Katana;
    public Image katana;
    public Image katanlock_icon;
    public GameObject weaponUI;
    
    public bool change = false;
    public static Action SwordEffectCall;
    [SerializeField] private List<BoolVariable> UnlockedWeapons;
    [SerializeField] private IntVariable UnlockedWeapon;
    [SerializeField] private BoolVariable canShoot;
    [SerializeField] private BoolVariable WeaponUI_Open;
    
    private int currentWeaponIndex = -1; 
    private int previousWeaponIndex = -1;
    private int lastWeaponIndex = -1;

    void Start()
    {
        katana.enabled = false;
        canShoot.Value = true;
        InitializeWeapons();
        InitializeWeaponIcons();
        weaponUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        WeaponUI_Open.Value = false;
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
            WeaponUI_Open.Value = true;
         
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
           
             
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor again
            Cursor.visible = false; 
            MouseLook MS = FindObjectOfType<MouseLook>();
            MS.enabled = true;
            WeaponUI_Open.Value = false;
            StartCoroutine(showfalse());
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
        if (Input.GetKeyDown(KeyCode.F) && change&& WeaponUI_Open==false)
        {
            Katana.SetActive(true);
            katana.enabled = true;
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
        if (index < 0 || index >= weapons.Count || index == currentWeaponIndex)
            return;

        // Deactivate the previously active weapon and its icon
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].SetActive(false);
            weapons_icon[currentWeaponIndex].SetActive(false);
        }

        // Update previous and current weapon indices
        previousWeaponIndex = currentWeaponIndex;
        currentWeaponIndex = index;

        // Activate the new weapon and its icon
        weapons[currentWeaponIndex].SetActive(true);
        weapons_icon[currentWeaponIndex].SetActive(true);

        // Update lock icons based on unlocked weapons
        UpdateLockIcons();

        ShowWeaponUI(false);
    }

    private void UpdateLockIcons()
    {
        for (int i = 0; i < lock_icon.Count; i++)
        {
            if (i < UnlockedWeapons.Count)
            {
                bool isUnlocked = UnlockedWeapons[i].Value;
                lock_icon[i].SetActive(!isUnlocked);
                
            }
            else
            {
                lock_icon[i].SetActive(false);
                
            }
        }
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
        katana.enabled = false;
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].SetActive(true);
            weapons_icon[currentWeaponIndex].SetActive(true);
        }
        canShoot.Value = true;
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
        katanlock_icon.enabled = false;
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
    }
    IEnumerator showfalse()
    {
        yield return new WaitForSeconds(0.1f); // Adjust this delay as needed
        ShowWeaponUI(false);
    }
}
 