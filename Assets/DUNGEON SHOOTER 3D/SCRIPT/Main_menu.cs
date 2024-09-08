using System;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    [SerializeField] private ScriptableEventNoParam SaveEvent;
    [SerializeField] private ScriptableEventNoParam LoadEvent;
    [SerializeField] private ScriptableEventNoParam Reset;
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
        SaveEvent.Raise();
    }
    
    public void ResetData()
    {
        if(Reset!=null)
        Reset.Raise();
    }
   
}