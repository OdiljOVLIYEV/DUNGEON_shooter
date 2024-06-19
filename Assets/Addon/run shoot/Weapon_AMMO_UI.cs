using System.Collections;
using System.Collections.Generic;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_AMMO_UI : MonoBehaviour
{
	public Text text;

	[SerializeField] private IntVariable ammo_UI;
	//[SerializeField] private IntVariable ammo_UI_shootgun;
	//private int max;
	//public int gun_ammo;
	//public int shotgun_ammo;
	//public int rifle_ammo;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	   
	    text.text=ammo_UI.Value.ToString();
    }
}
