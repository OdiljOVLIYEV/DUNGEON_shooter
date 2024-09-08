using System;
using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Weapon_AMMO_UI : MonoBehaviour
{
	public TextMeshProUGUI text;

	[SerializeField] private IntVariable ammo_UI;
	[SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
	public TextMeshProUGUI gun_ammo;
	[SerializeField] private IntVariable gun_ammo_add;
	public TextMeshProUGUI Shotgun_ammo;
	public TextMeshProUGUI Super_Shotgun_ammo;
	[SerializeField] private IntVariable shotgun_ammo_add;
	public TextMeshProUGUI Rifle_ammo;
	[SerializeField] private IntVariable rifle_ammo_add;
	public TextMeshProUGUI Plasma_ammo;
	[SerializeField] private IntVariable plasma_ammo_add;
	public TextMeshProUGUI Rocket_launcher_ammo;
	[SerializeField] private IntVariable rocket_launcher_ammo_add;

	private PlayerData playerData;
	private void Start()
	{
		playerData = SaveManager.instance.LoadPlayerData();

		// Initialize ammo values from the player data
		gun_ammo_add.Value = playerData.Ammogun;
		shotgun_ammo_add.Value = playerData.AmmoShotgun;
		rifle_ammo_add.Value = playerData.Machinegun;
		plasma_ammo_add.Value = playerData.Plasma;
		rocket_launcher_ammo_add.Value = playerData.RocketLauncher;
		UpdateAmmoUI();
	}
	private void Update()
	{
		UpdateAmmoUI();
	}
	//private int max;
	//public int gun_ammo;
	//public int shotgun_ammo;
	//public int rifle_ammo;
	
    // Start is called before the first frame update
    private void OnEnable()
    {
	    UI_AMMO_UPDATE.OnRaised += UI_UPDATE;
	   // UpdateAmmoUI();
    }

    private void OnDisable()
    {
	    UI_AMMO_UPDATE.OnRaised -= UI_UPDATE;
    }

    void UI_UPDATE(int all_ammo_add)
    {
	    
	    text.text=all_ammo_add.ToString();
	    
	    
    }
    
    void UpdateAmmoUI()
    {
	    gun_ammo.text = gun_ammo_add.Value.ToString();
	    Shotgun_ammo.text = shotgun_ammo_add.Value.ToString();
	    Super_Shotgun_ammo.text = shotgun_ammo_add.Value.ToString(); // Use shotgun_ammo_add value for Super Shotgun as well
	    Rifle_ammo.text = rifle_ammo_add.Value.ToString();
	    Plasma_ammo.text = plasma_ammo_add.Value.ToString();
	    Rocket_launcher_ammo.text = rocket_launcher_ammo_add.Value.ToString();
    }
}
