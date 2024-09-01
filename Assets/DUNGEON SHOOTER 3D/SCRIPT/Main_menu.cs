using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{

    private void Start()
    {
        AudioListener.volume=1f;
        
        
    }

    private void Update()
    {
       
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenus()
    {
        SceneManager.LoadScene(0);
    }
}