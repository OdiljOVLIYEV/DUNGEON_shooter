using System;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using System.Collections;
using System.IO;
using Animancer;
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
    [SerializeField] private ScriptableEventNoParam SaveEvent;
    [SerializeField] private ScriptableEventNoParam UnlockEvent;
    [SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
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
        
        
        
        UpdateLockIcons();
        LoadWeaponUnlockStates();
       
    }

   
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            
            ShowWeaponUI(true);
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true;
            MouseLook MS = FindObjectOfType<MouseLook>();
            MS.enabled = false;
            WeaponUI_Open.Value = true;
         
        }
        else if (Input.GetMouseButtonUp(2))
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
                SaveEvent.Raise();
                SetActiveWeapon(i);
            }
        }

        // Q key switches between the last two selected weapons
        if (Input.GetKeyDown(KeyCode.Q) && previousWeaponIndex != -1)
        {
            SaveEvent.Raise();
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
        // Ensure index is valid
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

        // DO NOT change the unlock states, only switch the active weapon
        // The unlock states are managed independently and shouldn't be touched here

        ShowWeaponUI(false);
    }

    
    private void UpdateLockIcons()
    {
        for (int i = 0; i < lock_icon.Count; i++)
        {
            if (i < UnlockedWeapons.Count)
            {
                // Hide the lock icon if the weapon is unlocked, show it if still locked
                lock_icon[i].SetActive(!UnlockedWeapons[i].Value);
            }
            else
            {
                lock_icon[i].SetActive(false); // Hide lock icon for out-of-bounds items (safety check)
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

    public void UnlockWeapon(int unlockedWeaponIndex)
    {
        // Ensure index is valid
        if (unlockedWeaponIndex >= 0 && unlockedWeaponIndex < weapons.Count)
        {
            UnlockedWeapons[unlockedWeaponIndex].Value = true;  // Unlock the specified weapon

            // Ensure that all unlocked weapons remain unlocked
            for (int i = 0; i < UnlockedWeapons.Count; i++)
            {
                if (UnlockedWeapons[i].Value)
                {
                    lock_icon[i].SetActive(false); // Hide lock icon for unlocked weapons
                }
            }

            UpdateLockIcons(); // Update the lock icons to reflect changes
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
        UnlockEvent.OnRaised += SaveWeaponUnlockStates;
    }

    private void OnDisable()
    {
        global::UnlockWeapon.KatanaUnlock -= Katana_unlock;
       UnlockEvent.OnRaised -= SaveWeaponUnlockStates;
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
    public void SaveWeaponUnlockStates()
    {
        PlayerData playerData = SaveManager.instance.LoadPlayerData(); // Load existing data
        playerData.weaponUnlockStates = new List<bool>();  // Initialize the list

        // Save the unlock state for each weapon
        foreach (BoolVariable weaponUnlocked in UnlockedWeapons)
        {
            playerData.weaponUnlockStates.Add(weaponUnlocked.Value);
        }

        SaveManager.instance.SavePlayerData(playerData); // Save the updated data
    }
    public void LoadWeaponUnlockStates()
    {
        PlayerData playerData = SaveManager.instance.LoadPlayerData(); // Load saved data

        // If there are saved weapon unlock states, apply them
        if (playerData.weaponUnlockStates != null && playerData.weaponUnlockStates.Count == UnlockedWeapons.Count)
        {
            for (int i = 0; i < playerData.weaponUnlockStates.Count; i++)
            {
                UnlockedWeapons[i].Value = playerData.weaponUnlockStates[i];
            }
        }

        UpdateLockIcons(); // Update the UI icons to reflect the loaded state
    }

    
}