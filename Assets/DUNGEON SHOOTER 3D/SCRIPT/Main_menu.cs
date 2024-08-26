using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    // Start is called before the first frame update
	public void Play(){
		
		SceneManager.LoadScene(1);
	
	}
	
	public void Main_menus(){

		SceneManager.LoadScene("Main_Menu");
	}
}
