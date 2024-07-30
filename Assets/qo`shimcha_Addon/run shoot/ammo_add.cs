using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ammo_add : MonoBehaviour
{
	[SerializeField] private IntVariable gun_ammo_add;
	[SerializeField] private IntVariable shotgun_ammo_add;
	[SerializeField] private IntVariable rifle_ammo_add;
	[SerializeField] private ScriptableEventInt UI_AMMO_UPDATE;
	//[SerializeField] private IntVariable rifle_ammo_add;
	public int gun_ammo;
	public int shotgun_ammo;
	public int rifle_ammo;
	public Image white;
	//public int rifle_ammo;
    // Start is called before the first frame update
    void Start()
	{
		
		
    }

    // Update is called once per frame
  

    // OnTriggerEnter is called when the Collider other enters the trigger.
	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag=="Player")
		{
			white.enabled = true;
			Invoke("whiteoff",0.1f);
			gun_ammo_add.Value += gun_ammo;// + shotgun_ammo_add + rifle_ammo_add;
			shotgun_ammo_add.Value += shotgun_ammo;
			rifle_ammo_add.Value += rifle_ammo;
			UI_AMMO_UPDATE.Raise(gun_ammo_add.Value);
			UI_AMMO_UPDATE.Raise(shotgun_ammo_add.Value);
			UI_AMMO_UPDATE.Raise(rifle_ammo_add);
			
		}
	}

	public void whiteoff()
	{
		white.enabled = false;
		Destroy(gameObject);
	}
}
