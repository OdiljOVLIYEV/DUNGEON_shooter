using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_AMMO : MonoBehaviour
{
	public Text text;
	
	public int gun_ammo;
	public int shotgun_ammo;
	public int rifle_ammo;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    text.text=gun_ammo.ToString();
    }
}
