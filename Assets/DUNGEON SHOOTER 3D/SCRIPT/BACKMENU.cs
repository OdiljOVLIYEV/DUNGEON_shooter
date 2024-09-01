using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BACKMENU : MonoBehaviour
{
	public GameObject PauseMenu;
	public GameObject UI;
	[SerializeField] private BoolVariable Main_menu;
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		Cursor.visible = false;
		Main_menu.Value = false;
		PauseMenu.SetActive(false);
		if(UI!=null) 
			UI.SetActive(true);
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale=1f;
		AudioListener.volume=1f;
	}
	void Update()
	{
		
		
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			
			ToggleMenu();
		}
	}

	void ToggleMenu()
	{
		if (!Main_menu.Value)
		{
		
			
			Cursor.lockState = CursorLockMode.Confined;
			Main_menu.Value = true;
			PauseMenu.SetActive(true);
			if(UI!=null)
			UI.SetActive(false);
			Cursor.visible = true;
			Time.timeScale=0f;
			AudioListener.volume=0f;
		}
		else
		{
			
			Cursor.visible = false;
			Main_menu.Value = false;
			PauseMenu.SetActive(false);
			if(UI!=null)
			UI.SetActive(true);
			Cursor.lockState = CursorLockMode.Locked;
			Time.timeScale=1f;
			AudioListener.volume=1f;
		}
	}

	
		
		
	
	 
}
