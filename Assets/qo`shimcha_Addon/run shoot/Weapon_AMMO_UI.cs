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
	
	//private int max;
	//public int gun_ammo;
	//public int shotgun_ammo;
	//public int rifle_ammo;
	
    // Start is called before the first frame update
    private void OnEnable()
    {
	    UI_AMMO_UPDATE.OnRaised += UI_UPDATE;
    }

    private void OnDisable()
    {
	    UI_AMMO_UPDATE.OnRaised -= UI_UPDATE;
    }

    void UI_UPDATE(int all_ammo_add)
    {
	    
	    text.text=all_ammo_add.ToString();
	    
    }
}
